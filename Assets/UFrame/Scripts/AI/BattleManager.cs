using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UFrame.MessageCenter;
using UFrame.LUA;
using GameName.Lua.Config;

public class BattleManager : MonoBehaviour {

    public DirectMessageCenter battleMessageCenter;
    public BattleLogic battleLogic;
    public BattleDisplay battleDisplay;

    // Use this for initialization
    void Start ()
    {
        LuaManager.Instance.Init();
        LuaManager.Instance.luaState.DoFile("init_lua_config");
        LuaConfigManager.Instance.Init();
        battleMessageCenter = new DirectMessageCenter();
        battleLogic = new BattleLogic(this);
        battleDisplay = new BattleDisplay(this);

        battleLogic.InitBattleStage();
    }


	
	// Update is called once per frame
	void Update () {
        battleMessageCenter.Tick();
        battleLogic.Tick((int)(Time.deltaTime * 1000));
        battleDisplay.Tick((int)(Time.deltaTime * 1000));
    }

}
