using System;
using UFrame.Common;
using UFrame.MessageCenter;

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

        public void Regist(int messageID, Action<UFrame.MessageCenter.Message> executor)
        {
            gameMessageCenter.Regist(messageID, executor);
        }

        public void UnRegist(int messageID, Action<UFrame.MessageCenter.Message> executor)
        {
            gameMessageCenter.UnRegist(messageID, executor);
        }

        public void Send(UFrame.MessageCenter.Message msg, bool immediately = true)
        {
            gameMessageCenter.Send(msg, immediately);
        }
    }
}

