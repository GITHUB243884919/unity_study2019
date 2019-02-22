using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UFrame.MessageCenter;
using GameName.Lua.Config;
using FixMath;

namespace GameName.Battle.Logic
{
    public partial class BattleLogic : IMessageExecutor
    {
        void InitBattleField()
        {
            L2D_BattleInit initMsg = new L2D_BattleInit();
            initMsg.tankGroup = new List<TankGroupInit>();

            InitSelf(initMsg);
            //InitNpc(initMsg);

            InitAvoidance(initMsg);

            SendBattleMessage(initMsg);

        }

        void InitSelf(L2D_BattleInit initMsg)
        {
            Data.Player player = new Data.Player();
            player.isSelf = true;
            Data.Tank tank = new Data.Tank();
            tank.isPlayer = true;
            tank.isCaption = true;
            tank.tankType = 1000;
            tank.moveData.detectionLen = new F64(5);
            tank.moveData.detectionWidth = new F64(0.8);

            tank.SetPos(new F64Vec3(0, 0, 30));
            tank.SetDir(new F64Vec3(0, 0, -1));
            tank.SetSpeed(new F64(5));
            tank.SetTurnSpeed(new F64(15));
            tank.SetTurnType(UFrame.AI.TurnType.None);
            player.tanks.Add(tank);
            logicDataManager.AddTank(tank);

            UFrame.AI.SimpleMoveObjectCtr tankCtr = new UFrame.AI.SimpleMoveObjectCtr(this);
            tankCtr.moveObject = tank;
            logicDataManager.AddTankCtr(tankCtr);

            logicDataManager.AddPlayer(player);

            TankGroupInit tgi = new TankGroupInit();
            tgi.id = tank.GetID();
            tgi.isSelf = player.isSelf;
            tgi.isPlayer = tank.isPlayer;
            tgi.isCaptain = tank.isCaption;

            tgi.tank_type = tank.tankType;
            tgi.pos = tank.GetPos().ToUnityVector3();
            tgi.dir = tank.GetDir().ToUnityVector3();
            tgi.detectionLen = tank.moveData.detectionLen.Float;
            tgi.detectionWidth = tank.moveData.detectionWidth.Float;
            initMsg.tankGroup.Add(tgi);
        }

        void InitNpc(L2D_BattleInit initMsg)
        {
            //stage_info si = stage_infoAPI.GetDataBy_id(1);
            //List<tank_group_info> tgis = tank_group_infoAPI.GetDataListBy_tank_group_id(si.tank_group_id);

            //for (int i = 0; i < tgis.Count; ++i)
            //{
            //    Vector3 pos = new Vector3((float)tgis[i].pos.GetDouble(0), (float)tgis[i].pos.GetDouble(1), (float)tgis[i].pos.GetDouble(2));
            //    Vector3 dir = new Vector3((float)tgis[i].dir.GetDouble(0), (float)tgis[i].dir.GetDouble(1), (float)tgis[i].dir.GetDouble(2));

            //    var ti = tank_infoAPI.GetDataBy_tank_type(tgis[i].tank_type);
            //    Data.Tank tank = new Data.Tank();
            //    tank.SetDir(dir);
            //    tank.SetPos(pos);
            //    tank.SetSpeed(ti.speed);
            //    tank.SetTurnSpeed(0);
            //    tank.SetTurnType(UFrame.AI.TurnType.None);


            //    Data.BattleLogicDataManager.GetInstance().AddTank(tank);
            //    UFrame.AI.SimpleMoveObjectCtr tankCtr = new UFrame.AI.SimpleMoveObjectCtr();
            //    tankCtr.moveObject = tank;
            //    tankCtrs.Add(tankCtr.moveObject.ID, tankCtr);

            //    TankGroupInit tgi = new TankGroupInit();
            //    tgi.id = tank.GetID();
            //    tgi.tank_type = tgis[i].tank_type;
            //    tgi.pos = pos;
            //    tgi.dir = dir;
            //    initMsg.tankGroup.Add(tgi);

            //}
        }

        void InitAvoidance(L2D_BattleInit initMsg)
        {
            Data.Avoidance adv = new Data.Avoidance();
            adv.pos = new F64Vec3(0, 0, 10);
            //adv.radius = F64.Half;
            adv.radius = new F64(1.6);
            logicDataManager.AddAvoidance(adv);

            initMsg.avoidances = new List<Avoidance>();
            Avoidance av = new Avoidance();
            av.pos = adv.pos.ToUnityVector3();
            av.rad = adv.radius.Float;
            initMsg.avoidances.Add(av);
        }

    }
}
