using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UFrame.Executor
{
    public class ExecutorDuration : ExecutorCondition
    {
        /// <summary>
        /// 生存周期，总时长
        /// </summary>
        protected long surviveTimeMS;

        /// <summary>
        /// 累计时间
        /// </summary>
        protected long accumulatedTimeMS;

        public ExecutorDuration(int surviveTimeMS)
            : base()
        {
            this.surviveTimeMS = surviveTimeMS;
        }

        public override void Tick(long ms)
        {
            accumulatedTimeMS += ms;
            Debug.LogError(surviveTimeMS + " " + accumulatedTimeMS);
        }

        public override void OnBegin()
        {

        }

        public override void OnFinish()
        {

        }

        public override void RePlay()
        {
            if (eState == EState.E_Stop || eState == EState.E_Finish)
            {
                accumulatedTimeMS = 0;
            }
            base.RePlay();

        }

        public override bool RunningCondition()
        {
            return accumulatedTimeMS < surviveTimeMS;
        }

        public override bool FinishCondition()
        {
            //throw new System.NotImplementedException();
            return !RunningCondition();
        }
    }

}
