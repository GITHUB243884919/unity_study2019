using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using UFrame.FSM;
using UFrame.MessageCenter;
using Game.MessageDefine;
namespace Game
{
    public class StateLoginMessageExecutor : BroadcastMessageExecutor
    {
        StateLogin stateLogin;
        public StateLoginMessageExecutor(StateLogin stateLogin) : base()
        {
            this.stateLogin = stateLogin;
        }
        public override void Execute(UFrame.MessageCenter.Message msg)
        {
            //Debug.LogError("Execute " + msg.messageID);
            if (msg.messageID == (int)GameMsg.C2S_Login)
            {
                stateLogin.loginSuccess = true;
            }
        }
    }

    public class StateLogin : FSMState
    {
        public bool loginSuccess = false;
        StateLoginMessageExecutor msgExe;
        string loginScene;
        public StateLogin(string stateName, FSMMachine fsmCtr) : base(stateName, fsmCtr)
        {
            msgExe = new StateLoginMessageExecutor(this);
        }

        public override void Enter(string preStateName)
        {
            base.Enter(preStateName);
            //加载Login场景
            ResHelper.LoadScene(loginScene);
            MessageManager.GetInstance().gameMessageCenter.Regist((int)GameMsg.C2S_Login, msgExe);
            MessageManager.GetInstance().gameMessageCenter.Regist((int)GameMsg.S2C_Login, msgExe);

        }

        public override void AddAllConvertCond()
        {
            AddConvertCond("Home", CouldHome);
        }

        public override void Tick(int deltaTimeMS)
        {
        }

        protected override void GetEnterParam()
        {
            loginScene = "scenes/login";
        }

        protected override void GetLeaveParam()
        {
        }

        public override void Leave()
        {
            MessageManager.GetInstance().gameMessageCenter.UnRegist((int)GameMsg.C2S_Login, msgExe);
            base.Leave();
        }

        bool CouldHome()
        {
            return loginSuccess;
        }
    }
}

