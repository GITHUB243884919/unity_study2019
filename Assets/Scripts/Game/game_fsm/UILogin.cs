using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Game
{
    /// <summary>
    /// 发送登录消息
    /// 等待服务器返回后进入主城场景
    /// </summary>
    public class UILogin : MonoBehaviour
    {
        public Button btn;

        // Use this for initialization
        void Start()
        {
            btn.onClick.AddListener(OnClickLogin);
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnClickLogin()
        {
            //发送登录消息
            SendLogin();
        }

        void SendLogin()
        {
            //Debug.LogError("SendLogin");
            MessageDefine.GameMsg_C2S_Login msgLogin = new MessageDefine.GameMsg_C2S_Login();
            msgLogin.userName = "AAAA";
            msgLogin.password = "0000";
            MessageManager.GetInstance().gameMessageCenter.Send(msgLogin);
        }


    }
}

