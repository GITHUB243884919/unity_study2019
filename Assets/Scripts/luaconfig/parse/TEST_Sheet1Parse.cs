using System;
using System.Collections.Generic;
using LuaInterface;
using UFrame.LUA;

namespace GameName.Lua.Config
{
    public class TEST_Sheet1Parse : LuaConfigBase <TEST_Sheet1>
    {
        public override void AddPrimaryIndex (TEST_Sheet1 v)
        {
            cachePrimary[v.id.ToString ()] = v;
        }

        protected override string GetLuaFileName ()
        {
            return "config_test_sheet1";
        }

        protected override TEST_Sheet1 ParseLuaTableData (LuaTable tableData)
        {
            TEST_Sheet1 data = new TEST_Sheet1 ();

			data.id = System.Convert.ToInt32(tableData["id"]);
			data.field1 = tableData["field1"] as string;
			data.field2 = System.Convert.ToInt32(tableData["field2"]);

            return data;
        }



    }

}
