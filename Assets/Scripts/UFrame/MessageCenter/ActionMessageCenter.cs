using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace UFrame.MessageCenter
{
    public class ActionMessageCenter
    {
        protected Queue<Message> messages = new Queue<Message>();
        protected Dictionary<int, HashSet<Action<Message>>> executorDic =
            new Dictionary<int, HashSet<Action<Message>>>();

        public void Regist(int messageID, Action<Message> executor)
        {
            HashSet<Action<Message>> excutors = null;
            if (!executorDic.TryGetValue(messageID, out excutors))
            {
                excutors = new HashSet<Action<Message>>();
                executorDic.Add(messageID, excutors);
            }
            excutors.Add(executor);
        }

        public void UnRegist(int messageID, Action<Message> executor)
        {
            HashSet<Action<Message>> excutors = null;
            if (!executorDic.TryGetValue(messageID, out excutors))
            {
                return;
            }
            excutors.Remove(executor);
        }

        public void Send(Message msg, bool immediately = true)
        {
            if (immediately)
            {
                Execute(msg);
                return;
            }

            messages.Enqueue(msg);
        }

        public void Tick()
        {
            while (messages.Count > 0)
            {
                Message msg = messages.Dequeue();
                Execute(msg);
            }
        }

        void Execute(Message msg)
        {
            HashSet<Action<Message>> excutors = null;
            if (!executorDic.TryGetValue(msg.messageID, out excutors))
            {
                throw new System.Exception("消息被注册" + msg.messageID);
            }

            foreach (var executor in excutors)
            {
                executor(msg);
            }
            msg.Release();
        }
    }
}
