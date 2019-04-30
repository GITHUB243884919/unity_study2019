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
    }
}

