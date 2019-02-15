using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UFrame.MessageCenter;
//using LuaInterface;
//using UFrame.LUA;
using GameName.Lua.Config;

public class BattleLogic : IMessageExecutor
{
    BattleManager battleManager;
    public BattleLogic(BattleManager battleManager)
    {
        this.battleManager = battleManager;
        battleManager.battleMessageCenter.Regist((int)BattleMessageID.D2L_BattleInit, this);
    }

    public void Execute(UFrame.MessageCenter.Message msg)
    {
        if (msg.messageID == (int)BattleMessageID.D2L_BattleInit)
        {
            D2L_BattleInit initMsg = msg as D2L_BattleInit;
            if (initMsg.result)
            {

            }
        }
    }

    public void InitBattleStage()
    {
        stage_info si = stage_infoAPI.GetDataBy_id(1);
        List<tank_group_info> tgi = tank_group_infoAPI.GetDataListBy_tank_group_id(si.tank_group_id);

        L2D_BattleInit initMsg = new L2D_BattleInit();
        initMsg.messageID = (int)BattleMessageID.L2D_BattleInit;
        initMsg.tankGroup = new List<TankGroupInit>();// tgi;
        for (int i = 0; i < tgi.Count; ++i)
        {
            TankGroupInit ti = new TankGroupInit();
            ti.id = i;
            ti.tank_type = tgi[i].tank_type;
            float f = (float)(tgi[i].pos.GetDouble(0));
            ti.pos = new Vector3((float)tgi[i].pos.GetDouble(0), (float)tgi[i].pos.GetDouble(0), (float)tgi[i].pos.GetDouble(2));
            ti.dir = new Vector3((float)tgi[i].dir.GetDouble(0), (float)tgi[i].dir.GetDouble(0), (float)tgi[i].dir.GetDouble(2));
            initMsg.tankGroup.Add(ti);

        }

        battleManager.battleMessageCenter.Send(initMsg);
    }

    public void Tick(int deltaTimeMS)
    {

    }
}
