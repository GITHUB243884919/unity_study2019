﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class LuaMessageCenterWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(LuaMessageCenter), typeof(UnityEngine.MonoBehaviour));
		L.RegFunction("AddMessage", AddMessage);
		L.RegFunction("SendMessage", SendMessage);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("Instance", get_Instance, set_Instance);
		L.RegVar("luaFuncss", get_luaFuncss, set_luaFuncss);
		L.RegVar("luaFuncs", get_luaFuncs, set_luaFuncs);
		L.RegVar("dictTest", get_dictTest, set_dictTest);
		L.RegVar("VOID_CB___S", get_VOID_CB___S, set_VOID_CB___S);
		L.RegVar("messages", get_messages, set_messages);
		L.RegFunction("VOID_MESSAGE", LuaMessageCenter_VOID_MESSAGE);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddMessage(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			LuaMessageCenter obj = (LuaMessageCenter)ToLua.CheckObject<LuaMessageCenter>(L, 1);
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			LuaMessageCenter.VOID_MESSAGE arg1 = (LuaMessageCenter.VOID_MESSAGE)ToLua.CheckDelegate<LuaMessageCenter.VOID_MESSAGE>(L, 3);
			obj.AddMessage(arg0, arg1);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SendMessage(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2)
			{
				LuaMessageCenter obj = (LuaMessageCenter)ToLua.CheckObject<LuaMessageCenter>(L, 1);
				string arg0 = ToLua.CheckString(L, 2);
				obj.SendMessage(arg0);
				return 0;
			}
			else if (count == 3 && TypeChecker.CheckTypes<string, UnityEngine.SendMessageOptions>(L, 2))
			{
				LuaMessageCenter obj = (LuaMessageCenter)ToLua.CheckObject<LuaMessageCenter>(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				UnityEngine.SendMessageOptions arg1 = (UnityEngine.SendMessageOptions)ToLua.ToObject(L, 3);
				obj.SendMessage(arg0, arg1);
				return 0;
			}
			else if (count == 3 && TypeChecker.CheckTypes<int, LuaMessageCenter.Message>(L, 2))
			{
				LuaMessageCenter obj = (LuaMessageCenter)ToLua.CheckObject<LuaMessageCenter>(L, 1);
				int arg0 = (int)LuaDLL.lua_tonumber(L, 2);
				LuaMessageCenter.Message arg1 = (LuaMessageCenter.Message)ToLua.ToObject(L, 3);
				obj.SendMessage(arg0, arg1);
				return 0;
			}
			else if (count == 3 && TypeChecker.CheckTypes<string, object>(L, 2))
			{
				LuaMessageCenter obj = (LuaMessageCenter)ToLua.CheckObject<LuaMessageCenter>(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				object arg1 = ToLua.ToVarObject(L, 3);
				obj.SendMessage(arg0, arg1);
				return 0;
			}
			else if (count == 4)
			{
				LuaMessageCenter obj = (LuaMessageCenter)ToLua.CheckObject<LuaMessageCenter>(L, 1);
				string arg0 = ToLua.CheckString(L, 2);
				object arg1 = ToLua.ToVarObject(L, 3);
				UnityEngine.SendMessageOptions arg2 = (UnityEngine.SendMessageOptions)ToLua.CheckObject(L, 4, typeof(UnityEngine.SendMessageOptions));
				obj.SendMessage(arg0, arg1, arg2);
				return 0;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: LuaMessageCenter.SendMessage");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int op_Equality(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UnityEngine.Object arg0 = (UnityEngine.Object)ToLua.ToObject(L, 1);
			UnityEngine.Object arg1 = (UnityEngine.Object)ToLua.ToObject(L, 2);
			bool o = arg0 == arg1;
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Instance(IntPtr L)
	{
		try
		{
			ToLua.Push(L, LuaMessageCenter.Instance);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_luaFuncss(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			LuaMessageCenter obj = (LuaMessageCenter)o;
			System.Collections.Generic.Dictionary<int,LuaMessageCenter.VOID_MESSAGE> ret = obj.luaFuncss;
			ToLua.PushSealed(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index luaFuncss on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_luaFuncs(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			LuaMessageCenter obj = (LuaMessageCenter)o;
			LuaMessageCenter.VOID_MESSAGE ret = obj.luaFuncs;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index luaFuncs on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_dictTest(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			LuaMessageCenter obj = (LuaMessageCenter)o;
			System.Collections.Generic.Dictionary<int,int> ret = obj.dictTest;
			ToLua.PushSealed(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index dictTest on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_VOID_CB___S(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			LuaMessageCenter obj = (LuaMessageCenter)o;
			System.Collections.Generic.Dictionary<int,LuaMessageCenter.VOID_CB___> ret = obj.VOID_CB___S;
			ToLua.PushSealed(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index VOID_CB___S on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_messages(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			LuaMessageCenter obj = (LuaMessageCenter)o;
			System.Collections.Generic.Queue<LuaMessageCenter.Message> ret = obj.messages;
			ToLua.PushObject(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index messages on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_Instance(IntPtr L)
	{
		try
		{
			LuaMessageCenter arg0 = (LuaMessageCenter)ToLua.CheckObject<LuaMessageCenter>(L, 2);
			LuaMessageCenter.Instance = arg0;
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_luaFuncss(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			LuaMessageCenter obj = (LuaMessageCenter)o;
			System.Collections.Generic.Dictionary<int,LuaMessageCenter.VOID_MESSAGE> arg0 = (System.Collections.Generic.Dictionary<int,LuaMessageCenter.VOID_MESSAGE>)ToLua.CheckObject(L, 2, typeof(System.Collections.Generic.Dictionary<int,LuaMessageCenter.VOID_MESSAGE>));
			obj.luaFuncss = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index luaFuncss on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_luaFuncs(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			LuaMessageCenter obj = (LuaMessageCenter)o;
			LuaMessageCenter.VOID_MESSAGE arg0 = (LuaMessageCenter.VOID_MESSAGE)ToLua.CheckDelegate<LuaMessageCenter.VOID_MESSAGE>(L, 2);
			obj.luaFuncs = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index luaFuncs on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_dictTest(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			LuaMessageCenter obj = (LuaMessageCenter)o;
			System.Collections.Generic.Dictionary<int,int> arg0 = (System.Collections.Generic.Dictionary<int,int>)ToLua.CheckObject(L, 2, typeof(System.Collections.Generic.Dictionary<int,int>));
			obj.dictTest = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index dictTest on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_VOID_CB___S(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			LuaMessageCenter obj = (LuaMessageCenter)o;
			System.Collections.Generic.Dictionary<int,LuaMessageCenter.VOID_CB___> arg0 = (System.Collections.Generic.Dictionary<int,LuaMessageCenter.VOID_CB___>)ToLua.CheckObject(L, 2, typeof(System.Collections.Generic.Dictionary<int,LuaMessageCenter.VOID_CB___>));
			obj.VOID_CB___S = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index VOID_CB___S on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_messages(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			LuaMessageCenter obj = (LuaMessageCenter)o;
			System.Collections.Generic.Queue<LuaMessageCenter.Message> arg0 = (System.Collections.Generic.Queue<LuaMessageCenter.Message>)ToLua.CheckObject<System.Collections.Generic.Queue<LuaMessageCenter.Message>>(L, 2);
			obj.messages = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index messages on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LuaMessageCenter_VOID_MESSAGE(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);
			LuaFunction func = ToLua.CheckLuaFunction(L, 1);

			if (count == 1)
			{
				Delegate arg1 = DelegateTraits<LuaMessageCenter.VOID_MESSAGE>.Create(func);
				ToLua.Push(L, arg1);
			}
			else
			{
				LuaTable self = ToLua.CheckLuaTable(L, 2);
				Delegate arg1 = DelegateTraits<LuaMessageCenter.VOID_MESSAGE>.Create(func, self);
				ToLua.Push(L, arg1);
			}
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}

