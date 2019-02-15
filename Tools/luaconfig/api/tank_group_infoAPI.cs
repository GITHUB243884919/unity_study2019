
//Auto Generate
using System;
using System.Collections.Generic;

namespace GameName.Lua.Config
{
    public partial class tank_group_infoAPI
    {
        public static tank_group_info Gettank_group_info (string dictId)
		{
			return LuaConfigManager.Instance.tank_group_infoParse.GetById (dictId);
		}


		public static tank_group_info GetDataBy_id (List<tank_group_info> list, int target)
		{
			foreach (var item in list){
				if (item.id == target) {
					return item;
				}
			}
			return null;
		}
        
		public static tank_group_info GetDataBy_id (int target)
		{
			var cacheList = LuaConfigManager.Instance.tank_group_infoParse.CacheList;
			foreach (var item in cacheList){
				if (item.id == target) {
					return item;
				}
			}
			return null;
		}

		public static List<tank_group_info> GetDataListBy_id (List<tank_group_info> list, int target)
		{
			List<tank_group_info> res = new List<tank_group_info> ();
			foreach (var item in list){
				if (item.id == target) {
					res.Add (item);
				}
			}
			return res;
		}

		public static List<tank_group_info> GetDataListBy_id (int target)
		{
			var cacheList = LuaConfigManager.Instance.tank_group_infoParse.CacheList;
			List<tank_group_info> res = new List<tank_group_info> ();
			foreach (var item in cacheList){
				if (item.id == target) {
					res.Add (item);
				}
			}
			return res;
		}

		public static tank_group_info GetDataBy_tank_group_id (List<tank_group_info> list, int target)
		{
			foreach (var item in list){
				if (item.tank_group_id == target) {
					return item;
				}
			}
			return null;
		}
        
		public static tank_group_info GetDataBy_tank_group_id (int target)
		{
			var cacheList = LuaConfigManager.Instance.tank_group_infoParse.CacheList;
			foreach (var item in cacheList){
				if (item.tank_group_id == target) {
					return item;
				}
			}
			return null;
		}

		public static List<tank_group_info> GetDataListBy_tank_group_id (List<tank_group_info> list, int target)
		{
			List<tank_group_info> res = new List<tank_group_info> ();
			foreach (var item in list){
				if (item.tank_group_id == target) {
					res.Add (item);
				}
			}
			return res;
		}

		public static List<tank_group_info> GetDataListBy_tank_group_id (int target)
		{
			var cacheList = LuaConfigManager.Instance.tank_group_infoParse.CacheList;
			List<tank_group_info> res = new List<tank_group_info> ();
			foreach (var item in cacheList){
				if (item.tank_group_id == target) {
					res.Add (item);
				}
			}
			return res;
		}

		public static tank_group_info GetDataBy_tank_type (List<tank_group_info> list, int target)
		{
			foreach (var item in list){
				if (item.tank_type == target) {
					return item;
				}
			}
			return null;
		}
        
		public static tank_group_info GetDataBy_tank_type (int target)
		{
			var cacheList = LuaConfigManager.Instance.tank_group_infoParse.CacheList;
			foreach (var item in cacheList){
				if (item.tank_type == target) {
					return item;
				}
			}
			return null;
		}

		public static List<tank_group_info> GetDataListBy_tank_type (List<tank_group_info> list, int target)
		{
			List<tank_group_info> res = new List<tank_group_info> ();
			foreach (var item in list){
				if (item.tank_type == target) {
					res.Add (item);
				}
			}
			return res;
		}

		public static List<tank_group_info> GetDataListBy_tank_type (int target)
		{
			var cacheList = LuaConfigManager.Instance.tank_group_infoParse.CacheList;
			List<tank_group_info> res = new List<tank_group_info> ();
			foreach (var item in cacheList){
				if (item.tank_type == target) {
					res.Add (item);
				}
			}
			return res;
		}


    }

}
