//auto gen code by fanzhengyong

using System;
using System.Collections.Generic;
using UFrame.Json;

namespace Game.Json.Config
{
    public partial class helpParse : JsonConfigBase <help>
    {
        protected override void AddPrimaryIndex(help v)
        {
            cachePrimary[v.ID] = v;
        }

        protected override string GetConfigFileName()
        {
            return "TableData/" + "help" + ".txt";
        }
    }

}
