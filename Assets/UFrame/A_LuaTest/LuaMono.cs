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
            LuaManager.Instance.Init();
            LuaManager.Instance.luaState.DoFile(name);
            LuaFunction func = LuaManager.Instance.luaState.GetFunction("Awake");
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
            LuaFunction func = LuaManager.Instance.luaState.GetFunction("Start");
            func.Call();
            func.Dispose();
            func = null;

        }


    }

}
