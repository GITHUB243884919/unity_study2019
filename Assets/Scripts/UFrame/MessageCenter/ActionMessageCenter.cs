using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace UFrame.MessageCenter
{
    public class ActionMessageCenter
    {
        protected Queue<Message> messages = new Queue<Message>();
        protected Dictionary<int, HashSet<Action<Message>>> executors =
            new Dictionary<int, HashSet<Action<Message>>>();

        public void Regist(int messageID, Action<Message> executor)
        {
            HashSet<Action<Message>> linkExecutor = null;
            if (!executors.TryGetValue(messageID, out linkExecutor))
            {
                linkExecutor = new HashSet<Action<Message>>();
                executors.Add(messageID, linkExecutor);
            }
            linkExecutor.Add(executor);
        }

        public void UnRegist(int messageID, Action<Message> executor)
        {
            HashSet<Action<Message>> linkExecutor = null;
            if (!executors.TryGetValue(messageID, out linkExecutor))
            {
                return;
            }
            linkExecutor.Remove(executor);
        }

        public void Send(Message msg)
        {
            messages.Enqueue(msg);
        }

        public void Tick()
        {
            while (true)
            {
                if (messages.Count <= 0)
                {
                    return;
                }

                Message msg = messages.Dequeue();

                HashSet<Action<Message>> linkExecutor = null;
                if (!executors.TryGetValue(msg.messageID, out linkExecutor))
                {
                    throw new System.Exception("没有消息被注册" + msg.messageID);
                }

                foreach (var executor in linkExecutor)
                {
                    executor(msg);
                }
                msg.Release();
            }
        }
    }
}
