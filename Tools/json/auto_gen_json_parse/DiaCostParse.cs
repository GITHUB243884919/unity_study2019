//auto gen code by fanzhengyong

using System;
using System.Collections.Generic;
using UFrame.Json;

namespace Game.Json.Config
{
    public partial class DiaCostParse : JsonConfigBase <DiaCost>
    {
        protected override void AddPrimaryIndex(DiaCost v)
        {
            cachePrimary[v.ID] = v;
        }

        protected override string GetConfigFileName()
        {
            return "TableData/" + "DiaCost" + ".txt";
        }
    }

}
