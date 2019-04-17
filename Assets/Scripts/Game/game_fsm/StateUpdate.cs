using UnityEngine;
using UFrame.FSM;
using UFrame.Update;

namespace Game
{
    public class StateUpdate : FSMState
    {
        public bool updateSuccess = false;

        bool realDownLoad = true;
        public StateUpdate(string stateName, FSMMachine fsmCtr) : base(stateName, fsmCtr)
        {
        }

        public override void Enter(string preStateName)
        {
            base.Enter(preStateName);

            if (!realDownLoad)
            {
                updateSuccess = true;
                return;
            }

            //UpdateManager.GetInstance().EnsureGameVersion();
            updateSuccess = true;

            //updateSuccess = false;
            //var http = new HttpDownLoad();
            //string URL = @"http://127.0.0.1:8080/a.txt";
            //http.DownLoad(URL, Application.streamingAssetsPath, "a.txt", DownLoadCallback);
        }

        public override void AddAllConvertCond()
        {
            AddConvertCond("Login", CouldLogin);
        }

        public override void Tick(int deltaTimeMS)
        {
        }

        protected override void GetEnterParam()
        {
        }

        protected override void GetLeaveParam()
        {
        }

        public override void Leave()
        {
            updateSuccess = false;
            base.Leave();
        }

        bool CouldLogin()
        {
            return updateSuccess;
        }

        void DownLoadCallback()
        {
            updateSuccess = true;
        }
    }
}

