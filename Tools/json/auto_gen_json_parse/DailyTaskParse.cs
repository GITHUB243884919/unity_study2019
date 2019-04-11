//auto gen code by fanzhengyong

using System;
using System.Collections.Generic;
using UFrame.Json;

namespace Game.Json.Config
{
    public partial class DailyTaskParse : JsonConfigBase <DailyTask>
    {
        protected override void AddPrimaryIndex(DailyTask v)
        {
            cachePrimary[v.ID] = v;
        }

        protected override string GetConfigFileName()
        {
            return "TableData/" + "DailyTask" + ".txt";
        }
    }

}
