using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UFrame.FSM;
using System;

public class FSMState1 : FSMState
{
    public FSMState1(string stateName, FSMMachine fsmCtr) : base(stateName, fsmCtr)
    {

    }

    public override void Tick(int deltaTimeMS)
    {
        (fsmCtr as FSMMachineTest).id += deltaTimeMS;
        Debug.LogError(stateName + " " + (fsmCtr as FSMMachineTest).id);
    }

    public override void AddAllConvertCond()
    {
        AddConvertCond("state2", ConvertState2Cond);
    }
    public bool ConvertState2Cond()
    {
        if ((fsmCtr as FSMMachineTest).id > 5000)
        {
            return true;
        }

        return false;
    }

    protected override void GetEnterParam()
    {
    }

    protected override void GetLeaveParam()
    {

    }
}

public class FSMState2 : FSMState
{
    public FSMState2(string stateName, FSMMachine fsmCtr) : base(stateName, fsmCtr)
    {

    }

    public override void AddAllConvertCond()
    {
        
    }

    public override void Tick(int deltaTimeMS)
    {

        (fsmCtr as FSMMachineTest).id += deltaTimeMS;
        Debug.LogError(stateName + " " + (fsmCtr as FSMMachineTest).id);
        if ((fsmCtr as FSMMachineTest).id > 10000)
        {
            Debug.LogError(stateName + " stop");
        }
    }

    protected override void GetEnterParam()
    {
        
    }

    protected override void GetLeaveParam()
    {
        
    }
}

public class FSMMachineTest : FSMMachine
{
    public int id;
}

public class FSMTest : MonoBehaviour {

    // Use this for initialization
    FSMMachine machine;
    FSMState1 state1;
    FSMState2 state2;
    void Start () {

        machine = new FSMMachineTest();
        state1 = new FSMState1("state1", machine);
        state2 = new FSMState2("state2", machine);
        machine.AddState(state1);
        machine.AddState(state2);
        machine.GoToState("state1");
    }
	
	// Update is called once per frame
	void Update ()
    {
        machine.Tick((int)(Time.deltaTime * 1000));

    }
}
