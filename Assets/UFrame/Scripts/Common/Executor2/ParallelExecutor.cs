using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UFrame.Executor
{
    public class ParallelExecutor : GroupExecutor
    {
        bool isFinish = false;
        public override void Play()
        {
            RePlay();
            if (eState == EState.E_Idle)
            {
                eState = EState.E_WaitBegin;
            }
            else if (eState == EState.E_Pause)
            {
                eState = EState.E_Running;
            }

            foreach (var executor in this.linkList)
            {
                executor.Play();
            }

        }

        public override void Pause()
        {
            eState = EState.E_Pause;
            foreach (var executor in this.linkList)
            {
                executor.Pause();
            }
        }

        public override void Stop()
        {
            eState = EState.E_Stop;
            foreach (var executor in this.linkList)
            {
                executor.Stop();
            }
        }

        public override void RePlay()
        {
            if (eState == EState.E_Running)
            {
                return;
            }

            eState = EState.E_WaitBegin;

            foreach (var executor in this.linkList)
            {
                executor.RePlay();
            }
        }

        public override bool RunningCondition()
        {
            return true;
        }

        public override bool FinishCondition()
        {
            return isFinish;
        }
        
        public override void OnBegin()
        {

        }

        public override void Tick(long ms)
        {
            isFinish = true;
            foreach (var executor in linkList)
            {

                executor.ControlTick(ms);
                if (executor.eState != EState.E_Finish)
                {
                    isFinish = false;
                }

            }
        }

        public override void OnFinish()
        {

        }
    }
}

