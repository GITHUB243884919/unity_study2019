//auto gen code by fanzhengyong

using System;
using System.Collections.Generic;
using UFrame.Json;

namespace Game.Json.Config
{
    public partial class NewPlayerParse : JsonConfigBase <NewPlayer>
    {
        protected override void AddPrimaryIndex(NewPlayer v)
        {
            cachePrimary[v.ID] = v;
        }

        protected override string GetConfigFileName()
        {
            return "TableData/" + "NewPlayer" + ".txt";
        }
    }

}
