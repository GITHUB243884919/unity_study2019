using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CSerialExecutor : CGroupExecutor
{
    private CExecutor mCurExecutor;

    private float mResidueTime = 0.0f;

    public CSerialExecutor()
    {
        SurviveTime = -1.0f;
    }

    public override void AddExecutor(CExecutor childExecutor)
    {
            base.AddExecutor(childExecutor);
    }

    public override void Play()
    {
        base.Play();
    }
    public override void Stop()
    {

        base.Stop();
    }
    public override void Update(float fDeltaTime)
    {
        if (IsPlaying())
        {

            currentTime += fDeltaTime;
            if (currentTime >= StartTime)
            {
                if (eExeState.Exe_WaitStart == ExeState)
                {
                    startImp();
                    SurviveTime = mCurExecutor.SurviveTime;
                    ExeState = eExeState.Exe_Running;
                }
                bool bOver = updateImp(fDeltaTime);

                if (SurviveTime >= 0)
                {
                    if ((SurviveTime - currentTime) <= 0.0001f)
                    {
                        bOver = true;
                        mResidueTime = currentTime - SurviveTime;
                        currentTime = mResidueTime;
                    }
                    else
                    {
                        bOver = false;
                    }

                }

                if (bOver)
                {
                    linkList.RemoveFirst();
                    if (linkList.Count > 0)
                    {
                        mCurExecutor = linkList.First.Value;
                        SurviveTime = mCurExecutor.SurviveTime;

                        mCurExecutor.Play();
                        if (mResidueTime > 0f)
                        {
                            mCurExecutor.Update(mResidueTime);
                        }

                        bOver = false;
                    }
                    else
                    {
                        Stop();
                        if (onFinished != null)
                        {
                            onFinished(null);
                        }

                    }
                }
            }
        }
        
    }

    protected override void startImp()
    {
        if ( linkList.First!=null)
        {
            mCurExecutor = linkList.First.Value;
            mCurExecutor.Play();
        }
    }

    protected override bool updateImp(float fDeltaTime)
    {
        bool bOver = false;
        if (IsPlaying())
        {
                if (null != mCurExecutor)
                {
                    mCurExecutor.Update(fDeltaTime);
                    if (!mCurExecutor.IsPlaying())
                    {
                        bOver = true;
#if DEBUG && !PROFILER
                ///        Debug.Log("当前执行器结束");
#endif

                    }
                }
            }
        return bOver;
    }


}
