using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LuaInterface;
namespace UFrame.LUA
{
    public class LuaMono : MonoBehaviour
    {
        private void Awake()
        {
            LuaManager.GetInstance().Init();
            LuaManager.GetInstance().luaState.DoFile(name);
            LuaFunction func = LuaManager.GetInstance().luaState.GetFunction("Awake");
            func.BeginPCall();
            func.PushGeneric<GameObject>(gameObject);
            func.PCall();
            func.EndPCall();

            func.Dispose();
            func = null;
        }

        // Use this for initialization
        void Start()
        {
            //LuaFunction func = luaState.GetFunction("Start");
            LuaFunction func = LuaManager.GetInstance().luaState.GetFunction("Start");
            func.Call();
            func.Dispose();
            func = null;

        }


    }

}
