//auto gen code by fanzhengyong

using System;
using System.Collections.Generic;
using UFrame.Json;

namespace Game.Json.Config
{
    public partial class ChildNameParse : JsonConfigBase <ChildName>
    {
        protected override void AddPrimaryIndex(ChildName v)
        {
            cachePrimary[v.ID] = v;
        }

        protected override string GetConfigFileName()
        {
            return "TableData/" + "ChildName" + ".txt";
        }
    }

}
