using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UFrame.MessageCenter
{

    public class LuaMessage : IObjectPoolable
    {
        public int messageID;
        public object body;
        #region Pool
        public static ObjectPool<LuaMessage> pool = new ObjectPool<LuaMessage>();

        public void OnDeathToPool()
        {
            messageID = -1;
            body = null;
        }
        #endregion

        public static LuaMessage CreateLuaMessage(int messageiD, object body = null)
        {
            LuaMessage luaMessage = pool.New();
            luaMessage.messageID = messageiD;
            luaMessage.body = body;

            return luaMessage;
        }

        public void Realse()
        {
            pool.Delete(this);
        }
    }

    public delegate void LuaMessageCallback(LuaMessage msg);

    public class LuaMessageObserver
    {
        public int messageID;
        public object obj;
        public LuaMessageCallback callback;
        public bool vaild;
    }



    public class LuaMessageCenter
    {

        private List<LuaMessageObserver> observers = new List<LuaMessageObserver>();

        private Dictionary<int, List<LuaMessageObserver>> dictObservers = new Dictionary<int, List<LuaMessageObserver>>();

        private bool dirty = false;
        public void Regist(int messageID, object observer, LuaMessageCallback callback)
        {
            LuaMessageObserver _observer = new LuaMessageObserver();
            _observer.messageID = messageID;
            _observer.callback = callback;
            _observer.obj = observer;
            _observer.vaild = true;

            observers.Add(_observer);
            dirty = true;
        }

        public void UnRegist(object observer)
        {
            LuaMessageObserver _observer = null;
            for (int i = 0, iMax = observers.Count; i < iMax; ++i)
            {
                _observer = observers[i];
                if (_observer.obj.Equals(observer))
                {
                    _observer.vaild = false;
                }
            }
            dirty = true;
        }

        public void UnRegist(int messageID, object observer)
        {
            LuaMessageObserver _observer = null;
            for(int i = 0, iMax= observers.Count; i < iMax; ++i)
            {
                _observer = observers[i];
                if (_observer.messageID == messageID && _observer.obj.Equals(observer))
                {
                    _observer.vaild = false;
                }
            }
            dirty = true;
        }

        public void Send(int messageID, object body)
        {
            LuaMessage msg = LuaMessage.CreateLuaMessage(messageID, body);
            Send(msg);
        }

        private void Send(LuaMessage msg)
        {
            RebuilddictObservers();
            if (dictObservers.ContainsKey(msg.messageID) )
            {
                var observers = dictObservers[msg.messageID];
                foreach (var observer in observers)
                {
                    if (observer.vaild)
                    {
                        observer.callback(msg);
                    }
                }
                msg.Realse();
            }
        }

        private void RebuilddictObservers()
        {
            if (!dirty)
            {
                return;
            }

            dictObservers.Clear();
            LuaMessageObserver observer = null;

            for (int i = observers.Count - 1; i >= 0; i--)
            {
                observer = observers[i];
                if (!observer.vaild)
                {
                    observers.RemoveAt(i);
                }
                else
                {
                    if (!dictObservers.ContainsKey(observer.messageID))
                    {
                        dictObservers[observer.messageID] = new List<LuaMessageObserver>();
                    }
                    dictObservers[observer.messageID].Add(observer);
                }
            }
            this.dirty = false;
        }
    }

}
