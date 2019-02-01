using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UFrame.MessageCenter
{
    public class Message : IObjectPoolable
    {
        #region pool
        public virtual void OnDeathToPool()
        {
            this.messageID = -1;
        }

        public virtual void Release()
        {
        }
        #endregion
        
        public int messageID;
    }

    public interface IMessageExecutor
    {
        void Execute(Message msg);
    }

    public abstract class BroadcastMessageExecutor : IMessageExecutor
    {
        protected LinkedListNode<BroadcastMessageExecutor> linkNode;
        public BroadcastMessageExecutor()
        {
            linkNode = new LinkedListNode<BroadcastMessageExecutor>(this);
        }

        public abstract void Execute(Message msg);
        
    }


    /// <summary>
    /// 一个消息有唯一的执行器，消息只发送到对应的唯一执行器。
    /// 消息属于pool对象，执行完毕后pool会回收。
    /// 因此，如果执行器不是立即使用消息，需要用新对象缓存消息。
    /// </summary>
    public class DirectMessageCenter
    {
        protected Queue<Message> messages = new Queue<Message>();
        protected Dictionary<int, IMessageExecutor> executors = new Dictionary<int, IMessageExecutor>();

        public void Regist(int messageID, IMessageExecutor executor)
        {
            executors.Add(messageID, executor);
        }

        public void UnRegist(int messageID)
        {
            if (executors.ContainsKey(messageID))
            {
                executors.Remove(messageID);
            }
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
                IMessageExecutor executor = null;
                if (executors.TryGetValue(msg.messageID, out executor))
                {
                    executor.Execute(msg);
                }
                msg.Release();
            }
        }
    }

    /// <summary>
    /// 一个消息有多个的执行器，消息广播到所有执行器，执行器根据消息编号自行判定是否需要执行
    /// </summary>
    public class BroadcastMessageCenter
    {
        protected Queue<Message> messages = new Queue<Message>();
        protected LinkedList<BroadcastMessageExecutor> executors = new LinkedList<BroadcastMessageExecutor>();
        public void Regist(BroadcastMessageExecutor executor)
        {
            executors.AddLast(executor);
        }

        public void UnRegist(BroadcastMessageExecutor executor)
        {
            executors.Remove(executor);
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
                foreach(var executor in executors)
                {
                    executor.Execute(msg);
                }
                msg.Release();
            }
        }
    }

}

