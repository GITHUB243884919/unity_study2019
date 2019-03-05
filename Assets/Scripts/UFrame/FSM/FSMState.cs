using UnityEngine;
using System.Collections;

namespace FSM
{
	public class FSMtate : FSMBaseState<FSMMachine>, IFSMState
	{
		/// <summary>
		/// Get the running time.
		/// </summary>
		/// <value>Current state running time(s).</value>
		public float runningTime{ get { return _runningTime; } }

		public FSMtate (int stateType, FSMMachine controller) : base (stateType, controller)
		{
		}

		protected sealed override void Enter (FSMParam enterParam)
		{
			this.AEnter (enterParam.otherState, enterParam.param as OCDictionary);
		}

		protected virtual void AEnter (int beforeState, OCDictionary param)
		{
		}

		protected sealed override void Leave (FSMParam enterParam)
		{
			this.ALeave (enterParam.otherState);
		}

		protected virtual void ALeave (int nextState)
		{
		}

		public virtual int GetNextStateType (ref OCDictionary nextStateEnterParamData)
		{
			return StateType;
		}
	}
}
