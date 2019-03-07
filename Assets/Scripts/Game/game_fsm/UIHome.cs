using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Game
{
    /// <summary>
    /// 主城
    /// </summary>
    public class UIHome : MonoBehaviour
    {
        public Button btnReturn;
        public Button btnActorView;
        public Button btnExeLuaFile;

        // Use this for initialization
        void Start()
        {
            btnReturn.onClick.AddListener(OnClickReturn);
            btnActorView.onClick.AddListener(OnClickActorView);
            btnExeLuaFile.onClick.AddListener(OnClickExeLuaFile);
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

        void OnClickActorView()
        {
            ResHelper.LoadScene("scenes/actor_view");
        }

        void OnClickExeLuaFile()
        {
            ResHelper.LoadScene("scenes/exe_lua_file");
        }
    }
}

