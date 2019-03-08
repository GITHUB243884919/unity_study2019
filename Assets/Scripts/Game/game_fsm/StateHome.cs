using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using UFrame.FSM;
using UFrame.MessageCenter;
using Game.MessageDefine;
namespace Game
{
    public class StateHome : FSMState
    {
        public bool returnLogin = false;
        string sceneHome;
        public StateHome(string stateName, FSMMachine fsmCtr) : base(stateName, fsmCtr)
        {
        }

        public override void Enter(string preStateName)
        {
            base.Enter(preStateName);

            returnLogin = false;
                        
            MessageManager.GetInstance().gameMessageCenter.Regist((int)GameMsg.C2C_Return_Login, MessageCallback);
            //加载Home场景
            ResHelper.LoadScene(sceneHome);
        }

        public override void AddAllConvertCond()
        {
            AddConvertCond("Login", CouldReturnLogin);
        }

        public override void Tick(int deltaTimeMS)
        {
        }

        protected override void GetEnterParam()
        {
            sceneHome = "scenes/home";
        }

        protected override void GetLeaveParam()
        {
        }

        public override void Leave()
        {
            MessageManager.GetInstance().gameMessageCenter.UnRegist((int)GameMsg.C2C_Return_Login, MessageCallback);
            returnLogin = false;
            base.Leave();
        }

        bool CouldReturnLogin()
        {
            return this.returnLogin;
        }

        public void MessageCallback(UFrame.MessageCenter.Message msg)
        {
            if (msg.messageID == (int)GameMsg.C2C_Return_Login)
            {
                returnLogin = true;
            }
        }

    }
}

