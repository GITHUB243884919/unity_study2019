//auto gen code by fanzhengyong

using System;
using System.Collections.Generic;
using UFrame.Json;

namespace Game.Json.Config
{
    public partial class ChildLvUpParse : JsonConfigBase <ChildLvUp>
    {
        protected override void AddPrimaryIndex(ChildLvUp v)
        {
            cachePrimary[v.ID] = v;
        }

        protected override string GetConfigFileName()
        {
            return "TableData/" + "ChildLvUp" + ".txt";
        }
    }

}
