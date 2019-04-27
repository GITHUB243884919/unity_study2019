using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LuaInterface;
//using System.IO;
//using System;
//#if UNITY_5_4_OR_NEWER
//using UnityEngine.SceneManagement;
//#endif

//using UFrame.Common;
namespace UFrame.ToLua
{
    //public class UFrameLuaClient : SingletonMono<UFrameLuaClient>
    public class UFrameLuaClient : LuaClient
    {

        protected override LuaFileUtils InitLoader()
        {
            return new LuaLoader();
        }

        protected override void LoadLuaFiles()
        {
            string testPath = Application.dataPath + "/UFrame/A_LuaTest";
            luaState.AddSearchPath(testPath);

            string configPath = Application.dataPath + "/GameResources/lua/config";
            luaState.AddSearchPath(configPath);
            base.LoadLuaFiles();
        }

        protected override void Init()
        {
            luaState = new LuaState();
            OpenLibs();
            luaState.LuaSetTop(0);
            Bind();
        }

        void Start()
        {
            InitLoader();
            LoadLuaFiles();
        }
    }
}

