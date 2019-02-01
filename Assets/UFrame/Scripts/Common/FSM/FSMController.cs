using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FSM
{
	public class FSMMachine : FSMBaseCtrl<FSMtate>, IFSMCtrl
	{
		public const string Key_BeforeStateNAN = "FSM_CUS_KEY_BSNAN";

		private OCDictionary defaultStateEnterData = null;

		public FSMMachine ()
		{
		}

		public new void SetDefaultState (int defaultState)
		{
			this.SetDefaultState (defaultState, null);
		}

		public void SetDefaultState (int defaultState, OCDictionary defaultStateEnterData)
		{
			this.defaultState = defaultState;
			this.hadDefaultState = true;
			this.defaultStateEnterData = defaultStateEnterData;
		}

		OCDictionary _enterParamData = new OCDictionary ();

		public override void Tick (float deltaTime)
		{
			if (currState == null) {
				if (hadDefaultState) {
					currState = statePool [defaultState];
					currState._Enter (new FSMParam (defaultState, defaultStateEnterData));
				}
			} else {
				if (_enterParamData == null) {
					_enterParamData = new OCDictionary ();
				}
				int beforeStateType = currState.StateType;
				int nextStateType = currState.GetNextStateType (ref _enterParamData);
				if (!nextStateType.Equals (beforeStateType)) {
					currState._Leave (new FSMParam (nextStateType));
					currState = statePool [nextStateType];
					currState._Enter (new FSMParam (beforeStateType, _enterParamData));
					_enterParamData = new OCDictionary ();
				}
				currState._Tick (deltaTime);
			}
		}

		public override void Goto (int nextStateType, object enterParam, bool allowSameState = false)
		{
			int beforeStateType = default(int);
			OCDictionary enterParamData = enterParam as OCDictionary;
			if (enterParamData == null) {
				enterParamData = new OCDictionary ();
			}
			if (currState != null) {
				beforeStateType = currState.StateType;
				if (!allowSameState && nextStateType.Equals (beforeStateType)) {
					return;
				}
				currState._Leave (new FSMParam (nextStateType));
				enterParamData.Insert (Key_BeforeStateNAN, false);
			} else {
				enterParamData.Insert (Key_BeforeStateNAN, true);
			}
			if (statePool.ContainsKey (nextStateType)) {
				currState = statePool [nextStateType];
				currState._Enter (new FSMParam (beforeStateType, enterParamData));
			}
		}
		
	}
}
