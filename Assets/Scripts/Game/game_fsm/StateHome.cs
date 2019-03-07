using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using UFrame.FSM;
using UFrame.MessageCenter;
using Game.MessageDefine;
namespace Game
{
    public class StateHomeMessageExecutor : BroadcastMessageExecutor
    {
        StateHome stateHome;
        public StateHomeMessageExecutor(StateHome stateHome) : base()
        {
            this.stateHome = stateHome;
        }
        public override void Execute(UFrame.MessageCenter.Message msg)
        {
            if (msg.messageID == (int)GameMsg.Return_Login)
            {
                stateHome.returnLogin = true;
            }
        }
    }


    public class StateHome : FSMState
    {
        public bool returnLogin = false;
        string sceneHome;
        StateHomeMessageExecutor msgExe;
        public StateHome(string stateName, FSMMachine fsmCtr) : base(stateName, fsmCtr)
        {
            msgExe = new StateHomeMessageExecutor(this);
        }

        public override void Enter(string preStateName)
        {
            base.Enter(preStateName);

            returnLogin = false;
            MessageManager.GetInstance().gameMessageCenter.Regist((int)GameMsg.Return_Login, msgExe);

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
            MessageManager.GetInstance().gameMessageCenter.UnRegist((int)GameMsg.Return_Login, msgExe);
            returnLogin = false;
            base.Leave();
        }

        bool CouldReturnLogin()
        {
            return this.returnLogin;
        }



    }
}

