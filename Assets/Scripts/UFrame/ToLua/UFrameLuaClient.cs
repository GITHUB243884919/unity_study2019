﻿using UnityEngine;
using System.Collections.Generic;
using LuaInterface;
using System.Collections;
using System.IO;
using System;
#if UNITY_5_4_OR_NEWER
using UnityEngine.SceneManagement;
#endif

using UFrame.Common;

namespace UFrame.ToLua
{
    public class UFrameLuaClient : SingletonMono<UFrameLuaClient>
    //public class UFrameLuaClient : LuaClient
    {
        protected LuaState luaState = null;
        protected LuaLooper loop = null;
        protected LuaFunction levelLoaded = null;

        protected bool openLuaSocket = false;
        protected bool beZbStart = false;

        protected virtual LuaFileUtils InitLoader()
        {
            return new LuaLoader();
        }

        protected virtual void LoadLuaFiles()
        {
            string testPath = Application.dataPath + "/UFrame/A_LuaTest";
            luaState.AddSearchPath(testPath);

            string configPath = Application.dataPath + "/GameResources/lua/config";
            luaState.AddSearchPath(configPath);

            string luaFrameRoot1 = Application.dataPath + "/LuaFramework/Lua";
            luaState.AddSearchPath(luaFrameRoot1);

            string luaFrameRoot2 = Application.dataPath + "/LuaFramework/ToLua/Lua";
            luaState.AddSearchPath(luaFrameRoot2);

            OnLoadFinished();
        }

        public void Init()
        {
            Logger.LogWarp.Log("Init");
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

        protected virtual void OpenLibs()
        {
            luaState.OpenLibs(LuaDLL.luaopen_pb);
            luaState.OpenLibs(LuaDLL.luaopen_struct);
            luaState.OpenLibs(LuaDLL.luaopen_lpeg);
#if UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
        luaState.OpenLibs(LuaDLL.luaopen_bit);
#endif

            if (LuaConst.openLuaSocket)
            {
                OpenLuaSocket();
            }

            if (LuaConst.openLuaDebugger)
            {
                OpenZbsDebugger();
            }
        }

        public void OpenZbsDebugger(string ip = "localhost")
        {
            if (!Directory.Exists(LuaConst.zbsDir))
            {
                Debugger.LogWarning("ZeroBraneStudio not install or LuaConst.zbsDir not right");
                return;
            }

            if (!LuaConst.openLuaSocket)
            {
                OpenLuaSocket();
            }

            if (!string.IsNullOrEmpty(LuaConst.zbsDir))
            {
                luaState.AddSearchPath(LuaConst.zbsDir);
            }

            luaState.LuaDoString(string.Format("DebugServerIp = '{0}'", ip), "@LuaClient.cs");
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int LuaOpen_Socket_Core(IntPtr L)
        {
            return LuaDLL.luaopen_socket_core(L);
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int LuaOpen_Mime_Core(IntPtr L)
        {
            return LuaDLL.luaopen_mime_core(L);
        }

        protected void OpenLuaSocket()
        {
            LuaConst.openLuaSocket = true;

            luaState.BeginPreLoad();
            luaState.RegFunction("socket.core", LuaOpen_Socket_Core);
            luaState.RegFunction("mime.core", LuaOpen_Mime_Core);
            luaState.EndPreLoad();
        }

        //cjson 比较特殊，只new了一个table，没有注册库，这里注册一下
        protected void OpenCJson()
        {
            luaState.LuaGetField(LuaIndexes.LUA_REGISTRYINDEX, "_LOADED");
            luaState.OpenLibs(LuaDLL.luaopen_cjson);
            luaState.LuaSetField(-2, "cjson");

            luaState.OpenLibs(LuaDLL.luaopen_cjson_safe);
            luaState.LuaSetField(-2, "cjson.safe");
        }

        protected virtual void CallMain()
        {
            LuaFunction main = luaState.GetFunction("Main");
            main.Call();
            main.Dispose();
            main = null;
        }

        public virtual void StartMain()
        {
            luaState.DoFile("Main.lua");
            levelLoaded = luaState.GetFunction("OnLevelWasLoaded");
            CallMain();
        }

        protected void StartLooper()
        {
            loop = gameObject.AddComponent<LuaLooper>();
            loop.luaState = luaState;
        }

        protected virtual void Bind()
        {
            LuaBinder.Bind(luaState);
            DelegateFactory.Init();
            LuaCoroutine.Register(luaState, this);
        }

        public override void Awake()
        {
            base.Awake();
            Init();
#if UNITY_5_4_OR_NEWER
            SceneManager.sceneLoaded += OnSceneLoaded;
#endif
        }

        protected virtual void OnLoadFinished()
        {
            luaState.Start();
            StartLooper();
            //StartMain();
        }

        void OnLevelLoaded(int level)
        {
            if (levelLoaded != null)
            {
                levelLoaded.BeginPCall();
                levelLoaded.Push(level);
                levelLoaded.PCall();
                levelLoaded.EndPCall();
            }

            if (luaState != null)
            {
                luaState.RefreshDelegateMap();
            }
        }

#if UNITY_5_4_OR_NEWER
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            OnLevelLoaded(scene.buildIndex);
        }
#else
    protected void OnLevelWasLoaded(int level)
    {
        OnLevelLoaded(level);
    }
#endif

        public virtual void Destroy()
        {
            if (luaState != null)
            {
#if UNITY_5_4_OR_NEWER
                SceneManager.sceneLoaded -= OnSceneLoaded;
#endif
                luaState.Call("OnApplicationQuit", false);
                DetachProfiler();
                LuaState state = luaState;
                luaState = null;

                if (levelLoaded != null)
                {
                    levelLoaded.Dispose();
                    levelLoaded = null;
                }

                if (loop != null)
                {
                    loop.Destroy();
                    loop = null;
                }

                state.Dispose();
            }
        }

        public override void OnDestroy()
        {
            Destroy();
            base.OnDestroy();
        }

        protected void OnApplicationQuit()
        {
            Destroy();
        }

        public static LuaState GetMainState()
        {
            return UFrameLuaClient.GetInstance().luaState;
        }

        public LuaLooper GetLooper()
        {
            return loop;
        }

        LuaTable profiler = null;

        public void AttachProfiler()
        {
            if (profiler == null)
            {
                profiler = luaState.Require<LuaTable>("UnityEngine.Profiler");
                profiler.Call("start", profiler);
            }
        }
        public void DetachProfiler()
        {
            if (profiler != null)
            {
                profiler.Call("stop", profiler);
                profiler.Dispose();
                LuaProfiler.Clear();
            }
        }



    }
}

