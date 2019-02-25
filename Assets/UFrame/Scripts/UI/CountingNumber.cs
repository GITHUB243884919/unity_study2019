using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 实现说明:
/// 
/// 指数变快和指数变慢都是通过指数和对数运算
/// 1.计算出要变化的数字区间
/// 
/// 2.将这个区间值算到指数或对数中
/// 
/// 3.根据累计时间在这个区间的指数值或对数值取插值
/// 以上1,2在InitExponentFast() 和 InitExponentSlow()实现
/// 第3点在OnExponentFast() 和 OnExponentSlow()实现
/// 
/// 4.指数变化的scale处理
/// 另外指数变慢，考虑到要计算e^x(x为变化区间)可能会非常大越界，因此做了scale处理，最大只可能计算e^10，不会越界
/// 根据公式 ln(e^x) = x*ln(e) 且 ln(e)=1, 用scale后的x(用x'表示) 带入 ln(e^x') = x'*ln(e) = x'
/// 这样就能完全可以在计算对数后，用scale回去。
/// 
/// 5.调试
/// 给出的题目是用ChangeTo这样一个函数去驱动这个功能，考虑到调试的方便
/// 在面板上用了一个bool值来驱动，省去了再写一个MonoBehaviour,然后调用
/// ChangeTo的麻烦。
/// 调试时，只需要在面板上设置好参数，运行，勾选对勾"勾选执行变化"
/// 
/// </summary>
public class CountingNumber : MonoBehaviour
{
    public enum ChangeType
    {
        Linear,       //线性变化
        ExponentFast, //指数变快
        ExponentSlow, //指数变快
    }

    Text text;

    int currNum = 0;

    int defaultNum = 0;

    /// <summary>
    /// 累计时间
    /// </summary>
    float accumulatedTime = 0;

    [SerializeField]
    [Header("变化时长")]
    float duration = 0;

    [SerializeField]
    [Header("变化到的值")]
    int target = 0;

    [SerializeField]
    [Header("选择变化方式")]
    public ChangeType changeType = ChangeType.ExponentFast;

    [SerializeField]
    [Header("勾选执行变化")]
    bool running = false;

    public AnimationCurve animCurrve = AnimationCurve.Linear(0, 0, 1, 1);

    #region ExponentFast
    float beginExp = 0;
    float endExp = 0;
    float currExp = 0;
    int orgNum = 0;
    #endregion

    #region ExponentSlow
    float beginLog = 0;
    float endLog = 0;
    float currLog = 0;
    float scale = 0;
    #endregion

    Dictionary<int, System.Action<float>> changeCallbacks = new Dictionary<int, System.Action<float>>();

    public void ChangeTo(int target, float duration)
    {
        this.target = target;
        accumulatedTime = 0;
        this.duration = duration;

        if (animCurrve == null)
        {
            animCurrve = AnimationCurve.Linear(0, 0, 1, 1);
        }

        switch(changeType)
        {
            case ChangeType.Linear:
                InitLinear();
                break;
            case ChangeType.ExponentFast:
                InitExponentFast();
                break;
            case ChangeType.ExponentSlow:
                InitExponentSlow();
                break;
            default:
                InitLinear();
                break;
        }

        running = true;
    }

    void Awake()
    {
        Init();
    }

    void Update()
    {
        if (!CouldRunning())
        {
            return;
        }

        float value = animCurrve.Evaluate(accumulatedTime / duration);
        value = Mathf.Clamp(value, 0, 1);

        changeCallbacks[(int)changeType](value);

        text.text = currNum.ToString();

    }

    void OnDestroy()
    {
        this.text = null;
    }

    void Init()
    {
        running = false;

        text = GetComponent<Text>();
        try
        {
            currNum = System.Convert.ToInt32(text.text);
        }
        catch
        {
            currNum = defaultNum;
            text.text = currNum.ToString();
        }

        changeCallbacks.Add((int)ChangeType.Linear,       OnLiner);
        changeCallbacks.Add((int)ChangeType.ExponentFast, OnExponentFast);
        changeCallbacks.Add((int)ChangeType.ExponentSlow, OnExponentSlow);

        #region Only For Test
        InitLinear();
        InitExponentFast();
        InitExponentSlow();
        #endregion
    }

    void InitLinear()
    {
        //原始值
        orgNum = currNum;
    }

    void InitExponentFast()
    {
        //原始值
        orgNum = currNum;
        //变化范围
        int change = target - orgNum;
        //指数, 指数范围即是时间范围
        beginExp = 0; //e^beginExp =1
        endExp = Mathf.Log(change); // e^endExp = change
    }
    
    void InitExponentSlow()
    {
        //原始值
        orgNum = currNum;
        //变化范围
        int change = target - orgNum;
        //把变化值缩小,防止Exp越界,并记录scale
        scale = (float)change / 10f;
        float scaleChange = change / scale;
        beginLog = 1;
        endLog = Mathf.Exp(scaleChange);
    }

    bool CouldRunning()
    {
        if (!running)
        {
            return false;
        }

        accumulatedTime += Time.deltaTime;
        if (accumulatedTime >= duration)
        {
            currNum = target;
            text.text = target.ToString();
            accumulatedTime = 0;
            running = false;
            return false;
        }

        return true;
    }

    void OnLiner(float currveVale)
    {
        currNum = (int)Mathf.Lerp(orgNum, target, currveVale);
    }

    void OnExponentFast(float currveVale)
    {
        currExp = Mathf.Lerp(beginExp, endExp, currveVale);
        currNum = orgNum + (int)Mathf.Exp(currExp);
    }

    void OnExponentSlow(float currveVale)
    {
        currLog = Mathf.Lerp(beginLog, endLog, currveVale);
        currNum = orgNum + (int)(Mathf.Log(currLog) * scale);
    }

    //tobak
    public static int BinarySearch(int[] arr, int low, int high, int key)
    {
        int mid = (low + high) / 2;
        if (low > high)
            return -1;
        else
        {
            if (arr[mid] == key)
                return mid;
            else if (arr[mid] > key)
                return BinarySearch(arr, low, mid - 1, key);
            else
                return BinarySearch(arr, mid + 1, high, key);
        }
    }

    /// <summary>
    /// 高斯求和公式
    /// </summary>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    int GaussSummation(int begin, int end)
    {
        int n = end - begin;

        return n * (n + 1) / 2;
    }
}
