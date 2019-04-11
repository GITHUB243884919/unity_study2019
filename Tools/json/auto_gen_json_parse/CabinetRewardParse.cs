//auto gen code by fanzhengyong

using System;
using System.Collections.Generic;
using UFrame.Json;

namespace Game.Json.Config
{
    public partial class CabinetRewardParse : JsonConfigBase <CabinetReward>
    {
        protected override void AddPrimaryIndex(CabinetReward v)
        {
            cachePrimary[v.ID] = v;
        }

        protected override string GetConfigFileName()
        {
            return "TableData/" + "CabinetReward" + ".txt";
        }
    }

}
