using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UFrame.Executor
{
    public class SerialExecutor : GroupExecutor
    {
        Executor currExecutor;
        LinkedListNode<Executor> nextNode;

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

            if (currExecutor == null)
            {
                currExecutor = linkList.First.Value;
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
            currExecutor = linkList.First.Value;
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
            return (currExecutor != null);
        }

        public override bool FinishCondition()
        {
            return !RunningCondition();
        }
        
        public override void OnBegin()
        {

        }

        public override void Tick(long ms)
        {
            if (currExecutor != null)
            {
                currExecutor.ControlTick(ms);
                if (/*currExecutor.eState == EState.E_Stop ||*/
                    currExecutor.eState == EState.E_Finish)
                {
                    nextNode = currExecutor.linkNode.Next;
                    if (nextNode == null)
                    {
                        currExecutor = null;
                    }
                    else
                    {
                        currExecutor = nextNode.Value;
                        currExecutor.Play();
                    }
                }
            }
        }

        public override void OnFinish()
        {

        }
    }
}

