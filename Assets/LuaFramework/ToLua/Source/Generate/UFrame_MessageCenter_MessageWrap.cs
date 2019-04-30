﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class UFrame_MessageCenter_MessageWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(UFrame.MessageCenter.Message), typeof(System.Object));
		L.RegFunction("OnDeathToPool", OnDeathToPool);
		L.RegFunction("Release", Release);
		L.RegFunction("New", _CreateUFrame_MessageCenter_Message);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("messageID", get_messageID, set_messageID);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUFrame_MessageCenter_Message(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 0)
			{
				UFrame.MessageCenter.Message obj = new UFrame.MessageCenter.Message();
				ToLua.PushObject(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: UFrame.MessageCenter.Message.New");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnDeathToPool(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UFrame.MessageCenter.Message obj = (UFrame.MessageCenter.Message)ToLua.CheckObject<UFrame.MessageCenter.Message>(L, 1);
			obj.OnDeathToPool();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Release(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UFrame.MessageCenter.Message obj = (UFrame.MessageCenter.Message)ToLua.CheckObject<UFrame.MessageCenter.Message>(L, 1);
			obj.Release();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_messageID(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UFrame.MessageCenter.Message obj = (UFrame.MessageCenter.Message)o;
			int ret = obj.messageID;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index messageID on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_messageID(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UFrame.MessageCenter.Message obj = (UFrame.MessageCenter.Message)o;
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			obj.messageID = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index messageID on a nil value");
		}
	}
}

