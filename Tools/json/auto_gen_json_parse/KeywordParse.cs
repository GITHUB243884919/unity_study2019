//auto gen code by fanzhengyong

using System;
using System.Collections.Generic;
using UFrame.Json;

namespace Game.Json.Config
{
    public partial class KeywordParse : JsonConfigBase <Keyword>
    {
        protected override void AddPrimaryIndex(Keyword v)
        {
            cachePrimary[v.ID] = v;
        }

        protected override string GetConfigFileName()
        {
            return "TableData/" + "Keyword" + ".txt";
        }
    }

}
