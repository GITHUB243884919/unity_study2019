﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class LuaMessageCenter_MessageTestWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(LuaMessageCenter.MessageTest), typeof(LuaMessageCenter.Message));
		L.RegFunction("New", _CreateLuaMessageCenter_MessageTest);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("field1", get_field1, set_field1);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateLuaMessageCenter_MessageTest(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 0)
			{
				LuaMessageCenter.MessageTest obj = new LuaMessageCenter.MessageTest();
				ToLua.PushObject(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: LuaMessageCenter.MessageTest.New");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_field1(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			LuaMessageCenter.MessageTest obj = (LuaMessageCenter.MessageTest)o;
			int ret = obj.field1;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index field1 on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_field1(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			LuaMessageCenter.MessageTest obj = (LuaMessageCenter.MessageTest)o;
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			obj.field1 = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index field1 on a nil value");
		}
	}
}

