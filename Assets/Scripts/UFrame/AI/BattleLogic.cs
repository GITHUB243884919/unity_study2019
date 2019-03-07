using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UFrame.MessageCenter;
using Game.ToLua.Config;
using FixMath;
namespace GameName.Battle.Logic
{
    public partial class BattleLogic : IMessageExecutor
    {
        public Data.BattleLogicDataManager logicDataManager = new Data.BattleLogicDataManager();

        BattleManager battleManager;
        bool displayOK = false;

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

        public void RegistBattleMessage(BattleMessageID messageID, System.Action<UFrame.MessageCenter.Message> callback)
        {
            battleManager.battleMessageCenter.Regist((int)messageID, this);
            battleMessageCallbacks[(int)messageID] = callback;
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
            RegistBattleMessage(BattleMessageID.D2L_BattleInit, OnD2L_BattleInit);
            RegistBattleMessage(BattleMessageID.JOY_Press, OnJOY_Press);
        }

    }
}

