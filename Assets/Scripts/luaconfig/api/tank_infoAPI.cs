
//Auto Generate
using System;
using System.Collections.Generic;

namespace GameName.Lua.Config
{
    public partial class tank_infoAPI
    {
        public static tank_info Gettank_info (string dictId)
		{
			return LuaConfigManager.Instance.tank_infoParse.GetById (dictId);
		}


		public static tank_info GetDataBy_id (List<tank_info> list, int target)
		{
			foreach (var item in list){
				if (item.id == target) {
					return item;
				}
			}
			return null;
		}
        
		public static tank_info GetDataBy_id (int target)
		{
			var cacheList = LuaConfigManager.Instance.tank_infoParse.CacheList;
			foreach (var item in cacheList){
				if (item.id == target) {
					return item;
				}
			}
			return null;
		}

		public static List<tank_info> GetDataListBy_id (List<tank_info> list, int target)
		{
			List<tank_info> res = new List<tank_info> ();
			foreach (var item in list){
				if (item.id == target) {
					res.Add (item);
				}
			}
			return res;
		}

		public static List<tank_info> GetDataListBy_id (int target)
		{
			var cacheList = LuaConfigManager.Instance.tank_infoParse.CacheList;
			List<tank_info> res = new List<tank_info> ();
			foreach (var item in cacheList){
				if (item.id == target) {
					res.Add (item);
				}
			}
			return res;
		}

		public static tank_info GetDataBy_tank_type (List<tank_info> list, int target)
		{
			foreach (var item in list){
				if (item.tank_type == target) {
					return item;
				}
			}
			return null;
		}
        
		public static tank_info GetDataBy_tank_type (int target)
		{
			var cacheList = LuaConfigManager.Instance.tank_infoParse.CacheList;
			foreach (var item in cacheList){
				if (item.tank_type == target) {
					return item;
				}
			}
			return null;
		}

		public static List<tank_info> GetDataListBy_tank_type (List<tank_info> list, int target)
		{
			List<tank_info> res = new List<tank_info> ();
			foreach (var item in list){
				if (item.tank_type == target) {
					res.Add (item);
				}
			}
			return res;
		}

		public static List<tank_info> GetDataListBy_tank_type (int target)
		{
			var cacheList = LuaConfigManager.Instance.tank_infoParse.CacheList;
			List<tank_info> res = new List<tank_info> ();
			foreach (var item in cacheList){
				if (item.tank_type == target) {
					res.Add (item);
				}
			}
			return res;
		}

		public static tank_info GetDataBy_res_path (List<tank_info> list, string target)
		{
			foreach (var item in list){
				if (item.res_path == target) {
					return item;
				}
			}
			return null;
		}
        
		public static tank_info GetDataBy_res_path (string target)
		{
			var cacheList = LuaConfigManager.Instance.tank_infoParse.CacheList;
			foreach (var item in cacheList){
				if (item.res_path == target) {
					return item;
				}
			}
			return null;
		}

		public static List<tank_info> GetDataListBy_res_path (List<tank_info> list, string target)
		{
			List<tank_info> res = new List<tank_info> ();
			foreach (var item in list){
				if (item.res_path == target) {
					res.Add (item);
				}
			}
			return res;
		}

		public static List<tank_info> GetDataListBy_res_path (string target)
		{
			var cacheList = LuaConfigManager.Instance.tank_infoParse.CacheList;
			List<tank_info> res = new List<tank_info> ();
			foreach (var item in cacheList){
				if (item.res_path == target) {
					res.Add (item);
				}
			}
			return res;
		}

		public static tank_info GetDataBy_speed (List<tank_info> list, double target)
		{
			foreach (var item in list){
				if (item.speed == target) {
					return item;
				}
			}
			return null;
		}
        
		public static tank_info GetDataBy_speed (double target)
		{
			var cacheList = LuaConfigManager.Instance.tank_infoParse.CacheList;
			foreach (var item in cacheList){
				if (item.speed == target) {
					return item;
				}
			}
			return null;
		}

		public static List<tank_info> GetDataListBy_speed (List<tank_info> list, double target)
		{
			List<tank_info> res = new List<tank_info> ();
			foreach (var item in list){
				if (item.speed == target) {
					res.Add (item);
				}
			}
			return res;
		}

		public static List<tank_info> GetDataListBy_speed (double target)
		{
			var cacheList = LuaConfigManager.Instance.tank_infoParse.CacheList;
			List<tank_info> res = new List<tank_info> ();
			foreach (var item in cacheList){
				if (item.speed == target) {
					res.Add (item);
				}
			}
			return res;
		}

		public static tank_info GetDataBy_turn_speed (List<tank_info> list, double target)
		{
			foreach (var item in list){
				if (item.turn_speed == target) {
					return item;
				}
			}
			return null;
		}
        
		public static tank_info GetDataBy_turn_speed (double target)
		{
			var cacheList = LuaConfigManager.Instance.tank_infoParse.CacheList;
			foreach (var item in cacheList){
				if (item.turn_speed == target) {
					return item;
				}
			}
			return null;
		}

		public static List<tank_info> GetDataListBy_turn_speed (List<tank_info> list, double target)
		{
			List<tank_info> res = new List<tank_info> ();
			foreach (var item in list){
				if (item.turn_speed == target) {
					res.Add (item);
				}
			}
			return res;
		}

		public static List<tank_info> GetDataListBy_turn_speed (double target)
		{
			var cacheList = LuaConfigManager.Instance.tank_infoParse.CacheList;
			List<tank_info> res = new List<tank_info> ();
			foreach (var item in cacheList){
				if (item.turn_speed == target) {
					res.Add (item);
				}
			}
			return res;
		}


    }

}
