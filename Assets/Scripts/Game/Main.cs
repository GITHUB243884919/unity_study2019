using UFrame;
using UFrame.FSM;

namespace Game
{
    /// <summary>
    /// 游戏总入口
    /// 如果成员变量希望跨场景访问，定义成static, 或者是一个单件
    /// </summary>
    public class GameApp : IGameApp 
    {
        FSMMachine gameFSMMachine = new FSMMachine();
        
        public void Awake()
        {
            Logger.LogWarp.Log("GameApp Awake");
            FSMState stateUpdate = new StateUpdate("Update", gameFSMMachine);
            FSMState stateLogin = new StateLogin("Login", gameFSMMachine);
            FSMState stateHome = new StateHome("Home", gameFSMMachine);

            gameFSMMachine.AddState(stateUpdate);
            gameFSMMachine.AddState(stateLogin);
            gameFSMMachine.AddState(stateHome);
            gameFSMMachine.SetDefaultState("Update");
        }

        public void LateUpdate()
        {
            
        }

        public void OnApplicationFocus(bool force)
        {

        }

        public void OnApplicationPause(bool pause)
        {

        }

        public void OnApplicationQuit()
        {

        }

        public void OnMemoryWarnning()
        {

        }

        public void Shutdown()
        {

        }

        public void Start()
        {

        }

        public void Update(float s)
        {
            MessageManager.GetInstance().Tick();
            gameFSMMachine.Tick((int)(s * 1000));
            
        }
    }

    public class Main : AMain
    {
        protected override IGameApp CreateGameApp()
        {
            return new GameApp();
        }
    }
}

