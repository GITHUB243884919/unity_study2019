using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UFrame.Executor
{
    public abstract class ExecutorCondition : Executor
    {
        public ExecutorCondition()
            : base()
        {
            eState = EState.E_Idle;
            
        }

        public override void Play()
        {
            if (eState == EState.E_Idle)
            {
                eState = EState.E_WaitBegin;
            }
            else if (eState == EState.E_Pause)
            {
                eState = EState.E_Running;
            }
        }

        public override void Pause()
        {
            if (eState == EState.E_Running)
            {
                eState = EState.E_Pause;
            }

        }

        public override void Stop()
        {
            eState = EState.E_Stop;
        }

        public override void RePlay()
        {
            if (eState == EState.E_Running)
            {
                return;
            }

            if (eState == EState.E_Pause)
            {
                eState = EState.E_Running;
                return;
            }

            if (eState == EState.E_Stop || eState == EState.E_Finish)
            {
                eState = EState.E_Idle;
            }

        }

        public override void ControlTick(long ms)
        {
            if (eState != EState.E_Running &&
                eState != EState.E_WaitBegin)
            {
                return;
            }

            if (!RunningCondition())
            {
                return;
            }


            if (eState == EState.E_WaitBegin)
            {
                OnBegin();
                eState = EState.E_Running;
            }

            Tick(ms);

            if (FinishCondition())
            {
                OnFinish();
                eState = EState.E_Finish;
            }
            

        }

        public abstract bool RunningCondition();
        public abstract bool FinishCondition();

    }



}
