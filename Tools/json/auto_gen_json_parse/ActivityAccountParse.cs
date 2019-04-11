//auto gen code by fanzhengyong

using System;
using System.Collections.Generic;
using UFrame.Json;

namespace Game.Json.Config
{
    public partial class ActivityAccountParse : JsonConfigBase <ActivityAccount>
    {
        protected override void AddPrimaryIndex(ActivityAccount v)
        {
            cachePrimary[v.ID] = v;
        }

        protected override string GetConfigFileName()
        {
            return "TableData/" + "ActivityAccount" + ".txt";
        }
    }

}
