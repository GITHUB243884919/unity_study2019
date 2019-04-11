//auto gen code by fanzhengyong

using System;
using System.Collections.Generic;
using UFrame.Json;

namespace Game.Json.Config
{
    public partial class ActivityRechargeDayParse : JsonConfigBase <ActivityRechargeDay>
    {
        protected override void AddPrimaryIndex(ActivityRechargeDay v)
        {
            cachePrimary[v.ID] = v;
        }

        protected override string GetConfigFileName()
        {
            return "TableData/" + "ActivityRechargeDay" + ".txt";
        }
    }

}
