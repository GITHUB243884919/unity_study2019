/*
Author:		Augustine
History:	2.5.2016 创建
note   :    执行器的基础
 */

using System.Collections.Generic;
using UnityEngine;
using System;


public delegate void ParamDelegate(object obj);
public abstract class   CExecutor
{
    public ParamDelegate onFinished;

    public enum eExeState
    {
        Exe_Idle,
        Exe_WaitStart,
        Exe_Pause,
        Exe_Running
    }

    public class CParam
    {
        public CParam()
        {
            StartTime = 0.0f;
            SurviveTime = 0.0f;
            LoopNum = 1;
        }
        public virtual void Reset()
        {
            StartTime = 0.0f;
            SurviveTime = 0.0f;
            LoopNum = 1;
        }
        public float StartTime;
        public float SurviveTime;//total time;
        public int LoopNum; //0：无限循环 >0:循环次数
    }


    protected virtual void startImp() { }
    public virtual void Reset() { }
    protected abstract bool updateImp(float fDeltaTime);

    public CGroupExecutor Parent { get; protected set; }
    //为了效率
    public LinkedListNode<CExecutor> LinkNode;



    public eExeState ExeState { get; protected set; }

    public float StartTime;
    public float SurviveTime;//total time; <0 无限时间
    public int LoopNum; //0：无限循环 >0:循环次数

    public float mCurrentTime;

    public float currentTime { get; protected set; }//time elapsed from the point sound begin.


    protected int curLoop;


    public CExecutor()
    {
        Parent = null;
        LinkNode = new LinkedListNode<CExecutor>(this);
        ExeState = eExeState.Exe_Idle;

        LoopNum = 1;
        StartTime = 0.0f;
        SurviveTime = 1.0f;

        currentTime = 0.0f;
        curLoop = 0;
    }

    

    public virtual void InitParam(CExecutor.CParam param)
    {
        StartTime = param.StartTime;
        SurviveTime = param.SurviveTime;
        LoopNum = param.LoopNum;
    }

    public virtual void Play()
    {
        if (ExeState == eExeState.Exe_Pause)
        {
            ExeState = eExeState.Exe_Running;
        }
        else if (ExeState == eExeState.Exe_Idle)
        {
            ExeState = eExeState.Exe_WaitStart;
            currentTime = 0.0f;
            curLoop = 0;
        }
    }
    public virtual void Stop()
    {
        ExeState = eExeState.Exe_Idle;
    }
    public virtual bool isPause()
    {
        return ExeState == eExeState.Exe_Pause;
    }
    public virtual bool IsPlaying()
    {
        return ExeState != eExeState.Exe_Idle && ExeState != eExeState.Exe_Pause;
    }
    public virtual void Pause()
    {
        if (ExeState != eExeState.Exe_Idle)
        {
            ExeState = eExeState.Exe_Pause;
        }
        
    }
    public virtual void Clear()
    {
        Stop();
    }

    /// <summary>
    /// 执行器play之后,下一帧开始update.默认surviveTime大于starttime
    /// </summary>
    /// <param name="fDeltaTime"></param>
    public virtual void Update(float fDeltaTime)
    {
        if (IsPlaying())
        {
            currentTime += fDeltaTime;
            if (currentTime >= StartTime)
            {
                if (eExeState.Exe_WaitStart == ExeState)
                {
                    startImp();
                    ExeState = eExeState.Exe_Running;
                }
                bool bOver = updateImp(fDeltaTime);


                if (SurviveTime >= 0.0f )
                {
					if ((SurviveTime - currentTime) <= 0.0001f)
                    {
                        bOver = true;
                    }
                    else
                    {
                        bOver = false;                        
                    }
                }
                

                if (bOver)
                {
                    if (LoopNum > 0 && (curLoop + 1) >= LoopNum)
                    {
                        Stop();
                    }

                    else
                    {
                        ++curLoop;
                        int iOldLoop = curLoop;
                        Stop();
                        Play();
                        curLoop = iOldLoop;
                    }
                }
            }

        }
    }

    public void SetParent(CGroupExecutor parent, bool bNotifyGroup = true)
    {
        if (bNotifyGroup)
        {
            parent.AddExecutor(this);
        }

        Parent = parent;
    }


    

}


