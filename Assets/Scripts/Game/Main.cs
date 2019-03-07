﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//public class Main : MonoBehaviour
//{
//    public Button btn1;
//    public Button btn2;
//    public Button btn3;
//    // Use this for initialization
//    void Start ()
//    {
//        btn1.onClick.AddListener(() =>
//        {
//            var getter = ResHelper.LoadGameObject("prefabs/cube");
//            getter.Get();
//        });

//        btn2.onClick.AddListener(() =>
//        {
//            ResHelper.LoadScene("scenes/scene_nav2d");
//        });

//        btn3.onClick.AddListener(() =>
//        {
//            ResHelper.LoadScene("scenes/scripts_from_file");
//        });
//    }

//    // Update is called once per frame
//    void Update () {

//	}
//}
using UFrame;
using UFrame.FSM;
using UFrame.MessageCenter;

namespace Game
{
    /// <summary>
    /// 游戏总入口
    /// 如果成员变量希望跨场景访问，定义成static, 或者是一个单件
    /// </summary>
    public class GameApp : IGameApp 
    {
        FSMMachine gameFSMMachine = new FSMMachine();
        
        public void Awake()
        {
            FSMState stateLogin = new StateLogin("Login", gameFSMMachine);
            gameFSMMachine.AddState(stateLogin);
            gameFSMMachine.SetDefaultState("Login");
            FSMState stateHome = new StateHome("Home", gameFSMMachine);
            gameFSMMachine.AddState(stateHome);
        }

        public void LateUpdate()
        {
            
        }

        public void OnApplicationFocus(bool force)
        {

        }

        public void OnApplicationPause(bool pause)
        {

        }

        public void OnApplicationQuit()
        {

        }

        public void OnMemoryWarnning()
        {

        }

        public void Shutdown()
        {

        }

        public void Start()
        {

        }

        public void Update(float s)
        {
            MessageManager.GetInstance().Tick();
            gameFSMMachine.Tick((int)(s * 1000));
            
        }
    }

    public class Main : AMain
    {
        protected override IGameApp CreateGameApp()
        {
            return new GameApp();
        }
    }
}

