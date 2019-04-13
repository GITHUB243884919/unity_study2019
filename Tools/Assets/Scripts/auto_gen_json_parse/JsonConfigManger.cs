//auto gen code by fanzhengyong
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UFrame.Common;

namespace Game.Json.Config
{
    public class JsonConfigManger : Singleton<JsonConfigManger>, ISingleton
    {
        public void Init()
        {
        
        }
        
        public void Load()
        {
            
            newPlayerParse.LoadData ();

        }
        
        
        public NewPlayerParse newPlayerParse = new NewPlayerParse();

    }
}
