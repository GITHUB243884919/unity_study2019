using System;

namespace FSM
{
	public class FSMParam
	{
		public int otherState;
		public object param;

		public FSMParam (int state, object param = null)
		{
            this.SetParam(state, param);
        }

		public K GetParam<K> ()
		{
			return (K)param;
		}

        public FSMParam SetParam(int state, object param = null)
        {
            this.otherState = state;
            this.param = param;
            return this;
        }
	}
}

