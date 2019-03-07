
//Auto Generate
using System;
using System.Collections.Generic;

namespace Game.ToLua.Config
{
    public partial class stage_infoAPI
    {
        public static stage_info Getstage_info (string dictId)
		{
			return LuaConfigManager.Instance.stage_infoParse.GetById (dictId);
		}


		public static stage_info GetDataBy_id (List<stage_info> list, int target)
		{
			foreach (var item in list){
				if (item.id == target) {
					return item;
				}
			}
			return null;
		}
        
		public static stage_info GetDataBy_id (int target)
		{
			var cacheList = LuaConfigManager.Instance.stage_infoParse.CacheList;
			foreach (var item in cacheList){
				if (item.id == target) {
					return item;
				}
			}
			return null;
		}

		public static List<stage_info> GetDataListBy_id (List<stage_info> list, int target)
		{
			List<stage_info> res = new List<stage_info> ();
			foreach (var item in list){
				if (item.id == target) {
					res.Add (item);
				}
			}
			return res;
		}

		public static List<stage_info> GetDataListBy_id (int target)
		{
			var cacheList = LuaConfigManager.Instance.stage_infoParse.CacheList;
			List<stage_info> res = new List<stage_info> ();
			foreach (var item in cacheList){
				if (item.id == target) {
					res.Add (item);
				}
			}
			return res;
		}

		public static stage_info GetDataBy_tank_group_id (List<stage_info> list, int target)
		{
			foreach (var item in list){
				if (item.tank_group_id == target) {
					return item;
				}
			}
			return null;
		}
        
		public static stage_info GetDataBy_tank_group_id (int target)
		{
			var cacheList = LuaConfigManager.Instance.stage_infoParse.CacheList;
			foreach (var item in cacheList){
				if (item.tank_group_id == target) {
					return item;
				}
			}
			return null;
		}

		public static List<stage_info> GetDataListBy_tank_group_id (List<stage_info> list, int target)
		{
			List<stage_info> res = new List<stage_info> ();
			foreach (var item in list){
				if (item.tank_group_id == target) {
					res.Add (item);
				}
			}
			return res;
		}

		public static List<stage_info> GetDataListBy_tank_group_id (int target)
		{
			var cacheList = LuaConfigManager.Instance.stage_infoParse.CacheList;
			List<stage_info> res = new List<stage_info> ();
			foreach (var item in cacheList){
				if (item.tank_group_id == target) {
					res.Add (item);
				}
			}
			return res;
		}


    }

}
