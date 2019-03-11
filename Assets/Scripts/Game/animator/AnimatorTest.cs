using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimatorTest : MonoBehaviour
{
    public enum AnimState
    {
        Idle,
        Run,
        Hit,
        Attack,
    }

    AnimState currState;
    AnimState preState;  
    Button btn;
    Animator animator;
    // Use this for initialization

    
    void Awake ()
    {
        animator = GetComponent<Animator>();
        animator.ResetTrigger("idle");
        animator.ResetTrigger("run");
        animator.ResetTrigger("attack");
        animator.ResetTrigger("hit");
        preState = currState = AnimState.Idle;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (animator == null) return;

        var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        var clipInfo = animator.GetCurrentAnimatorClipInfo(0)[0];
        StoreState(clipInfo.clip.name);


        PlayFinishedReturnToIdle(stateInfo);
        //PlayFinishedReturnBefore(stateInfo);
    }

    void StoreState(string clipName)
    {
        AnimState tmp = AnimState.Idle;
        switch (clipName)
        {
            case "stand":
                tmp = AnimState.Idle;
                break;
            case "hit":
                tmp = AnimState.Hit;
                break;
            case "attack":
                tmp = AnimState.Attack;
                break;
            case "run":
                tmp = AnimState.Run;
                break;
        }
        preState = currState;
        currState = tmp;
        //Debug.LogError(preState + " " + currState);
    }

    /// <summary>
    /// 播放完毕后回到Idle
    /// </summary>
    /// <param name="stateInfo"></param>
    void PlayFinishedReturnToIdle(AnimatorStateInfo stateInfo)
    {
        //不设置Has Exit Time 切换会流畅
        if (stateInfo.IsName("attack") || stateInfo.IsName("hit"))
        {
            if (stateInfo.normalizedTime > 1.0f)
            {
                animator.SetTrigger("idle");
            }
        }
    }

    void PlayFinishedReturnBefore(AnimatorStateInfo stateInfo)
    {
        //不设置Has Exit Time 切换会流畅
        if (stateInfo.IsName("attack") || stateInfo.IsName("hit"))
        {
            if (stateInfo.normalizedTime > 1.0f)
            {
                //animator.SetTrigger("idle");
                if (preState == AnimState.Run)
                {
                    animator.SetTrigger("run");
                }
            }
        }
    }
}
