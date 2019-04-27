using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LuaInterface;
using System.IO;

namespace UFrame.ToLua
{
    public class LuaMonoBehaviour : MonoBehaviour
    {
        string awakeFunc = "";
        string startFunc = "";
        string ondestroyFunc = "";

        void Awake()
        {
            string luaPath = Path.Combine(UFrameConst.Lua_MonoBehaviourDir, gameObject.name);
            luaPath = luaPath.Replace("\\", "/");
            LuaManager.GetInstance().luaState.DoFile(luaPath);

            awakeFunc = gameObject.name + ".Awake";
            startFunc = gameObject.name + ".Start";
            ondestroyFunc = gameObject.name + ".OnDestroy";
            LuaFunction func = LuaManager.GetInstance().luaState.GetFunction(awakeFunc);
            func.Call<GameObject>(gameObject);
            func.Dispose();

        }
        // Use this for initialization
        void Start()
        {
            LuaFunction func = LuaManager.GetInstance().luaState.GetFunction(startFunc);
            func.Call();
            func.Dispose();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnDestroy()
        {
            LuaFunction func = LuaManager.GetInstance().luaState.GetFunction(ondestroyFunc);
            func.Call();
            func.Dispose();
        }
    }
}

