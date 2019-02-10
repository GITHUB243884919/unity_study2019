﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LuaInterface;
using UFrame.LUA;

public class ReadConfig : MonoBehaviour {

    //public void PrintTable(string name)
    //{
    //    LuaTable table = GetTable(name);
    //    LuaDictTable dict = table.ToDictTable();
    //    table.Dispose();
    //    var iter2 = dict.GetEnumerator();

    //    while (iter2.MoveNext())
    //    {
    //        Debugger.Log("map item, k,v is {0}:{1}", iter2.Current.Key, iter2.Current.Value);
    //    }

    //    iter2.Dispose();
    //    dict.Dispose();
    //}

    void TestBattleConfig()
    {
        LuaTable table = LuaManager.Instance.luaState.GetTable("config_battle_config");
        LuaTable table2 = table[1] as LuaTable;
        //Debug.LogError(table2["id"]);
        //Debug.LogError(table2["diff_name"]);
        
        LuaDictTable dict = table.ToDictTable();
        foreach (var kv in dict)
        {
            Debug.LogError(kv.Key);
            LuaTable table3 = kv.Value as LuaTable;
            LuaDictTable dict2 = table3.ToDictTable();
            foreach (var kv2 in dict2)
            {
                Debug.LogError(kv2.Key + " " + kv2.Value);
            }
        }
    }
    // Use this for initialization
    void Start () {
        LuaManager.Instance.Init();
        LuaManager.Instance.luaState.DoFile("init_config");

        //TestBattleConfig();

        LuaTable tsheet = LuaManager.Instance.luaState.GetTable("config_sheet1");
        
        LuaDictTable dsheet = tsheet.ToDictTable();
        foreach (var kv in dsheet)
        {
            //Debug.LogError("dsheet  " + kv.Key);
            
            LuaTable tfield3 = (kv.Value as LuaTable)["field3"] as LuaTable;
            LuaArrayTable afield3 = tfield3.ToArrayTable();
            //afield3.ForEach((obj) => { Debug.LogError("obj " + System.Convert.ToInt32(obj)); });
            for (int i = 1; i <= afield3.Length; i++)
            {
                Debug.LogError("dsheet  " + kv.Key + " obj " + System.Convert.ToInt32(afield3[i]));
            }

            ArrayData ad = new ArrayData(tfield3);
            Debug.LogError("ad " + ad.GetString(0));

        }

    }

    // Update is called once per frame
    void Update () {
		
	}
}