using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UFrame.MessageCenter;
using UFrame.ResourceManagement;
using GameName.Lua.Config;

namespace GameName.Battle.Display
{
    public class BattleDisplay : IMessageExecutor
    {
        BattleManager battleManager;

        Dictionary<int, GameObject> tanks = new Dictionary<int, GameObject>();

        public BattleDisplay(BattleManager battleManager)
        {
            this.battleManager = battleManager;
            battleManager.battleMessageCenter.Regist((int)BattleMessageID.L2D_BattleInit, this);
            battleManager.battleMessageCenter.Regist((int)BattleMessageID.L2D_TankPos, this);
        }

        public void Execute(UFrame.MessageCenter.Message msg)
        {
            TankInit(msg);
            TankPos(msg);

        }

        void TankInit(UFrame.MessageCenter.Message msg)
        {
            if (msg.messageID == (int)BattleMessageID.L2D_BattleInit)
            {
                L2D_BattleInit initMsg = msg as L2D_BattleInit;
                for (int i = 0; i < initMsg.tankGroup.Count; ++i)
                {

                    tank_info ti = tank_infoAPI.GetDataBy_tank_type(initMsg.tankGroup[i].tank_type);
                    GameObjectGetter tankGetter = ResHelper.LoadGameObject(ti.res_path);
                    GameObject tank = tankGetter.Get();
                    tank.transform.position = initMsg.tankGroup[i].pos;
                    tank.transform.transform.LookAt(initMsg.tankGroup[i].dir);
                    tanks.Add(initMsg.tankGroup[i].id, tank);
                }

                D2L_BattleInit initRetMsg = new D2L_BattleInit();
                initRetMsg.result = true;
                battleManager.battleMessageCenter.Send(initRetMsg);
            }
        }

        void TankPos(UFrame.MessageCenter.Message msg)
        {

            if (msg.messageID == (int)BattleMessageID.L2D_TankPos)
            {
                L2D_TankPos convMsg = msg as L2D_TankPos;
                for (int i = 0; i < convMsg.tankGroup.Count; ++i)
                {
                    tanks[convMsg.tankGroup[i].id].transform.position = convMsg.tankGroup[i].pos;

                }

            }
        }

        public void Tick(int deltaTimeMS)
        {

        }
    }
}

