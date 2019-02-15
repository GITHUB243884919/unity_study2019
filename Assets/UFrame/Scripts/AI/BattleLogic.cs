using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UFrame.MessageCenter;

public class BattleLogic : IMessageExecutor
{
    BattleManager battleManager;
    public BattleLogic(BattleManager battleManager)
    {
        this.battleManager = battleManager;
        battleManager.battleMessageCenter.Regist((int)BattleMessageID.D2L_BattleInit, this);
    }
    public void Execute(UFrame.MessageCenter.Message msg)
    {
        if (msg.messageID == (int)BattleMessageID.D2L_BattleInit)
        {
            D2L_BattleInit initMsg = msg as D2L_BattleInit;
            if (initMsg.result)
            {

            }
        }
    }

    public void InitTank()
    {
        L2D_BattleInit initMsg = new L2D_BattleInit();
        initMsg.messageID = (int)BattleMessageID.L2D_BattleInit;
        initMsg.tanks = new List<int>();
        initMsg.tanks.Add(1);
        battleManager.battleMessageCenter.Send(initMsg);
    }

    public void Tick(int deltaTimeMS)
    {

    }
}
