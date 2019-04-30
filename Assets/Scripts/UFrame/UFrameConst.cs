﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UFrame
{
    public class UFrameConst
    {
        public static string luaDir = Application.dataPath + "/LuaFramework/Lua";                //lua逻辑代码目录
        public static string toluaDir = Application.dataPath + "/LuaFramework/ToLua/Lua";        //tolua lua文件目录

        public static string Bundle_Root_Dir = "Bundles";
        public static string Lua_Copy_Dir = "LuaTemp";
        public static string Lua_Bundle_Dir = Bundle_Root_Dir + "/lua";
        public static string Asset_Bundle_Txt_Name = "asset-bundle.txt";
        public static string Bundle_Hash_Txt_Name = "bundle-hash.txt";
        public static string GameResources_Dir = "GameResources";
        public static string Scene_Dir = GameResources_Dir + "/scenes";
        public static string Main_Scene_Path = Scene_Dir + "/main.unity";
        public static string Bundle_Extension = ".unity3d";

        public static string Game_Version_Txt_Name = "game-version";
        public static string Download_Extension = ".downtmp";
        public static string Lua_ConfigerDir = "Game/config";
        public static string Lua_MonoBehaviourDir = "Game/mono_behaviour";


    }
}

