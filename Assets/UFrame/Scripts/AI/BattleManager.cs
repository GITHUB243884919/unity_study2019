﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UFrame.MessageCenter;
using UFrame.LUA;
using GameName.Lua.Config;

namespace GameName.Battle
{
    public class BattleManager : MonoBehaviour
    {

        public DirectMessageCenter battleMessageCenter;
        public Logic.BattleLogic battleLogic;
        public Display.BattleDisplay battleDisplay;

        // Use this for initialization
        void Start()
        {
            Application.targetFrameRate = 30;
            UFrame.ResourceManagement.ResourceManager.GetInstance().Init();
            LuaManager.GetInstance().Init();
            LuaManager.GetInstance().luaState.DoFile("init_lua_config");
            LuaConfigManager.Instance.Init();
            battleMessageCenter = new DirectMessageCenter();
            battleLogic = new Logic.BattleLogic(this);
            battleDisplay = new Display.BattleDisplay(this);

            battleLogic.InitBattleStage();
        }



        // Update is called once per frame
        void Update()
        {
            battleMessageCenter.Tick();
            battleLogic.Tick((int)(Time.deltaTime * 1000));
            battleDisplay.Tick((int)(Time.deltaTime * 1000));
        }
        [SerializeField]
        public ETCJoystick joy;
        void OnEnable()
        {
            
            joy.OnPressUp.AddListener(OnPress);
            joy.OnPressDown.AddListener(OnPress);
            joy.OnPressLeft.AddListener(OnPress);
            joy.OnPressRight.AddListener(OnPress);
            //joy.OnDownUp.AddListener(OnPress);
            //joy.OnDownDown.AddListener(OnPress);
            //joy.OnDownLeft.AddListener(OnPress);
            //joy.OnDownRight.AddListener(OnPress);
            //joy.onTouchStart.AddListener(OnTouchStart);
            joy.onTouchUp.AddListener(OnTouchUp);

        }


        public void OnPress()
        {
            //Debug.LogError("OnPress");
            float h = joy.axisX.axisValue;
            float v = joy.axisY.axisValue;

            if (h != 0 || v != 0)
            {
                //Debug.LogError(h + " " + v);
                JOY_Press joyPress = new JOY_Press();
                joyPress.tankID = 0;
                joyPress.couldMove = true;
                joyPress.couldTurn = true;
                Vector3 dir = new Vector3(h, 0, v);
                dir.Normalize();
                joyPress.dir = dir;
                battleMessageCenter.Send(joyPress);
                
            }
        }

        public void OnTouchUp()
        {
            
            float h = joy.axisX.axisValue;
            float v = joy.axisY.axisValue;
            Debug.LogError("OnTouchUp" + h + " " + v);
            JOY_Press joyPress = new JOY_Press();
            joyPress.tankID = 0;
            joyPress.couldMove = false;
            joyPress.couldTurn = false;

            battleMessageCenter.Send(joyPress);


            //Vector3 A = new Vector3(0, 0, 1);
            //Vector3 B = new Vector3(-1, 0, 0);
            //float dot = Vector3.Dot(A, B);
            //Debug.LogError("Dot = " + dot);
            //float acos = Mathf.Acos(dot);
            //Debug.LogError("acos = " + acos);
            //float Rad2Deg = Mathf.Rad2Deg * acos;
            //Debug.LogError("Rad2Deg = " + Rad2Deg);
            //Vector3 cross = Vector3.Cross(A, B);
            //Debug.LogError("angle = " + Vector3.Angle(A, B));
            //Debug.LogError("cross = " + cross);

        }

    }

}
