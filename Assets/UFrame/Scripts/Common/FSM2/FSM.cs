using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UFrame.FSM
{
    public abstract class FSMState
    {
        public string stateName;
        public string preStateName;
        public FSMMachine fsmCtr;
 
        Dictionary<string, System.Func<bool>> convertConds = new Dictionary<string, System.Func<bool>>();

        public FSMState(string stateName, FSMMachine fsmCtr)
        {
            this.stateName = stateName;
            this.fsmCtr = fsmCtr;
            AddAllConvertCond();
        }

        protected abstract void GetEnterParam();

        public virtual void Enter(string preStateName)
        {
            this.preStateName = preStateName;
            GetEnterParam();
        }

        public abstract void Tick(int deltaTimeMS);

        protected abstract void GetLeaveParam();

        public virtual void Leave()
        {
            GetLeaveParam();
        }

        public abstract void AddAllConvertCond();

        public void AddConvertCond(string stateName, System.Func<bool> callback)
        {
            convertConds.Add(stateName, callback);
        }

        public string ChangeState()
        {
            string newState = null;
            foreach(var kv in convertConds)
            {
                if (kv.Value())
                {
                    newState = kv.Key;
                }
            }

            return newState;
        }

        public virtual void Realse()
        {
            fsmCtr = null;
            preStateName = null;
        }
    }

    public class FSMMachine
    {
        Dictionary<string, FSMState> states = new Dictionary<string, FSMState>();
        FSMState currState;
        string preStateName;

        public void AddState(FSMState state)
        {
            states.Add(state.stateName, state);
        }

        public void SetDefaultState(string stateName)
        {
            if (currState == null)
            {
                currState = states[stateName];
            }
        }

        public void GotoState(string stateName)
        {
            if (currState != null)
            {
                if (currState.stateName == stateName)
                {
                    return;
                }
                preStateName = currState.stateName;
                currState.Leave();
            }

            currState = states[stateName];
            currState.Enter(preStateName);
        }

        public void Tick(int deltaTimeMS)
        {
            if (currState != null)
            {
                currState.Tick(deltaTimeMS);
                string newState = currState.ChangeState();
                if (!string.IsNullOrEmpty(newState))
                {
                    GotoState(newState);
                }
            }
        }

        public void Stop()
        {
            if (currState != null)
            {
                currState.Leave();
                currState = null;
            }
        }

        public virtual void Realse()
        {
            Stop();
            foreach(var v in states.Values)
            {
                v.Realse();
            }
            states.Clear();
            preStateName = null;
        }
    }




}


