using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.MessageDefine
{
    public enum GameMsg
    {
        //服务器客户端消息
        C2S_Login = 1,
        S2C_Login,

        //客户端自身消息
        C2C_Return_Login = 1000,

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

