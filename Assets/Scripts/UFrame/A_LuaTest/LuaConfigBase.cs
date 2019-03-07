using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LuaInterface;
using System;

namespace UFrame.ToLua
{
    /// <summary>
    /// todo
    /// *1.lua 和c#代码生成后copy到项目目录
    /// *2.GetLuaFileName() 里lua名称小写
    /// *3.lua初始化表要改成元表形式涉及GetConfigLuaTable
    /// *4.文件改名去Dao
    /// 5.增加额外索引c#代码生成
    /// *6.取数据API，原来的blo的c#代码生成
    /// *7.c#总配置表加载代码生成
    /// *8.ParseLuaTableData的参数key可以去掉
    /// 9.tolua dispose
    /// *10.parse代码去掉一个namespace
    /// </summary>
    /// <typeparam name="T"></typeparam>
	public abstract class LuaConfigBase<T> where T : new()
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

		public LuaConfigBase ()
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

                LuaTable luaTable = GetConfigLuaTable(GetLuaFileName());
                var luaArray = luaTable.ToArrayTable();
                for (int i = 1, iMax = luaArray.Length; i <= iMax; ++i)
                {
                    var v = ParseLuaTableData(luaArray[i] as LuaTable);
                    cacheList.Add(v);
                }
                BuildIndex();
                LoadOver = true;
            } catch (Exception e) {
#if UNITY_EDITOR
#if DEBUG && !PROFILER
				Debug.LogError ("[LuaConfigBase] Last Data is \"" + exceptionValue + "\"");
#endif

#if DEBUG && !PROFILER
				Debug.LogError ("[LuaConfigBase]" + this.GetType () + " LoadData Error!\n" + e.Message + "\n" + e.StackTrace);
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
		protected abstract T ParseLuaTableData (LuaTable tableData);

		protected LuaTable LuaTableToArrayParam (LuaTable tableData)
		{
			return tableData;
		}

		public virtual void BuildIndex ()
		{
			if (cacheList != null) {
				cachePrimary = new Dictionary<string, T> (cacheList.Count);
				foreach (var v in cacheList) {
					AddPrimaryIndex (v);
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
                throw new Exception("策划表 " + GetLuaFileName () + " 尚未初始化");
#endif

                //return default (T);
			}
			if (!cachePrimary.ContainsKey (id)) {

#if DEBUG && !PROFILER    
                throw new Exception("从策划表 " + GetLuaFileName() + " 找到id为 " + id + " 的数据，没有！");
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
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static LuaTable GetConfigLuaTable(string tableName)
        {
            var config = LuaManager.GetInstance().luaState.GetTable(tableName);
            return config;
        }
    }
}
