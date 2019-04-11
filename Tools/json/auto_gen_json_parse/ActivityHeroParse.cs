//auto gen code by fanzhengyong

using System;
using System.Collections.Generic;
using UFrame.Json;

namespace Game.Json.Config
{
    public partial class ActivityHeroParse : JsonConfigBase <ActivityHero>
    {
        protected override void AddPrimaryIndex(ActivityHero v)
        {
            cachePrimary[v.ID] = v;
        }

        protected override string GetConfigFileName()
        {
            return "TableData/" + "ActivityHero" + ".txt";
        }
    }

}
