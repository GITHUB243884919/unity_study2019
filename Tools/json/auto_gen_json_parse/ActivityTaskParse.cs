//auto gen code by fanzhengyong

using System;
using System.Collections.Generic;
using UFrame.Json;

namespace Game.Json.Config
{
    public partial class ActivityTaskParse : JsonConfigBase <ActivityTask>
    {
        protected override void AddPrimaryIndex(ActivityTask v)
        {
            cachePrimary[v.ID] = v;
        }

        protected override string GetConfigFileName()
        {
            return "TableData/" + "ActivityTask" + ".txt";
        }
    }

}
