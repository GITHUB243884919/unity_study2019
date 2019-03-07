
//Auto Generate
using System;
using System.Collections.Generic;

namespace Game.ToLua.Config
{
    public partial class TEST_Sheet1API
    {
        public static TEST_Sheet1 GetTEST_Sheet1 (string dictId)
		{
			return LuaConfigManager.Instance.tEST_Sheet1Parse.GetById (dictId);
		}


		public static TEST_Sheet1 GetDataBy_id (List<TEST_Sheet1> list, int target)
		{
			foreach (var item in list){
				if (item.id == target) {
					return item;
				}
			}
			return null;
		}
        
		public static TEST_Sheet1 GetDataBy_id (int target)
		{
			var cacheList = LuaConfigManager.Instance.tEST_Sheet1Parse.CacheList;
			foreach (var item in cacheList){
				if (item.id == target) {
					return item;
				}
			}
			return null;
		}

		public static List<TEST_Sheet1> GetDataListBy_id (List<TEST_Sheet1> list, int target)
		{
			List<TEST_Sheet1> res = new List<TEST_Sheet1> ();
			foreach (var item in list){
				if (item.id == target) {
					res.Add (item);
				}
			}
			return res;
		}

		public static List<TEST_Sheet1> GetDataListBy_id (int target)
		{
			var cacheList = LuaConfigManager.Instance.tEST_Sheet1Parse.CacheList;
			List<TEST_Sheet1> res = new List<TEST_Sheet1> ();
			foreach (var item in cacheList){
				if (item.id == target) {
					res.Add (item);
				}
			}
			return res;
		}

		public static TEST_Sheet1 GetDataBy_field1 (List<TEST_Sheet1> list, string target)
		{
			foreach (var item in list){
				if (item.field1 == target) {
					return item;
				}
			}
			return null;
		}
        
		public static TEST_Sheet1 GetDataBy_field1 (string target)
		{
			var cacheList = LuaConfigManager.Instance.tEST_Sheet1Parse.CacheList;
			foreach (var item in cacheList){
				if (item.field1 == target) {
					return item;
				}
			}
			return null;
		}

		public static List<TEST_Sheet1> GetDataListBy_field1 (List<TEST_Sheet1> list, string target)
		{
			List<TEST_Sheet1> res = new List<TEST_Sheet1> ();
			foreach (var item in list){
				if (item.field1 == target) {
					res.Add (item);
				}
			}
			return res;
		}

		public static List<TEST_Sheet1> GetDataListBy_field1 (string target)
		{
			var cacheList = LuaConfigManager.Instance.tEST_Sheet1Parse.CacheList;
			List<TEST_Sheet1> res = new List<TEST_Sheet1> ();
			foreach (var item in cacheList){
				if (item.field1 == target) {
					res.Add (item);
				}
			}
			return res;
		}

		public static TEST_Sheet1 GetDataBy_field2 (List<TEST_Sheet1> list, int target)
		{
			foreach (var item in list){
				if (item.field2 == target) {
					return item;
				}
			}
			return null;
		}
        
		public static TEST_Sheet1 GetDataBy_field2 (int target)
		{
			var cacheList = LuaConfigManager.Instance.tEST_Sheet1Parse.CacheList;
			foreach (var item in cacheList){
				if (item.field2 == target) {
					return item;
				}
			}
			return null;
		}

		public static List<TEST_Sheet1> GetDataListBy_field2 (List<TEST_Sheet1> list, int target)
		{
			List<TEST_Sheet1> res = new List<TEST_Sheet1> ();
			foreach (var item in list){
				if (item.field2 == target) {
					res.Add (item);
				}
			}
			return res;
		}

		public static List<TEST_Sheet1> GetDataListBy_field2 (int target)
		{
			var cacheList = LuaConfigManager.Instance.tEST_Sheet1Parse.CacheList;
			List<TEST_Sheet1> res = new List<TEST_Sheet1> ();
			foreach (var item in cacheList){
				if (item.field2 == target) {
					res.Add (item);
				}
			}
			return res;
		}


    }

}
