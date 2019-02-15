using System;
using System.Collections.Generic;
using LuaInterface;
using UFrame.LUA;

namespace GameName.Lua.Config
{
    public class stage_infoParse : LuaConfigBase <stage_info>
    {
        public override void AddPrimaryIndex (stage_info v)
        {
            cachePrimary[v.id.ToString ()] = v;
        }

        protected override string GetLuaFileName ()
        {
            return "config_stage_info";
        }

        protected override stage_info ParseLuaTableData (LuaTable tableData)
        {
            stage_info data = new stage_info ();

			data.id = System.Convert.ToInt32(tableData["id"]);
			data.tank_group_id = System.Convert.ToInt32(tableData["tank_group_id"]);

            return data;
        }



    }

}
