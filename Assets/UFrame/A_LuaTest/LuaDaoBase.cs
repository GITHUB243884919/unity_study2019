using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LuaInterface;
using System;

namespace UFrame.LUA
{
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
                //				cacheList = new List<T> ();
                //                LuaTable dictTableData = LuaUtil.GetConfigLuaTable (GetLuaFileName ());
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

                //                BuildIndex ();
                //				LoadOver = true;
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
                Debug.LogError ("程序问题：策划表 " + GetLuaFileName () + " 尚未初始化");
#endif

                return default (T);
			}
			if (!cachePrimary.ContainsKey (id)) {

#if DEBUG && !PROFILER    
                throw new Exception("策划问题：别的策划表想从策划表 " + GetLuaFileName() + " 找到id为 " + id + " 的数据，这是明知不可为而为之！");
#endif
                return default (T);
			}
			return cachePrimary[id];
		}

		public T GetById (int id)
		{
			return GetById (id.ToString ());
		}
	}
}
