using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UFrame.MessageCenter;

public enum BattleMessageID : int
{
    L2D_BattleInit = 1,
    D2L_BattleInit = 2,
}

public class L2D_BattleInit : UFrame.MessageCenter.Message
{
    public List<int> tanks;
}

public class D2L_BattleInit : UFrame.MessageCenter.Message
{
    public bool result;
}
