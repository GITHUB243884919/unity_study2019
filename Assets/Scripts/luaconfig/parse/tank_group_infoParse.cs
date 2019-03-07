using System;
using System.Collections.Generic;
using LuaInterface;
using UFrame.ToLua;

namespace Game.ToLua.Config
{
    public class tank_group_infoParse : LuaConfigBase <tank_group_info>
    {
        public override void AddPrimaryIndex (tank_group_info v)
        {
            cachePrimary[v.id.ToString ()] = v;
        }

        protected override string GetLuaFileName ()
        {
            return "config_tank_group_info";
        }

        protected override tank_group_info ParseLuaTableData (LuaTable tableData)
        {
            tank_group_info data = new tank_group_info ();

			data.id = System.Convert.ToInt32(tableData["id"]);
			data.tank_group_id = System.Convert.ToInt32(tableData["tank_group_id"]);
			data.tank_type = System.Convert.ToInt32(tableData["tank_type"]);
			data.pos = new ArrayData (LuaTableToArrayParam (tableData["pos"] as LuaTable));
			data.dir = new ArrayData (LuaTableToArrayParam (tableData["dir"] as LuaTable));

            return data;
        }



    }

}
