using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UFrame.Executor
{
    public enum EState
    {
        E_Idle,
        E_WaitBegin,
        E_Running,
        E_Pause,
        E_Stop,
        E_Finish,
    }

    public abstract class Executor
    {
        public EState eState;

        public LinkedListNode<Executor> linkNode;

        public GroupExecutor parent;

        public Executor()
        {
            this.linkNode = new LinkedListNode<Executor>(this);
        }

        public void SetParent(GroupExecutor parent, bool immediately = false)
        {
            parent.AddExecutor(this, immediately);
        }

        public abstract void Play();
        public abstract void Pause();
        public abstract void Stop();
        public abstract void RePlay();

        public abstract void ControlTick(long ms);
        public abstract void Tick(long ms);
        public abstract void OnBegin();
        public abstract void OnFinish();
        
    }

}


