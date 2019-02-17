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
        Dictionary<int, System.Action<UFrame.MessageCenter.Message>> battleMessageCallbacks =
            new Dictionary<int, System.Action<UFrame.MessageCenter.Message>>();

        public BattleDisplay(BattleManager battleManager)
        {
            this.battleManager = battleManager;
            battleManager.battleMessageCenter.Regist((int)BattleMessageID.L2D_BattleInit, this);
            battleMessageCallbacks[(int)BattleMessageID.L2D_BattleInit] = OnL2D_BattleInit;

            battleManager.battleMessageCenter.Regist((int)BattleMessageID.L2D_TankPos, this);
            battleMessageCallbacks[(int)BattleMessageID.L2D_TankPos] = OnL2D_TankPos;
        }

        public void Execute(UFrame.MessageCenter.Message msg)
        {
            battleMessageCallbacks[msg.messageID](msg);
        }

        void OnL2D_BattleInit(UFrame.MessageCenter.Message msg)
        {

            L2D_BattleInit initMsg = msg as L2D_BattleInit;
            for (int i = 0; i < initMsg.tankGroup.Count; ++i)
            {

                tank_info ti = tank_infoAPI.GetDataBy_tank_type(initMsg.tankGroup[i].tank_type);
                GameObjectGetter tankGetter = ResHelper.LoadGameObject(ti.res_path);
                GameObject tank = tankGetter.Get();
                tank.transform.position = initMsg.tankGroup[i].pos;
                Debug.LogError(initMsg.tankGroup[i].dir);
                tank.transform.transform.LookAt(initMsg.tankGroup[i].dir);

                tanks.Add(initMsg.tankGroup[i].id, tank);

                Debug.LogError(initMsg.tankGroup[i].id + " " + initMsg.tankGroup[i].isPlayer);
                //增加跟随相机
                //if (initMsg.tankGroup[i].isSelf && initMsg.tankGroup[i].isCaptain)
                //{
                //    GameObjectGetter selfCameraGetter = ResHelper.LoadGameObject("prefabs/self_camera");
                //    GameObject selfCamera = selfCameraGetter.Get();
                //    //RPGCamera rpgCamera = selfCamera.GetComponent<RPGCamera>();
                //    //rpgCamera.UsedCamera = Camera.main;
                //    selfCamera.transform.SetParent(tank.transform);
                //}
            }

            D2L_BattleInit initRetMsg = new D2L_BattleInit();
            initRetMsg.result = true;
            battleManager.battleMessageCenter.Send(initRetMsg);

        }

        void OnL2D_TankPos(UFrame.MessageCenter.Message msg)
        {

            L2D_TankPos convMsg = msg as L2D_TankPos;
            for (int i = 0; i < convMsg.tankGroup.Count; ++i)
            {
                var tankPos = convMsg.tankGroup[i];
                int tankID = tankPos.id;
                var tank = tanks[tankID];

                tank.transform.position = tankPos.pos;
                tank.transform.LookAt(tankPos.dir + tankPos.pos);
            }
        }

        public void Tick(int deltaTimeMS)
        {

        }
    }
}

