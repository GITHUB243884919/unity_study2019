using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Game.ToLua.Config
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

			fileCount = 5;
			initedFileCount = 0;

			tEST_Sheet1Parse.LoadData ();
			initedFileCount ++;

			sheet1Parse.LoadData ();
			initedFileCount ++;

			stage_infoParse.LoadData ();
			initedFileCount ++;

			tank_group_infoParse.LoadData ();
			initedFileCount ++;

			tank_infoParse.LoadData ();
			initedFileCount ++;

        }

		public IEnumerator InitAsyc ()
        {
        	DateTime a = DateTime.Now;

			fileCount = 5;

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

			stage_infoParse.LoadData ();
			initedFileCount ++;
			if ((DateTime.Now - a).TotalMilliseconds > 20) {
				yield return new WaitForEndOfFrame ();
				a = DateTime.Now;
			}

			tank_group_infoParse.LoadData ();
			initedFileCount ++;
			if ((DateTime.Now - a).TotalMilliseconds > 20) {
				yield return new WaitForEndOfFrame ();
				a = DateTime.Now;
			}

			tank_infoParse.LoadData ();
			initedFileCount ++;
			if ((DateTime.Now - a).TotalMilliseconds > 20) {
				yield return new WaitForEndOfFrame ();
				a = DateTime.Now;
			}

        }


		public TEST_Sheet1Parse tEST_Sheet1Parse = new TEST_Sheet1Parse();

		public Sheet1Parse sheet1Parse = new Sheet1Parse();

		public stage_infoParse stage_infoParse = new stage_infoParse();

		public tank_group_infoParse tank_group_infoParse = new tank_group_infoParse();

		public tank_infoParse tank_infoParse = new tank_infoParse();

    }
}
