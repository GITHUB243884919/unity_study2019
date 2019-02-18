using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameName.Battle.Logic
{
    public class LogicModule
    {
        List<LogicMoudleBase> modules = new List<LogicMoudleBase>();

        public void AddModules(LogicMoudleBase moudle)
        {
            modules.Add(moudle);
        }

        public void Tick(int deltaTimeMS)
        {
            for(int i = 0; i < modules.Count; ++i)
            {
                modules[i].Tick(deltaTimeMS);
            }
        }
    }

    public abstract class LogicMoudleBase
    {
        protected BattleLogic battleLogic;
        public LogicMoudleBase(BattleLogic battleLogic)
        {
            this.battleLogic = battleLogic;
        }

        public abstract void Tick(int deltaTimeMS);
    }


    public class PosMoudle : LogicMoudleBase
    {
        public PosMoudle(BattleLogic battleLogic) : base(battleLogic) { }
        public override void Tick(int deltaTimeMS)
        {
            L2D_TankPos msg = new L2D_TankPos();

            foreach (var v in battleLogic.tankCtrs.Values)
            {
                v.Tick(deltaTimeMS);
                TankPos tankPos = new TankPos();
                tankPos.id = v.moveObject.GetID();
                tankPos.pos = v.moveObject.GetPos(1).ToUnityVector3();
                tankPos.dir = v.moveObject.GetDir(1).ToUnityVector3();

                msg.tankGroup.Add(tankPos);
            }

            battleLogic.SendBattleMessage(msg);
        }
    }
}

