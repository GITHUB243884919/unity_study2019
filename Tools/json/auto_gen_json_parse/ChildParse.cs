//auto gen code by fanzhengyong

using System;
using System.Collections.Generic;
using UFrame.Json;

namespace Game.Json.Config
{
    public partial class ChildParse : JsonConfigBase <Child>
    {
        protected override void AddPrimaryIndex(Child v)
        {
            cachePrimary[v.ID] = v;
        }

        protected override string GetConfigFileName()
        {
            return "TableData/" + "Child" + ".txt";
        }
    }

}
