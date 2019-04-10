using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UFrame.Common;

public class UUIManager : Singleton<UUIManager>, ISingleton
{
    Dictionary<string, UUILoadBase> UIDic = new Dictionary<string, UUILoadBase>();
    Transform UI_Root;
    public void Init()
    {
        UI_Root = GameObject.Find("UI Root/LayerMain").transform;
    }

    public void RegisterUI(UUILoadBase UILoad)
    {
        UIDic.Add(UILoad.UIName, UILoad);
    }

    public void OpenUI(string UIName, bool isShow)
    {
        UUILoadBase UILoad = null;
        if (UIDic.TryGetValue(UIName, out UILoad))
        {
            UILoad.Load();
            UILoad.UIObj.SetActive(isShow);
            UILoad.UITrans.SetParent(UI_Root, false);
            //层级
            //UILoad.UITrans.SetAsLastSibling();
            //UILoad.UITrans.SetAsFirstSibling();
        }
    }

    public void CloseUI(string UIName)
    {
        UUILoadBase UILoad = null;
        if (UIDic.TryGetValue(UIName, out UILoad))
        {
            //Logger.LogWarp.LogError("CloseUI " + UILoad.UIPrefabPath);
            
            GameObject.Destroy(UILoad.UIObj);
            UILoad.UIObj = null;
            UILoad.UITrans = null;
        }
    }
}


public abstract class UUILoadBase
{
    public GameObject UIObj;
    public Transform UITrans;
    string uiName;
    public virtual string UIName
    {
        get
        {
            if (string.IsNullOrEmpty(uiName))
            {
                uiName = this.GetType().ToString().Replace("_Load", "");
            }
            return uiName;
        }
    }

    string prefabPath;
    public virtual string UIPrefabPath
    {
        get
        {
            if (string.IsNullOrEmpty(prefabPath))
            {
                prefabPath = "Prefabs/UI/Activity/" + UIName + ".prefab";
            }
            //Logger.LogWarp.LogError(prefabPath);
            return prefabPath;
        }
    }

    public void Load()
    {
        //UnityEngine.Object res = ResourceManager.Instance.LoadResourceSync(UIPrefabPath, false);
        //UIObj = GameObject.Instantiate(res) as GameObject;
        UITrans = UIObj.transform;
    }
}

public class UI_ActivityButtons_Load : UUILoadBase
{
}

////////////////////////////////////////////////////////////////
///

public class UIUtil
{
    public static GameObject InstantiateSeed(GameObject seed, Transform parentNode, bool worldPositionStays = false)
    {
        GameObject obj = GameObject.Instantiate<GameObject>(seed);
        obj.transform.SetParent(parentNode, worldPositionStays);
        obj.SetActive(true);
        return obj;
    }

    public static string DownCountFormat(long downCountValue)
    {
        int days = 0;
        int hours = 0;
        int minutes = 0;
        int seconds = 0;

        if (downCountValue <= 0)
        {
            return "";
        }

        days = (int)(downCountValue / 86400);
        downCountValue -= (days * 86400);

        if (downCountValue > 0)
        {
            hours = (int)(downCountValue / 3600);
            downCountValue -= (hours * 3600);
        }

        if (downCountValue > 0)
        {
            minutes = (int)(downCountValue / 60);
            downCountValue -= (minutes * 60);
        }

        seconds = (int)downCountValue;

        if (days > 0)
        {
            hours = days * 24 + hours;
        }

        return string.Format("{0:00}:{1:00}:{2:00}",
            hours, minutes, seconds);

    }
}
