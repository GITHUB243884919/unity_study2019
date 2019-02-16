using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UFrame.MessageCenter;
using GameName.Lua.Config;

namespace GameName.Battle.Logic
{
    public class BattleLogic : IMessageExecutor
    {
        BattleManager battleManager;
        bool displayOK = false;
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
                    displayOK = true;
                    Debug.LogError("display init ok");
                }
            }
        }

        List<UFrame.AI.SimpleMoveObjectCtr> tankCtrs = new List<UFrame.AI.SimpleMoveObjectCtr>();
        public void InitBattleStage()
        {
            stage_info si = stage_infoAPI.GetDataBy_id(1);
            List<tank_group_info> tgis = tank_group_infoAPI.GetDataListBy_tank_group_id(si.tank_group_id);

            L2D_BattleInit initMsg = new L2D_BattleInit();
            initMsg.tankGroup = new List<TankGroupInit>();
            for (int i = 0; i < tgis.Count; ++i)
            {
                Vector3 pos = new Vector3((float)tgis[i].pos.GetDouble(0), (float)tgis[i].pos.GetDouble(1), (float)tgis[i].pos.GetDouble(2));
                Vector3 dir = new Vector3((float)tgis[i].dir.GetDouble(0), (float)tgis[i].dir.GetDouble(1), (float)tgis[i].dir.GetDouble(2));

                var ti = tank_infoAPI.GetDataBy_tank_type(tgis[i].tank_type);
                Data.Tank tank = new Data.Tank();
                tank.SetDir(dir);
                tank.SetPos(pos);
                tank.SetSpeed(ti.speed);
                tank.SetTurnSpeed(0);
                tank.SetTurnType(UFrame.AI.TurnType.None);
                Data.BattleLogicDataManager.GetInstance().AddTank(tank);
                UFrame.AI.SimpleMoveObjectCtr tankCtr = new UFrame.AI.SimpleMoveObjectCtr();
                tankCtr.moveObject = tank;
                tankCtrs.Add(tankCtr);

                TankGroupInit tgi = new TankGroupInit();
                tgi.id = tank.GetID();
                tgi.tank_type = tgis[i].tank_type;
                tgi.pos = pos;
                tgi.dir = dir;
                initMsg.tankGroup.Add(tgi);

            }

            battleManager.battleMessageCenter.Send(initMsg);
        }

        public void Tick(int deltaTimeMS)
        {
            if (!displayOK)
            {
                return; 
            }

            TankPos(deltaTimeMS);
        }

        void TankPos(int deltaTimeMS)
        {
            L2D_TankPos msg = new L2D_TankPos();

            for (int i = 0; i < tankCtrs.Count; i++)
            {
                tankCtrs[i].Tick(deltaTimeMS);
                TankPos tankPos = new TankPos();
                tankPos.id = tankCtrs[i].moveObject.GetID();
                tankPos.pos = tankCtrs[i].moveObject.GetPos();
                tankPos.dir = tankCtrs[i].moveObject.GetDir();
                //Debug.LogError(tankPos.id + " " + tankPos.pos + " " + tankPos.dir);
                msg.tankGroup.Add(tankPos);
            }

            battleManager.battleMessageCenter.Send(msg);
        }
    }
}

