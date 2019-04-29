using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using UFrame.FSM;
using UFrame.MessageCenter;
using Game.MessageDefine;
using UFrame.ToLua;
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
            //UFrameLuaClient.GetMainState().DoFile("UFrame/Game/GameState/StateLogin.lua");
            //var luaFunEnter = UFrameLuaClient.GetMainState().GetFunction("StateLogin.Enter");
            //if (luaFunEnter != null)
            //{
            //    Logger.LogWarp.Log("call luaFunEnter");
            //    luaFunEnter.Call();
            //    luaFunEnter.Dispose();
            //    luaFunEnter = null;
            //}

            //var luaMsgTable = UFrameLuaClient.GetMainState().GetTable("MessageCode");
            //Logger.LogWarp.Log("msg " + (int)(double)(luaMsgTable["GameLogic_Enter_Login"]) + " " + luaMsgTable["GameLogic_Enter_Login"].ToString());
            //Logger.LogWarp.Log("msg " + (int)(double)(luaMsgTable["UIMsg_Open_Login"]));
            //luaMsgTable.Dispose();
            //luaMsgTable = null;





            loginSuccess = false;
            Logger.LogWarp.Log("StateLogin Enter");
            MessageManager.GetInstance().gameMessageCenter.Regist((int)GameMsg.C2S_Login, MessageCallback);
            MessageManager.GetInstance().gameMessageCenter.Regist((int)GameMsg.S2C_Login, MessageCallback);

            //加载Login场景
            ResHelper.LoadScene(loginScene);
            //var getter = ResHelper.LoadGameObject("prefabs/ui/ui_login");
            //getter.Get();
            var getter = ResHelper.LoadGameObject("prefabs/cube");
            GameObject go = getter.Get();
            Logger.LogWarp.Log((go != null) + " " + go.name);
            Logger.LogWarp.Log("Canvas" + " " + (GameObject.Find("Canvas") != null));
            //go.transform.SetParent(GameObject.Find("Login_MainCamera").transform, false);
            //Logger.LogWarp.Log("prefabs/cube");



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

