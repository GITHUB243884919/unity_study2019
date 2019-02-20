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
        BattleManager battleManager;
        bool displayOK = false;

        Dictionary<int, System.Action<UFrame.MessageCenter.Message>> battleMessageCallbacks =
            new Dictionary<int, System.Action<UFrame.MessageCenter.Message>>();

        //坦克控制
        public Dictionary<int, UFrame.AI.SimpleMoveObjectCtr> tankCtrs = new Dictionary<int, UFrame.AI.SimpleMoveObjectCtr>();

        //障碍物
        public List<Data.Avoidance> avoidances = new List<Data.Avoidance>();

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


            InitBattleField();


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

