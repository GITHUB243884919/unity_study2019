using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using UFrame.FSM;
using UFrame.MessageCenter;
using Game.MessageDefine;
namespace Game
{
    public class StateLogin : FSMState
    {
        public bool loginSuccess = false;
        //StateLoginMessageExecutor msgExe;
        string loginScene;
        public StateLogin(string stateName, FSMMachine fsmCtr) : base(stateName, fsmCtr)
        {
        }

        public override void Enter(string preStateName)
        {
            
            base.Enter(preStateName);
            loginSuccess = false;

            MessageManager.GetInstance().gameMessageCenter.Regist((int)GameMsg.C2S_Login, MessageCallback);
            MessageManager.GetInstance().gameMessageCenter.Regist((int)GameMsg.S2C_Login, MessageCallback);
            
            //加载Login场景
            ResHelper.LoadScene(loginScene);
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
            MessageManager.GetInstance().gameMessageCenter.UnRegist((int)GameMsg.C2S_Login, MessageCallback);
            MessageManager.GetInstance().gameMessageCenter.UnRegist((int)GameMsg.S2C_Login, MessageCallback);
            
            loginSuccess = false;
            base.Leave();
        }

        bool CouldHome()
        {
            return loginSuccess;
        }

        public void MessageCallback(UFrame.MessageCenter.Message msg)
        {
            //Debug.LogError("Execute " + msg.messageID);
            if (msg.messageID == (int)GameMsg.C2S_Login)
            {
                loginSuccess = true;
            }
        }
    }
}

