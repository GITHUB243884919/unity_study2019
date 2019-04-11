//auto gen code by fanzhengyong

using System;
using System.Collections.Generic;
using UFrame.Json;

namespace Game.Json.Config
{
    public partial class ActivityRechargeQuotaParse : JsonConfigBase <ActivityRechargeQuota>
    {
        protected override void AddPrimaryIndex(ActivityRechargeQuota v)
        {
            cachePrimary[v.ID] = v;
        }

        protected override string GetConfigFileName()
        {
            return "TableData/" + "ActivityRechargeQuota" + ".txt";
        }
    }

}
