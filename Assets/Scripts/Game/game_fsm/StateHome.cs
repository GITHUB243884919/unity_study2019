using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using UFrame.FSM;
using UFrame.MessageCenter;
using Game.MessageDefine;
namespace Game
{
    public class StateHome : FSMState, IMessageExecutor
    {
        //bool loginSuccess = false;
        public StateHome(string stateName, FSMMachine fsmCtr) : base(stateName, fsmCtr)
        {
        }

        public override void Enter(string preStateName)
        {
            base.Enter(preStateName);
            //加载Login场景
            ResHelper.LoadScene("scenes/home");
            //GetMessageCenter().Regist((int)GameMessage.Login, this);
            //AddConvertCond("home", CouldHome);


        }

        public override void AddAllConvertCond()
        {
            //throw new NotImplementedException();
        }

        public override void Tick(int deltaTimeMS)
        {
            //throw new NotImplementedException();
        }

        protected override void GetEnterParam()
        {
            //throw new NotImplementedException();
        }

        protected override void GetLeaveParam()
        {
            //throw new NotImplementedException();
        }

        public void Execute(UFrame.MessageCenter.Message msg)
        {
            //if (msg.messageID == (int)GameMessage.Login)
            //{

            //}
        }

        //bool CouldHome()
        //{
        //    return loginSuccess;
        //}
    }
}

