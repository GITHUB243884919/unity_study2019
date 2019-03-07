﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UFrame.Common;
using UFrame.MessageCenter;
using System;

namespace Game
{
    public class MessageManager : Singleton<MessageManager>, ISingleton
    {
        //public DirectMessageCenter gameMessageCenter = new DirectMessageCenter();
        public BroadcastMessageCenter gameMessageCenter = new BroadcastMessageCenter();

        public void Init()
        {
            //throw new NotImplementedException();
        }

        public void Tick()
        {
            gameMessageCenter.Tick();
        }
    }
}

