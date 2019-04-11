//auto gen code by fanzhengyong

using System;
using System.Collections.Generic;
using UFrame.Json;

namespace Game.Json.Config
{
    public partial class ActivityParse : JsonConfigBase <Activity>
    {
        protected override void AddPrimaryIndex(Activity v)
        {
            cachePrimary[v.ID] = v;
        }

        protected override string GetConfigFileName()
        {
            return "TableData/" + "Activity" + ".txt";
        }
    }

}
