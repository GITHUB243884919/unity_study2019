using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UFrame.MessageCenter;
using UFrame.ResourceManagement;

public class BattleDisplay : IMessageExecutor
{
    BattleManager battleManager;

    Dictionary<int, GameObject> tanks = new Dictionary<int, GameObject>();

    public BattleDisplay(BattleManager battleManager)
    {
        this.battleManager = battleManager;
        battleManager.battleMessageCenter.Regist((int)BattleMessageID.L2D_BattleInit, this);
    }
    public void Execute(UFrame.MessageCenter.Message msg)
    {
        if (msg.messageID == (int)BattleMessageID.L2D_BattleInit)
        {
            L2D_BattleInit initMsg = msg as L2D_BattleInit;
            for (int i = 0; i < initMsg.tanks.Count; ++i)
            {
                GameObjectGetter tankGetter = ResourceManager.GetInstance().LoadGameObject("prefabs/tank");
                GameObject tank = tankGetter.Get();
                tanks.Add(i, tank);
            }
        }
    }

    public void Tick(int deltaTimeMS)
    {

    }
}
