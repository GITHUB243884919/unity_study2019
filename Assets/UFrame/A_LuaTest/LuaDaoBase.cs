using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LuaInterface;
using System;

namespace UFrame.LUA
{
    /// <summary>
    /// todo
    /// 1.lua 和c#代码生成后copy到项目目录
    /// 2.GetLuaFileName() 里lua名称小写
    /// 3.lua初始化表要改成元表形式涉及GetConfigLuaTable
    /// 4.文件改名去Dao
    /// 5.增加额外索引c#代码生成
    /// 6.取数据API，原来的blo的c#代码生成
    /// 7.c#总配置表加载代码生成
    /// 8.ParseLuaTableData的参数key可以去掉
    /// 9.tolua dispose
    /// </summary>
    /// <typeparam name="T"></typeparam>
	public abstract class LuaDaoBase<T> where T : new()
	{
		protected List<T> cacheList;

		public List<T> CacheList {
			get {
				return cacheList;
			}
		}

		protected Dictionary<string, T> cachePrimary;

		public Dictionary<string, T> CachePrimary {
			get {
				return cachePrimary;
			}
		}

		public bool LoadOver { get; protected set; }

		public LuaDaoBase ()
		{
			LoadOver = false;
		}

		public void LoadData ()
		{
            LoadOver = false;
#if UNITY_EDITOR
            string exceptionValue = "";
#endif
            try
            {
                cacheList = new List<T>();
                //todo
                //GetLuaFileName() 里lua名称小写
                LuaTable luaTable = GetConfigLuaTable(GetLuaFileName());
                //                dictTableData.ForEach((object iKey, LuaTable _table) =>
                //                {
                //#if UNITY_EDITOR
                //                    exceptionValue = iKey + ":";

                //                    _table.ForEach((string key, string value) =>
                //                    {
                //                        exceptionValue += (key + "," + value + ";");
                //                    });
                //#endif
                //                    var model = ParseLuaTableData(iKey.ToString(), _table);
                //                    cacheList.Add(model);
                //                });

                var luaArray = luaTable.ToArrayTable();
                for (int i = 1, iMax = luaArray.Length; i <= iMax; ++i)
                {
                    var v = ParseLuaTableData("", luaArray[i] as LuaTable);
                    cacheList.Add(v);
                }
                BuildIndex();
                LoadOver = true;
            } catch (Exception e) {
#if UNITY_EDITOR
#if DEBUG && !PROFILER
				Debug.LogError ("[Dao] Last Data is \"" + exceptionValue + "\"");
#endif

#if DEBUG && !PROFILER
				Debug.LogError ("[Dao]" + this.GetType () + " LoadData Error!\n" + e.Message + "\n" + e.StackTrace);
#endif

#endif
			}
		}

		protected abstract string GetLuaFileName ();

        /// <summary>
        /// todo 
        /// key 可以去掉
        /// </summary>
        /// <param name="key"></param>
        /// <param name="tableData"></param>
        /// <returns></returns>
		protected abstract T ParseLuaTableData (string key, LuaTable tableData);

		protected LuaTable LuaTableToArrayParam (LuaTable tableData)
		{
			return tableData;
		}

		public virtual void BuildIndex ()
		{
			if (cacheList != null) {
				cachePrimary = new Dictionary<string, T> (cacheList.Count);
				foreach (var model in cacheList) {
					AddPrimaryIndex (model);
				}
				if (cachePrimary.Count != cacheList.Count) {
					throw new Exception (GetLuaFileName () + ":存在重复数据，请改数据");
				}
			}
		}

		public virtual void AddPrimaryIndex (T model)
		{

		}

		public T GetById (string id)
		{
			if (!LoadOver) {
#if DEBUG && !PROFILER
                throw new Exception("程序问题：策划表 " + GetLuaFileName () + " 尚未初始化");
#endif

                //return default (T);
			}
			if (!cachePrimary.ContainsKey (id)) {

#if DEBUG && !PROFILER    
                throw new Exception("策划问题：别的策划表想从策划表 " + GetLuaFileName() + " 找到id为 " + id + " 的数据，这是明知不可为而为之！");
#endif
                //return default (T);
			}
			return cachePrimary[id];
		}

		public T GetById (int id)
		{
			return GetById (id.ToString ());
		}

        /// <summary>
        /// todo
        /// lua初始化表要改成元表形式涉及GetConfigLuaTable
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static LuaTable GetConfigLuaTable(string tableName)
        {
            //var config = LuaManager.Instance.luaState.GetTable("config");
            //return config["'" + tableName + "'"] as LuaTable;
            return LuaManager.Instance.luaState.GetTable(tableName);
        }
    }
}
