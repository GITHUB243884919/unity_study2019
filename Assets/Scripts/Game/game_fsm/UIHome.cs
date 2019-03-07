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
    public class UIHome : MonoBehaviour
    {
        public Button btnReturn;

        // Use this for initialization
        void Start()
        {
            btnReturn.onClick.AddListener(OnClickReturn);
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnClickReturn()
        {
            //发送登录消息
            SendReturn();
        }

        void SendReturn()
        {
            UFrame.MessageCenter.Message msg = new UFrame.MessageCenter.Message();
            msg.messageID = (int)MessageDefine.GameMsg.Return_Login;
            MessageManager.GetInstance().gameMessageCenter.Send(msg);
        }

    }
}

