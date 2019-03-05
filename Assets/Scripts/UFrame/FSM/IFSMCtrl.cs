using System;

namespace FSM
{
	public interface IFSMCtrl
	{
		void Tick (float deltaTime);

		void Goto (int nextStateType, object enterParam, bool allowSameState);
	}
}
