//auto gen code by fanzhengyong

using System;
using System.Collections.Generic;
using UFrame.Json;

namespace Game.Json.Config
{
    public partial class CabinetParse : JsonConfigBase <Cabinet>
    {
        protected override void AddPrimaryIndex(Cabinet v)
        {
            cachePrimary[v.ID] = v;
        }

        protected override string GetConfigFileName()
        {
            return "TableData/" + "Cabinet" + ".txt";
        }
    }

}
