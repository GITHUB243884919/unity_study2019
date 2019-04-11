//auto gen code by fanzhengyong

using System;
using System.Collections.Generic;
using UFrame.Json;

namespace Game.Json.Config
{
    public partial class LanguageParse : JsonConfigBase <Language>
    {
        protected override void AddPrimaryIndex(Language v)
        {
            cachePrimary[v.ID] = v;
        }

        protected override string GetConfigFileName()
        {
            return "TableData/" + "Language" + ".txt";
        }
    }

}
