using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
	public class FSMBaseState<M> : IFSMState where M : IFSMCtrl
	{
		#region Fields

		protected readonly M _ctrl;
		protected float _runningBeforeTime;
		protected float _runningTime;

		#endregion

		public int StateType { get; private set; }

		#region Constructors

		public FSMBaseState (int stateType, M controller)
		{
			this.StateType = stateType;
			this._ctrl = controller;
			_runningTime = 0;
		}

		#endregion

		#region Methods

		public void _Enter (FSMParam enterParam)
		{
			ResetRunningTime ();
			this.Enter (enterParam);
		}

		protected void ResetRunningTime ()
		{
			_runningBeforeTime = -1;
			_runningTime = 0;
		}

		protected virtual void Enter (FSMParam enterParam)
		{
		}

		public void _Tick (float delta)
		{
			_runningBeforeTime = _runningTime;
			_runningTime += delta;
			this.Tick (delta);
		}

		protected virtual void Tick (float delta)
		{
		}

		public void _Leave (FSMParam leaveParam)
		{
			this.Leave (leaveParam);
		}

		protected virtual void Leave (FSMParam leaveParam)
		{
		}

		protected void Goto (int nextStateType, object param = null)
		{
			_ctrl.Goto (nextStateType, param, false);
		}

		protected bool AtTimePoint (float time)
		{
			return _runningBeforeTime < time && time <= _runningTime;
		}

		#endregion
	}
}
