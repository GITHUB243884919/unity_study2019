//auto gen code by fanzhengyong

using System;
using System.Collections.Generic;
using UFrame.Json;

namespace Game.Json.Config
{
    public partial class ChildQuaParse : JsonConfigBase <ChildQua>
    {
        protected override void AddPrimaryIndex(ChildQua v)
        {
            cachePrimary[v.ID] = v;
        }

        protected override string GetConfigFileName()
        {
            return "TableData/" + "ChildQua" + ".txt";
        }
    }

}
