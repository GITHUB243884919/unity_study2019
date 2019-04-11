//auto gen code by fanzhengyong

using System;
using System.Collections.Generic;
using UFrame.Json;

namespace Game.Json.Config
{
    public partial class MissionParse : JsonConfigBase <Mission>
    {
        protected override void AddPrimaryIndex(Mission v)
        {
            cachePrimary[v.ID] = v;
        }

        protected override string GetConfigFileName()
        {
            return "TableData/" + "Mission" + ".txt";
        }
    }

}
