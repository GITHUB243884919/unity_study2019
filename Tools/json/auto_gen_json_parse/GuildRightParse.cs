//auto gen code by fanzhengyong

using System;
using System.Collections.Generic;
using UFrame.Json;

namespace Game.Json.Config
{
    public partial class GuildRightParse : JsonConfigBase <GuildRight>
    {
        protected override void AddPrimaryIndex(GuildRight v)
        {
            cachePrimary[v.ID] = v;
        }

        protected override string GetConfigFileName()
        {
            return "TableData/" + "GuildRight" + ".txt";
        }
    }

}
