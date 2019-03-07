using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.MessageDefine
{
    public enum GameMsg
    {
        C2S_Login = 1,
        S2C_Login,

        Return_Login,

    }

    public class GameMsg_C2S_Login : UFrame.MessageCenter.Message
    {
        public GameMsg_C2S_Login()
        {
            messageID = (int)GameMsg.C2S_Login;
        }
        public string userName;
        public string password;
    }

    public class GameMsg_S2C_Login : UFrame.MessageCenter.Message
    {
        public GameMsg_S2C_Login()
        {
            messageID = (int)GameMsg.S2C_Login;
        }

        public bool successed;
    }




}

