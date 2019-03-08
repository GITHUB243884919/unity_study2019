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
        public Button btnNav2D;
        public Button btnAI;

        // Use this for initialization
        void Start()
        {
            btnReturn.onClick.AddListener(OnClickReturn);
            btnActorView.onClick.AddListener(OnClickActorView);
            btnExeLuaFile.onClick.AddListener(OnClickExeLuaFile);
            btnNav2D.onClick.AddListener(OnClickNav2D);
            btnAI.onClick.AddListener(OnClickAI);
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
            msg.messageID = (int)MessageDefine.GameMsg.C2C_Return_Login;
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

        void OnClickNav2D()
        {
            ResHelper.LoadScene("scenes/scene_nav2d");
        }

        void OnClickAI()
        {
            ResHelper.LoadScene("scenes/test_ai");
        }
    }
}

