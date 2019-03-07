using System;
using System.Collections.Generic;
using LuaInterface;
using UFrame.ToLua;

namespace Game.ToLua.Config
{
    public class Sheet1Parse : LuaConfigBase <Sheet1>
    {
        public override void AddPrimaryIndex (Sheet1 v)
        {
            cachePrimary[v.id.ToString ()] = v;
        }

        protected override string GetLuaFileName ()
        {
            return "config_sheet1";
        }

        protected override Sheet1 ParseLuaTableData (LuaTable tableData)
        {
            Sheet1 data = new Sheet1 ();

			data.id = System.Convert.ToInt32(tableData["id"]);
			data.field1 = tableData["field1"] as string;
			data.field2 = System.Convert.ToInt32(tableData["field2"]);
			data.field3 = new ArrayData (LuaTableToArrayParam (tableData["field3"] as LuaTable));
			data.field4 = System.Convert.ToSingle(tableData["field4"]);
			data.field5 = System.Convert.ToDouble(tableData["field5"]);

            return data;
        }



    }

}
