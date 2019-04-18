﻿using UnityEngine;
using UFrame.FSM;
using UFrame.Update;

namespace Game
{
    public class StateUpdate : FSMState
    {
        public StateUpdate(string stateName, FSMMachine fsmCtr) : base(stateName, fsmCtr)
        {
        }

        public override void Enter(string preStateName)
        {
            base.Enter(preStateName);
#if RES_AB
            UpdateManager.GetInstance().EnsureLocalGameVersionInfomation();
#endif

        }

        public override void AddAllConvertCond()
        {
            AddConvertCond("Login", CouldLogin);
        }

        public override void Tick(int deltaTimeMS)
        {
            UpdateManager.GetInstance().Tick();
        }

        protected override void GetEnterParam()
        {
        }

        protected override void GetLeaveParam()
        {
        }

        public override void Leave()
        {
            base.Leave();
        }

        bool CouldLogin()
        {
#if RES_AB
            return UpdateManager.GetInstance().updateFinished;
#else
            return true;
#endif
        }

    }
}

