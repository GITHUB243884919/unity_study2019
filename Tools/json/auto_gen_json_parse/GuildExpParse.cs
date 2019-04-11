//auto gen code by fanzhengyong

using System;
using System.Collections.Generic;
using UFrame.Json;

namespace Game.Json.Config
{
    public partial class GuildExpParse : JsonConfigBase <GuildExp>
    {
        protected override void AddPrimaryIndex(GuildExp v)
        {
            cachePrimary[v.ID] = v;
        }

        protected override string GetConfigFileName()
        {
            return "TableData/" + "GuildExp" + ".txt";
        }
    }

}
