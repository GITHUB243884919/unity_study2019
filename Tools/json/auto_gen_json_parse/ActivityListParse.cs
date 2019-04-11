//auto gen code by fanzhengyong

using System;
using System.Collections.Generic;
using UFrame.Json;

namespace Game.Json.Config
{
    public partial class ActivityListParse : JsonConfigBase <ActivityList>
    {
        protected override void AddPrimaryIndex(ActivityList v)
        {
            cachePrimary[v.ID] = v;
        }

        protected override string GetConfigFileName()
        {
            return "TableData/" + "ActivityList" + ".txt";
        }
    }

}
