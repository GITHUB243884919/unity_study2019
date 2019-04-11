//auto gen code by fanzhengyong

using System;
using System.Collections.Generic;
using UFrame.Json;

namespace Game.Json.Config
{
    public partial class GirlSkillExpParse : JsonConfigBase <GirlSkillExp>
    {
        protected override void AddPrimaryIndex(GirlSkillExp v)
        {
            cachePrimary[v.ID] = v;
        }

        protected override string GetConfigFileName()
        {
            return "TableData/" + "GirlSkillExp" + ".txt";
        }
    }

}
