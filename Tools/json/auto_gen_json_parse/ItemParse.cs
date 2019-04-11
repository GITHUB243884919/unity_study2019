//auto gen code by fanzhengyong

using System;
using System.Collections.Generic;
using UFrame.Json;

namespace Game.Json.Config
{
    public partial class ItemParse : JsonConfigBase <Item>
    {
        protected override void AddPrimaryIndex(Item v)
        {
            cachePrimary[v.ID] = v;
        }

        protected override string GetConfigFileName()
        {
            return "TableData/" + "Item" + ".txt";
        }
    }

}
