//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace UFrame.Executor
//{
//    public abstract class ExecutorByLoopCount : IExecutor
//    {
//        protected EState eState;
        
//        /// <summary>
//        /// 执行次数
//        /// </summary>
//        public int loopCount;

//        /// <summary>
//        /// 累计时间
//        /// </summary>
//        public int currLoopCount;

//        public ExecutorByLoopCount(int loopCount)
//        {
//            eState = EState.E_WaitBegin;
//            currLoopCount = 0;
//            this.loopCount = loopCount;
//        }

//        public EState GetState()
//        {
//            return eState;
//        }

//        public virtual void Play()
//        {
//            if (eState == EState.E_WaitBegin)
//            {
//                Begin();
//                eState = EState.E_Running;
//            }
//            else if (eState == EState.E_Pause)
//            {
//                eState = EState.E_Running;
//            }
//        }

//        public void Pause()
//        {
//            eState = EState.E_Pause;
//        }


//        public void Tick(long ms)
//        {
//            if (eState != EState.E_Running)
//            {
//                return;
//            }

//            if (currLoopCount >= loopCount)
//            {
//                return;
//            }

//            currLoopCount++;

//            if (currLoopCount >= loopCount)
//            {
//                TickImp(ms);
//                Finish();
//                eState = EState.E_Finish;
//            }
//            else
//            {
//                TickImp(ms);
//            }

//        }

//        public abstract void Begin();
//        public abstract void TickImp(long ms);
//        public abstract void Finish();

//    }
//}