﻿using UnityEngine;
using System.Collections;
using System;

namespace UFrame
{
    public interface IGameApp
    {
        void Awake();
        void Start();
        void Update(float s);
        void LateUpdate();
        void OnMemoryWarnning();
        void OnApplicationQuit();
        void OnApplicationPause(bool pause);
        void OnApplicationFocus(bool force);
        void Shutdown();
    }

	public abstract class AMain : MonoBehaviour
	{
		private IGameApp app = null;

        protected abstract IGameApp CreateGameApp(); 
		void Awake ()
        {
            app = CreateGameApp();
            app.Awake ();
            Application.lowMemory += app.OnMemoryWarnning;
            DontDestroyOnLoad(this);
		}

        void Start ()
		{
			app.Start ();
		}

		// Update is called once per frame
		void Update ()
		{
			//注意！！！！禁止在这里添加任何代码！！！
			if (app != null)
				app.Update (Time.deltaTime);
		}

		void LateUpdate ()
		{
            //注意！！！！禁止在这里添加任何代码！！！
            if (app != null)
				app.LateUpdate ();
		}

		void OnApplicationQuit ()
		{
			//注意！！！！禁止在这里添加任何代码！！！
			if (app != null)
				app.Shutdown ();
		}

		void OnApplicationPause (bool pause)
		{
			if (null != app) {
				app.OnApplicationPause (pause);
			}
		}

		void OnApplicationFocus (bool force)
		{
			if (null != app) {
				app.OnApplicationFocus (force);
			}
		}

//#if UNLOAD_ONGUI
//        public void OnGUI()
//        {
//            AssetLoaderManager.Instance.OnGUI();
//        }
//#endif

    }
}