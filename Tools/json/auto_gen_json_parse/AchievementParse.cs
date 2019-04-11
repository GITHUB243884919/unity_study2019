//auto gen code by fanzhengyong

using System;
using System.Collections.Generic;
using UFrame.Json;

namespace Game.Json.Config
{
    public partial class AchievementParse : JsonConfigBase <Achievement>
    {
        protected override void AddPrimaryIndex(Achievement v)
        {
            cachePrimary[v.ID] = v;
        }

        protected override string GetConfigFileName()
        {
            return "TableData/" + "Achievement" + ".txt";
        }
    }

}
