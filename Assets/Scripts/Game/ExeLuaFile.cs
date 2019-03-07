using UnityEngine;
using System.Collections;
using LuaInterface;
using System;
using System.IO;

using UFrame.ToLua;
//展示searchpath 使用，require 与 dofile 区别
public class ExeLuaFile : MonoBehaviour 
{
    private string strLog = "";    

	void Start () 
    {
#if UNITY_5 || UNITY_2017 || UNITY_2018		
        Application.logMessageReceived += Log;
#else
        Application.RegisterLogCallback(Log);
#endif              
    }

    void Log(string msg, string stackTrace, LogType type)
    {
        strLog += msg;
        strLog += "\r\n";
    }

    void OnGUI()
    {
        GUI.Label(new Rect(100, Screen.height / 2 - 100, 600, 400), strLog);

        if (GUI.Button(new Rect(50, 50, 120, 45), "DoFile"))
        {
            strLog = "";
            LuaManager.GetInstance().luaState.DoFile("Game/ScriptsFromFile.lua");                      
        }
        else if (GUI.Button(new Rect(50, 150, 120, 45), "Require"))
        {
            strLog = "";
            LuaManager.GetInstance().luaState.Require("Game/ScriptsFromFile");
        }
    }

    void OnApplicationQuit()
    {
#if UNITY_5 || UNITY_2017 || UNITY_2018	
        Application.logMessageReceived -= Log;
#else
        Application.RegisterLogCallback(null);
#endif 
    }
}
