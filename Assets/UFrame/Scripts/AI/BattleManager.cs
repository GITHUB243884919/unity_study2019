using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UFrame.MessageCenter;
using UFrame.LUA;
using GameName.Lua.Config;

namespace GameName.Battle
{
    public class BattleManager : MonoBehaviour
    {

        public DirectMessageCenter battleMessageCenter;
        public Logic.BattleLogic battleLogic;
        public Display.BattleDisplay battleDisplay;

        // Use this for initialization
        void Start()
        {
            Application.targetFrameRate = 30;
            LuaManager.GetInstance().Init();
            LuaManager.GetInstance().luaState.DoFile("init_lua_config");
            LuaConfigManager.Instance.Init();
            battleMessageCenter = new DirectMessageCenter();
            battleLogic = new Logic.BattleLogic(this);
            battleDisplay = new Display.BattleDisplay(this);

            battleLogic.InitBattleStage();
        }



        // Update is called once per frame
        void Update()
        {
            battleMessageCenter.Tick();
            battleLogic.Tick((int)(Time.deltaTime * 1000));
            battleDisplay.Tick((int)(Time.deltaTime * 1000));
        }

    }

}
