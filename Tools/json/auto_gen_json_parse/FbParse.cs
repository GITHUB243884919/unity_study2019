//auto gen code by fanzhengyong

using System;
using System.Collections.Generic;
using UFrame.Json;

namespace Game.Json.Config
{
    public partial class FbParse : JsonConfigBase <Fb>
    {
        protected override void AddPrimaryIndex(Fb v)
        {
            cachePrimary[v.ID] = v;
        }

        protected override string GetConfigFileName()
        {
            return "TableData/" + "Fb" + ".txt";
        }
    }

}
