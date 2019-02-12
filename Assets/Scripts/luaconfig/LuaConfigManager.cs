using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace GameName.Lua.Config
{
    public class LuaConfigManager
    {
    	public static int DictFileNum;

        private static LuaConfigManager instance = new LuaConfigManager ();

        public static LuaConfigManager Instance {
            get {
                return instance;
            }
        }

        private LuaConfigManager ()
        {
        }

        public int fileCount;
        public int initedFileCount { get; private set; }

        public void Init ()
        {

			fileCount = 2;
			initedFileCount = 0;

			tEST_Sheet1Parse.LoadData ();
			initedFileCount ++;

			sheet1Parse.LoadData ();
			initedFileCount ++;

        }

		public IEnumerator InitAsyc ()
        {
        	DateTime a = DateTime.Now;

			fileCount = 2;

			tEST_Sheet1Parse.LoadData ();
			initedFileCount ++;
			if ((DateTime.Now - a).TotalMilliseconds > 20) {
				yield return new WaitForEndOfFrame ();
				a = DateTime.Now;
			}

			sheet1Parse.LoadData ();
			initedFileCount ++;
			if ((DateTime.Now - a).TotalMilliseconds > 20) {
				yield return new WaitForEndOfFrame ();
				a = DateTime.Now;
			}

        }


		public TEST_Sheet1Parse tEST_Sheet1Parse = new TEST_Sheet1Parse();

		public Sheet1Parse sheet1Parse = new Sheet1Parse();

    }
}
