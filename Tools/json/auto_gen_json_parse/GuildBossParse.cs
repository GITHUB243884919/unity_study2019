//auto gen code by fanzhengyong

using System;
using System.Collections.Generic;
using UFrame.Json;

namespace Game.Json.Config
{
    public partial class GuildBossParse : JsonConfigBase <GuildBoss>
    {
        protected override void AddPrimaryIndex(GuildBoss v)
        {
            cachePrimary[v.ID] = v;
        }

        protected override string GetConfigFileName()
        {
            return "TableData/" + "GuildBoss" + ".txt";
        }
    }

}
