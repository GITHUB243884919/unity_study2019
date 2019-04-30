using UFrame.FSM;
using Game.MessageDefine;
using UFrame.ResourceManagement;
using UFrame.ToLua;

namespace Game
{
    public class StateLogin : FSMState
    {
        public bool loginSuccess = false;
        int GameLogic_LoginSuccessed;
        LuaInterface.LuaTable luaMsgTable;
        public StateLogin(string stateName, FSMMachine fsmCtr) : base(stateName, fsmCtr)
        {
        }

        public override void Enter(string preStateName)
        {
            Logger.LogWarp.Log("StateLogin Enter");
            base.Enter(preStateName);

            loginSuccess = false;
            
            UFrameLuaClient.GetMainState().DoFile("UFrame/Game/GameState/StateLogin.lua");
            var luaFunEnter = UFrameLuaClient.GetMainState().GetFunction("StateLogin.Enter");
            if (luaFunEnter != null)
            {
                Logger.LogWarp.Log("call luaFunEnter");
                luaFunEnter.Call();
                luaFunEnter.Dispose();
                luaFunEnter = null;
            }

            luaMsgTable = UFrameLuaClient.GetMainState().GetTable("MessageCode");
            GameLogic_LoginSuccessed = (int)(double)(luaMsgTable["GameLogic_LoginSuccessed"]);
            MessageManager.GetInstance().gameMessageCenter.Regist(GameLogic_LoginSuccessed, MessageCallback);

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

        }

        protected override void GetLeaveParam()
        {
        }

        public override void Leave()
        {
            MessageManager.GetInstance().gameMessageCenter.UnRegist(GameLogic_LoginSuccessed, MessageCallback);
            luaMsgTable.Dispose();
            luaMsgTable = null;
            base.Leave();
        }

        bool CouldHome()
        {
            return loginSuccess;
        }

        public void MessageCallback(UFrame.MessageCenter.Message msg)
        {
            if (msg.messageID == GameLogic_LoginSuccessed)
            {
                Logger.LogWarp.Log("msg.messageID == GameLogic_LoginSuccessed");
                loginSuccess = true;
            }
        }
    }
}

