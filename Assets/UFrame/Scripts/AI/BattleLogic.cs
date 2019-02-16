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
        Dictionary<int, UFrame.AI.SimpleMoveObjectCtr> tankCtrs = new Dictionary<int, UFrame.AI.SimpleMoveObjectCtr>();

        public BattleLogic(BattleManager battleManager)
        {
            this.battleManager = battleManager;
            battleManager.battleMessageCenter.Regist((int)BattleMessageID.D2L_BattleInit, this);
            battleManager.battleMessageCenter.Regist((int)BattleMessageID.JOY_Press, this);
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

            if (msg.messageID == (int)BattleMessageID.JOY_Press)
            {
                JOY_Press converMsg = msg as JOY_Press;
                tankCtrs[converMsg.tankID].moveObject.couldMove = converMsg.couldMove;
                tankCtrs[converMsg.tankID].moveObject.couldTurn = converMsg.couldTurn;

                if (converMsg.couldMove && converMsg.couldTurn)
                {
                    Vector3 newDir = tankCtrs[converMsg.tankID].moveObject.GetDir();
                    newDir += converMsg.dir;
                    newDir.Normalize();

                    tankCtrs[converMsg.tankID].moveObject.SetDir(newDir);
                    Debug.LogError("JOY_Press");
                }



                //if (converMsg.dir.x > 0)
                //{
                //    tankCtrs[converMsg.tankID].moveObject.SetTurnType(UFrame.AI.TurnType.Right);
                //}
                //else
                //{
                //    tankCtrs[converMsg.tankID].moveObject.SetTurnType(UFrame.AI.TurnType.Left);
                //}
            }
        }

        
        public void InitBattleStage()
        {

            L2D_BattleInit initMsg = new L2D_BattleInit();
            initMsg.tankGroup = new List<TankGroupInit>();

            InitSelf(initMsg);
            //InitNpc(initMsg);
            battleManager.battleMessageCenter.Send(initMsg);
        }

        void InitSelf(L2D_BattleInit initMsg)
        {
            Data.Player player = new Data.Player();
            player.isSelf = true;
            Data.Tank tank = new Data.Tank();
            tank.isPlayer = true;
            tank.isCaption = true;
            tank.tankType = 1000;
            tank.SetPos(Vector3.zero);
            tank.SetDir(new Vector3(0, 0, 1));
            tank.SetSpeed(3);
            tank.SetTurnSpeed(5);
            tank.SetTurnType(UFrame.AI.TurnType.None);
            player.tanks.Add(tank);

            Data.BattleLogicDataManager.GetInstance().AddTank(tank);
            UFrame.AI.SimpleMoveObjectCtr tankCtr = new UFrame.AI.SimpleMoveObjectCtr();
            tankCtr.moveObject = tank;
            tankCtrs.Add(tankCtr.moveObject.ID, tankCtr);


            Data.BattleLogicDataManager.GetInstance().AddPlayer(player);

            TankGroupInit tgi = new TankGroupInit();
            tgi.id = tank.GetID();
            tgi.isSelf = player.isSelf;
            tgi.isPlayer = tank.isPlayer;
            tgi.isCaptain = tank.isCaption;

            tgi.tank_type = tank.tankType;
            tgi.pos = tank.GetPos();
            tgi.dir = tank.GetDir();
            initMsg.tankGroup.Add(tgi);

        }

        void InitNpc(L2D_BattleInit initMsg)
        {
            stage_info si = stage_infoAPI.GetDataBy_id(1);
            List<tank_group_info> tgis = tank_group_infoAPI.GetDataListBy_tank_group_id(si.tank_group_id);

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
                tankCtrs.Add(tankCtr.moveObject.ID, tankCtr);

                TankGroupInit tgi = new TankGroupInit();
                tgi.id = tank.GetID();
                tgi.tank_type = tgis[i].tank_type;
                tgi.pos = pos;
                tgi.dir = dir;
                initMsg.tankGroup.Add(tgi);

            }
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

            foreach(var v in tankCtrs.Values)
            {
                v.Tick(deltaTimeMS);
                TankPos tankPos = new TankPos();
                tankPos.id = v.moveObject.GetID();
                tankPos.pos = v.moveObject.GetPos();
                tankPos.dir = v.moveObject.GetDir();
                
                msg.tankGroup.Add(tankPos);
            }

            battleManager.battleMessageCenter.Send(msg);
        }
    }
}

