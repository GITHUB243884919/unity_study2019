﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class UFrame_Common_Singleton_UFrame_ResourceManagement_SceneManagementWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(UFrame.Common.Singleton<UFrame.ResourceManagement.SceneManagement>), typeof(System.Object), "Singleton_UFrame_ResourceManagement_SceneManagement");
		L.RegFunction("GetInstance", GetInstance);
		L.RegFunction("DestroyInstance", DestroyInstance);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetInstance(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 0);
			UFrame.ResourceManagement.SceneManagement o = UFrame.Common.Singleton<UFrame.ResourceManagement.SceneManagement>.GetInstance();
			ToLua.PushObject(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DestroyInstance(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 0);
			UFrame.Common.Singleton<UFrame.ResourceManagement.SceneManagement>.DestroyInstance();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}

