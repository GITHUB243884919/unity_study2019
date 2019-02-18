using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UFrame.MessageCenter;
using GameName.Lua.Config;
using FixMath;
namespace GameName.Battle.Logic
{
    public class BattleLogic : IMessageExecutor
    {
        BattleManager battleManager;
        bool displayOK = false;
        public Dictionary<int, UFrame.AI.SimpleMoveObjectCtr> tankCtrs = new Dictionary<int, UFrame.AI.SimpleMoveObjectCtr>();
        Dictionary<int, System.Action<UFrame.MessageCenter.Message>> battleMessageCallbacks =
            new Dictionary<int, System.Action<UFrame.MessageCenter.Message>>();

        LogicModule logicMoudle = new LogicModule();

        public BattleLogic(BattleManager battleManager)
        {
            this.battleManager = battleManager;

            InitBattleMessage();
            InitMoudle();
        }

        public void Tick(int deltaTimeMS)
        {
            if (!displayOK)
            {
                return;
            }

            logicMoudle.Tick(deltaTimeMS);
        }

        public void SendBattleMessage(UFrame.MessageCenter.Message msg)
        {
            battleManager.battleMessageCenter.Send(msg);
        }

        public void Execute(UFrame.MessageCenter.Message msg)
        {
            battleMessageCallbacks[msg.messageID](msg);
        }

        public void InitBattleStage()
        {
            L2D_BattleInit initMsg = new L2D_BattleInit();
            initMsg.tankGroup = new List<TankGroupInit>();

            InitSelf(initMsg);
            //InitNpc(initMsg);
            SendBattleMessage(initMsg);


        }

        void InitMoudle()
        {
            PosMoudle posMoudle = new PosMoudle(this);
            logicMoudle.AddModules(posMoudle);
        }

        void InitBattleMessage()
        {
            battleManager.battleMessageCenter.Regist((int)BattleMessageID.D2L_BattleInit, this);
            battleMessageCallbacks[(int)BattleMessageID.D2L_BattleInit] = OnD2L_BattleInit;

            battleManager.battleMessageCenter.Regist((int)BattleMessageID.JOY_Press, this);
            battleMessageCallbacks[(int)BattleMessageID.JOY_Press] = OnJOY_Press;

        }

        void InitSelf(L2D_BattleInit initMsg)
        {
            Data.Player player = new Data.Player();
            player.isSelf = true;
            Data.Tank tank = new Data.Tank();
            tank.isPlayer = true;
            tank.isCaption = true;
            tank.tankType = 1000;
            tank.SetPos(new F64Vec3(0, 0, 0));
            tank.SetDir(new F64Vec3(0, 0, 1));
            tank.SetSpeed(new F64(3));
            tank.SetTurnSpeed(new F64(10));
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
            tgi.pos = tank.GetPos().ToUnityVector3();
            tgi.dir = tank.GetDir().ToUnityVector3();
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

        void OnD2L_BattleInit(UFrame.MessageCenter.Message msg)
        {
            D2L_BattleInit convMsg = msg as D2L_BattleInit;
            if (convMsg.result)
            {
                displayOK = true;
                Debug.LogError("display init ok");
            }

        }

        void OnJOY_Press(UFrame.MessageCenter.Message msg)
        {
            JOY_Press convMsg = msg as JOY_Press;
            var tankCtr = tankCtrs[convMsg.tankID];
            tankCtr.moveObject.couldMove = convMsg.couldMove;
            tankCtr.moveObject.couldTurn = convMsg.couldTurn;

            if (convMsg.couldMove && convMsg.couldTurn)
            {
                tankCtr.Turn(F64Vec3.FromUnityVector3(convMsg.dir));
            }
        }

    }
}

