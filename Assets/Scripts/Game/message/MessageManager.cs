using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UFrame.Common;
using UFrame.MessageCenter;
using System;

namespace Game
{
    public class MessageManager : Singleton<MessageManager>, ISingleton
    {
        public ActionMessageCenter gameMessageCenter = new ActionMessageCenter();
        public void Init()
        {
        }

        public void Tick()
        {
            gameMessageCenter.Tick();
        }
    }
}

