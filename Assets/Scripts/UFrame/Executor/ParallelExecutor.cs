using System.Collections.Generic;
using UnityEngine;
using System;

public class CParallelExecutor : CGroupExecutor
{

    public override void AddExecutor(CExecutor childExecutor)
    {
        if (!childExecutor.IsPlaying())
        {
            base.AddExecutor(childExecutor);

            // 运行模式下动态插入
            if (this.ExeState == eExeState.Exe_Running)
            {
                childExecutor.Play();

                //childExecutor.Update(this.currentTime - this.StartTime);
            }
        }
        else
        {
            //TODO
        }
    }

    public void AddExecutorNoUpdate(CExecutor childExecutor)
    {
        if (!childExecutor.IsPlaying())
        {
            base.AddExecutor(childExecutor);

            // 运行模式下动态插入
            if (this.ExeState == eExeState.Exe_Running)
            {
                childExecutor.Play();
            }
        }
    }

    public override void Play()
    {
        base.Play();

    }
    public override void Stop()
    {
        base.Stop();
        foreach (CExecutor curExecutor in linkList)
        {
            curExecutor.Stop();
        }
        if (onFinished != null)
        {
            onFinished(null);
        }
    }

    public override void Update(float fDeltaTime)
    {
        if (IsPlaying())
        {

            if (eExeState.Exe_WaitStart == ExeState)
            {
                startImp();
                ExeState = eExeState.Exe_Running;
            }
            bool bOver = updateImp(fDeltaTime);


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
                    Reset();
                    Play();
                    curLoop = iOldLoop;
                }
            }
        }

    }

    public override void Reset()
    {
        base.Reset();
        foreach (CExecutor curExecutor in linkList)
        {
            curExecutor.Reset();
        }
    }

    protected override void startImp()
    {
        LinkedListNode<CExecutor> CurNode = linkList.First;
        for (int i = 0; i < linkList.Count; ++i)
        {
            CExecutor executor = CurNode.Value;
            if (null != executor)
            {
                executor.Play();
            }
            CurNode = CurNode.Next;
        }
    }

    protected override bool updateImp(float fDeltaTime)
    {
        bool bOver = true;
        foreach(CExecutor curExecutor in linkList)
        {
            curExecutor.Update(fDeltaTime);
            if(curExecutor.IsPlaying()){
                bOver = false;
            }
        }
        return bOver;
    }


}
