using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UFrame.MessageCenter;
using UFrame.ResourceManagement;
using Game.ToLua.Config;

namespace GameName.Battle.Display
{
    public class Tank
    {
        public int ID;
        public float dectLen;
        public float dectWidth;
        public GameObject go;
    }
    public class BattleDisplay : IMessageExecutor
    {
        BattleManager battleManager;

        Dictionary<int, Tank> tanks = new Dictionary<int, Tank>();
        Dictionary<int, System.Action<UFrame.MessageCenter.Message>> battleMessageCallbacks =
            new Dictionary<int, System.Action<UFrame.MessageCenter.Message>>();

        Vector3 wallBegin = new Vector3(10, 0, 5);
        Vector3 wallEnd = new Vector3(10, 0, 10);
        Vector3 wallDir;
        public BattleDisplay(BattleManager battleManager)
        {
            this.battleManager = battleManager;
            battleManager.battleMessageCenter.Regist((int)BattleMessageID.L2D_BattleInit, this);
            battleMessageCallbacks[(int)BattleMessageID.L2D_BattleInit] = OnL2D_BattleInit;

            battleManager.battleMessageCenter.Regist((int)BattleMessageID.L2D_TankPos, this);
            battleMessageCallbacks[(int)BattleMessageID.L2D_TankPos] = OnL2D_TankPos;

            wallDir = (wallEnd - wallBegin).normalized;
            Debug.LogError("wallDir = " + wallDir);
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
                GameObject tankGo = tankGetter.Get();
                tankGo.transform.position = initMsg.tankGroup[i].pos;
                Debug.LogError(initMsg.tankGroup[i].dir);
                tankGo.transform.transform.LookAt(initMsg.tankGroup[i].dir);
                var tank = new Tank();
                tank.go = tankGo;
                tank.ID = initMsg.tankGroup[i].id;
                tank.dectLen = initMsg.tankGroup[i].detectionLen;
                tank.dectWidth = initMsg.tankGroup[i].detectionWidth;
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

            for (int i = 0; i < initMsg.avoidances.Count; ++i)
            {

                //tank_info ti = tank_infoAPI.GetDataBy_tank_type(initMsg.tankGroup[i].tank_type);
                GameObjectGetter getter = ResHelper.LoadGameObject("prefabs/avoidance3");
                GameObject av = getter.Get();
                av.transform.position = initMsg.avoidances[i].pos;
                //av.transform.localScale *= (initMsg.avoidances[i].rad);
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

                tank.go.transform.position = tankPos.pos;
                //tank.go.transform.LookAt(tankPos.dir + tankPos.pos);
                //tank.go.transform.LookAt(tankPos.dir);
                //tank.go.transform.LookAt(tankPos.pos);
                tank.go.transform.forward = tankPos.dir;
            }
        }


        public void Tick(int deltaTimeMS)
        {
            Debug.DrawLine(wallBegin, wallEnd, Color.red);

            foreach(var kv in tanks)
            {
                Vector3 o = kv.Value.go.transform.position;
                Vector3 e = o + kv.Value.go.transform.forward * kv.Value.dectLen;
                Debug.DrawLine(o, e, Color.blue);

                //Vector3 o1 = o + new Vector3(kv.Value.dectWidth, 0, 0);
                //Vector3 e1 = e + new Vector3(kv.Value.dectWidth, 0, 0);
                //Debug.DrawLine(o1, e1, Color.red);

                //Vector3 o2 = o + new Vector3(-kv.Value.dectWidth, 0, 0);
                //Vector3 e2 = e + new Vector3(-kv.Value.dectWidth, 0, 0);
                //Debug.DrawLine(o2, e2, Color.green);

            }
        }
    }
}

