using System;
using System.Collections.Generic;
using LuaInterface;
using UFrame.LUA;

namespace GameName.Lua.Config
{
    public class tank_infoParse : LuaConfigBase <tank_info>
    {
        public override void AddPrimaryIndex (tank_info v)
        {
            cachePrimary[v.id.ToString ()] = v;
        }

        protected override string GetLuaFileName ()
        {
            return "config_tank_info";
        }

        protected override tank_info ParseLuaTableData (LuaTable tableData)
        {
            tank_info data = new tank_info ();

			data.id = System.Convert.ToInt32(tableData["id"]);
			data.tank_type = System.Convert.ToInt32(tableData["tank_type"]);
			data.res_path = tableData["res_path"] as string;
			data.speed = System.Convert.ToDouble(tableData["speed"]);
			data.turn_speed = System.Convert.ToDouble(tableData["turn_speed"]);
			data.detection_len = System.Convert.ToDouble(tableData["detection_len"]);

            return data;
        }



    }

}
