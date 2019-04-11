//auto gen code by fanzhengyong

using System;
using System.Collections.Generic;
using UFrame.Json;

namespace Game.Json.Config
{
    public partial class GirlParse : JsonConfigBase <Girl>
    {
        protected override void AddPrimaryIndex(Girl v)
        {
            cachePrimary[v.ID] = v;
        }

        protected override string GetConfigFileName()
        {
            return "TableData/" + "Girl" + ".txt";
        }
    }

}
