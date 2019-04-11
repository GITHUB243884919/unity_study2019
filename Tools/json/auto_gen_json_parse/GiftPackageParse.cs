//auto gen code by fanzhengyong

using System;
using System.Collections.Generic;
using UFrame.Json;

namespace Game.Json.Config
{
    public partial class GiftPackageParse : JsonConfigBase <GiftPackage>
    {
        protected override void AddPrimaryIndex(GiftPackage v)
        {
            cachePrimary[v.ID] = v;
        }

        protected override string GetConfigFileName()
        {
            return "TableData/" + "GiftPackage" + ".txt";
        }
    }

}
