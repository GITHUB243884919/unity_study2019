using System;

namespace FSM
{
	public interface IFSMState
	{
		int StateType{ get; }

		void _Enter (FSMParam enterParam);

		void _Tick (float delta);

		void _Leave (FSMParam leaveParam);
	}
}
