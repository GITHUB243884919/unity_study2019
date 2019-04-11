//auto gen code by fanzhengyong

using System;
using System.Collections.Generic;
using UFrame.Json;

namespace Game.Json.Config
{
    public partial class BuildingVisitParse : JsonConfigBase <BuildingVisit>
    {
        protected override void AddPrimaryIndex(BuildingVisit v)
        {
            cachePrimary[v.ID] = v;
        }

        protected override string GetConfigFileName()
        {
            return "TableData/" + "BuildingVisit" + ".txt";
        }
    }

}
