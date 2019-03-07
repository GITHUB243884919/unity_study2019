
//Auto Generate
using System;
using System.Collections.Generic;

namespace Game.ToLua.Config
{
    public partial class Sheet1API
    {
        public static Sheet1 GetSheet1 (string dictId)
		{
			return LuaConfigManager.Instance.sheet1Parse.GetById (dictId);
		}


		public static Sheet1 GetDataBy_id (List<Sheet1> list, int target)
		{
			foreach (var item in list){
				if (item.id == target) {
					return item;
				}
			}
			return null;
		}
        
		public static Sheet1 GetDataBy_id (int target)
		{
			var cacheList = LuaConfigManager.Instance.sheet1Parse.CacheList;
			foreach (var item in cacheList){
				if (item.id == target) {
					return item;
				}
			}
			return null;
		}

		public static List<Sheet1> GetDataListBy_id (List<Sheet1> list, int target)
		{
			List<Sheet1> res = new List<Sheet1> ();
			foreach (var item in list){
				if (item.id == target) {
					res.Add (item);
				}
			}
			return res;
		}

		public static List<Sheet1> GetDataListBy_id (int target)
		{
			var cacheList = LuaConfigManager.Instance.sheet1Parse.CacheList;
			List<Sheet1> res = new List<Sheet1> ();
			foreach (var item in cacheList){
				if (item.id == target) {
					res.Add (item);
				}
			}
			return res;
		}

		public static Sheet1 GetDataBy_field1 (List<Sheet1> list, string target)
		{
			foreach (var item in list){
				if (item.field1 == target) {
					return item;
				}
			}
			return null;
		}
        
		public static Sheet1 GetDataBy_field1 (string target)
		{
			var cacheList = LuaConfigManager.Instance.sheet1Parse.CacheList;
			foreach (var item in cacheList){
				if (item.field1 == target) {
					return item;
				}
			}
			return null;
		}

		public static List<Sheet1> GetDataListBy_field1 (List<Sheet1> list, string target)
		{
			List<Sheet1> res = new List<Sheet1> ();
			foreach (var item in list){
				if (item.field1 == target) {
					res.Add (item);
				}
			}
			return res;
		}

		public static List<Sheet1> GetDataListBy_field1 (string target)
		{
			var cacheList = LuaConfigManager.Instance.sheet1Parse.CacheList;
			List<Sheet1> res = new List<Sheet1> ();
			foreach (var item in cacheList){
				if (item.field1 == target) {
					res.Add (item);
				}
			}
			return res;
		}

		public static Sheet1 GetDataBy_field2 (List<Sheet1> list, int target)
		{
			foreach (var item in list){
				if (item.field2 == target) {
					return item;
				}
			}
			return null;
		}
        
		public static Sheet1 GetDataBy_field2 (int target)
		{
			var cacheList = LuaConfigManager.Instance.sheet1Parse.CacheList;
			foreach (var item in cacheList){
				if (item.field2 == target) {
					return item;
				}
			}
			return null;
		}

		public static List<Sheet1> GetDataListBy_field2 (List<Sheet1> list, int target)
		{
			List<Sheet1> res = new List<Sheet1> ();
			foreach (var item in list){
				if (item.field2 == target) {
					res.Add (item);
				}
			}
			return res;
		}

		public static List<Sheet1> GetDataListBy_field2 (int target)
		{
			var cacheList = LuaConfigManager.Instance.sheet1Parse.CacheList;
			List<Sheet1> res = new List<Sheet1> ();
			foreach (var item in cacheList){
				if (item.field2 == target) {
					res.Add (item);
				}
			}
			return res;
		}

		public static Sheet1 GetDataBy_field4 (List<Sheet1> list, float target)
		{
			foreach (var item in list){
				if (item.field4 == target) {
					return item;
				}
			}
			return null;
		}
        
		public static Sheet1 GetDataBy_field4 (float target)
		{
			var cacheList = LuaConfigManager.Instance.sheet1Parse.CacheList;
			foreach (var item in cacheList){
				if (item.field4 == target) {
					return item;
				}
			}
			return null;
		}

		public static List<Sheet1> GetDataListBy_field4 (List<Sheet1> list, float target)
		{
			List<Sheet1> res = new List<Sheet1> ();
			foreach (var item in list){
				if (item.field4 == target) {
					res.Add (item);
				}
			}
			return res;
		}

		public static List<Sheet1> GetDataListBy_field4 (float target)
		{
			var cacheList = LuaConfigManager.Instance.sheet1Parse.CacheList;
			List<Sheet1> res = new List<Sheet1> ();
			foreach (var item in cacheList){
				if (item.field4 == target) {
					res.Add (item);
				}
			}
			return res;
		}

		public static Sheet1 GetDataBy_field5 (List<Sheet1> list, double target)
		{
			foreach (var item in list){
				if (item.field5 == target) {
					return item;
				}
			}
			return null;
		}
        
		public static Sheet1 GetDataBy_field5 (double target)
		{
			var cacheList = LuaConfigManager.Instance.sheet1Parse.CacheList;
			foreach (var item in cacheList){
				if (item.field5 == target) {
					return item;
				}
			}
			return null;
		}

		public static List<Sheet1> GetDataListBy_field5 (List<Sheet1> list, double target)
		{
			List<Sheet1> res = new List<Sheet1> ();
			foreach (var item in list){
				if (item.field5 == target) {
					res.Add (item);
				}
			}
			return res;
		}

		public static List<Sheet1> GetDataListBy_field5 (double target)
		{
			var cacheList = LuaConfigManager.Instance.sheet1Parse.CacheList;
			List<Sheet1> res = new List<Sheet1> ();
			foreach (var item in cacheList){
				if (item.field5 == target) {
					res.Add (item);
				}
			}
			return res;
		}


    }

}
