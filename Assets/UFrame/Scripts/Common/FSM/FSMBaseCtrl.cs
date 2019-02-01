using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
	public class FSMBaseCtrl<S> : IFSMCtrl where S : IFSMState
	{
		#region Fields

		protected Dictionary<int, S> statePool = new Dictionary<int, S> ();
		protected S currState;

		protected bool hadDefaultState = false;
		protected int defaultState;

		#endregion

		#region Properties

		public bool Running { get { return currState != null; } }

		public int CurrStateType { 
			get {
				if (currState != null) {
					return currState.StateType;
				} else {
					return default(int);
				}
			}
		}

		#endregion

		#region Constructors

		public FSMBaseCtrl ()
		{
		}

		#endregion

		#region Methods

		public void AddState (S newState)
		{
			this.statePool [newState.StateType] = newState;
		}

		public void SetDefaultState (int defaultState)
		{
			this.hadDefaultState = true;
			this.defaultState = defaultState;
		}

		public bool CurrentStateIs (int state)
		{
			if (this.currState != null) {
				return state.Equals (this.currState.StateType);
			} else {
				return false;
			}
		}

		public S this [int stateType] {
			get {
				return this.statePool [stateType];
			}
		}

		public V GetState<V> (int stateType) where V : S
		{
			return (V)this.statePool [stateType];
		}

		public virtual void Tick (float deltaTime)
		{
			if (this.currState == null && hadDefaultState) {
				this.currState = this.statePool [defaultState];
				this.currState._Enter (new FSMParam (default(int)));
			}
			if (this.currState != null) {
				this.currState._Tick (deltaTime);
			}
		}

        /// <summary>
        /// 进入某新状态，当前状态执行_Leave，新状态执行_Enter
        /// allowSameState == false的情况下，新状态==当前状态不执行
        /// </summary>
        /// <param name="nextStateType"></param>
        /// <param name="enterParam"></param>
        /// <param name="allowSameState"></param>
		public virtual void Goto (int nextStateType, object enterParam = null, bool allowSameState = false)
		{
            int lastState = default(int);
			if (this.currState != null) {
				if (!allowSameState && nextStateType.Equals (this.currState.StateType)) {
					return;
				} else {
					lastState = this.currState.StateType;
					this.currState._Leave (new FSMParam (nextStateType));
				}
			}
			if (this.statePool.ContainsKey (nextStateType)) {
				this.currState = this.statePool [nextStateType];
				this.currState._Enter (new FSMParam (lastState, enterParam));
			}
		}

		#endregion
	}
}
