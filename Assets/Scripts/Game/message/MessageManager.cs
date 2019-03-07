using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UFrame.Common;
using UFrame.MessageCenter;

namespace Game
{
    public class MessageManager : Singleton<MessageManager>
    {
        //public DirectMessageCenter gameMessageCenter = new DirectMessageCenter();
        public BroadcastMessageCenter gameMessageCenter = new BroadcastMessageCenter();

        public void Tick()
        {
            gameMessageCenter.Tick();
        }
    }
}

