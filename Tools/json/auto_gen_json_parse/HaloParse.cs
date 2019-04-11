//auto gen code by fanzhengyong

using System;
using System.Collections.Generic;
using UFrame.Json;

namespace Game.Json.Config
{
    public partial class HaloParse : JsonConfigBase <Halo>
    {
        protected override void AddPrimaryIndex(Halo v)
        {
            cachePrimary[v.ID] = v;
        }

        protected override string GetConfigFileName()
        {
            return "TableData/" + "Halo" + ".txt";
        }
    }

}
