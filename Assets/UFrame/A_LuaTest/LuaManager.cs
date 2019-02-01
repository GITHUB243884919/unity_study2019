using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LuaInterface;

namespace UFrame.LUA
{
    public class LuaManager
    {
        private static LuaManager _instance;

        public static LuaManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LuaManager();
                }
                return _instance;
            }
        }

        public LuaState luaState { get; private set; }
        private bool bInit = false;
        public void Init()
        {
            if (bInit)
            {
                return;
            }
            //InitLoader();
            luaState = new LuaState();
            OpenLibs();
            luaState.LuaSetTop(0);
            Bind();

            string fullPath = Application.dataPath + "/UFrame/A_LuaTest";
            luaState.AddSearchPath(fullPath);

            luaState.Start();

            bInit = true;
        }


        public void Release()
        {
            luaState.CheckTop();
            luaState.Dispose();
            luaState = null;
        }

        private void OpenLibs()
        {
            luaState.OpenLibs(LuaDLL.luaopen_pb);
            luaState.OpenLibs(LuaDLL.luaopen_struct);
            luaState.OpenLibs(LuaDLL.luaopen_lpeg);
#if UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
            luaState.OpenLibs(LuaDLL.luaopen_bit);
#endif

            if (LuaConst.openLuaSocket)
            {
                //OpenLuaSocket();
            }

            if (LuaConst.openLuaDebugger)
            {
                //OpenZbsDebugger();
            }
        }



        private void Bind()
        {
            LuaBinder.Bind(luaState);
            DelegateFactory.Init();
            //LuaCoroutine.Register(luaState, this);
        }
    }
}
