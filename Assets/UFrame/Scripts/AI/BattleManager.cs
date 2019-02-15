using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UFrame.MessageCenter;

public class BattleManager : MonoBehaviour {

    public DirectMessageCenter battleMessageCenter;
    public BattleLogic battleLogic;
    public BattleDisplay battleDisplay;

    // Use this for initialization
    void Start ()
    {
        battleMessageCenter = new DirectMessageCenter();
        battleLogic = new BattleLogic(this);
        battleDisplay = new BattleDisplay(this);

        battleLogic.InitTank();
    }


	
	// Update is called once per frame
	void Update () {
        battleMessageCenter.Tick();
        battleLogic.Tick((int)(Time.deltaTime * 1000));
        battleDisplay.Tick((int)(Time.deltaTime * 1000));
    }

}
