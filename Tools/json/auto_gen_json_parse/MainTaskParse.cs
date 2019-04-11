//auto gen code by fanzhengyong

using System;
using System.Collections.Generic;
using UFrame.Json;

namespace Game.Json.Config
{
    public partial class MainTaskParse : JsonConfigBase <MainTask>
    {
        protected override void AddPrimaryIndex(MainTask v)
        {
            cachePrimary[v.ID] = v;
        }

        protected override string GetConfigFileName()
        {
            return "TableData/" + "MainTask" + ".txt";
        }
    }

}
