//auto gen code by fanzhengyong

using System;
using System.Collections.Generic;
using UFrame.Json;

namespace Game.Json.Config
{
    public partial class GuildContributeParse : JsonConfigBase <GuildContribute>
    {
        protected override void AddPrimaryIndex(GuildContribute v)
        {
            cachePrimary[v.ID] = v;
        }

        protected override string GetConfigFileName()
        {
            return "TableData/" + "GuildContribute" + ".txt";
        }
    }

}
