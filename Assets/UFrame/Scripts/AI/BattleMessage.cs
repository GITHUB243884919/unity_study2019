using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UFrame.MessageCenter;
using GameName.Lua.Config;
public enum BattleMessageID : int
{
    L2D_BattleInit = 1,
    D2L_BattleInit = 2,
}

public class TankGroupInit
{
    public int id;
    public int tank_type;
    public Vector3 pos;
    public Vector3 dir;
}

public class L2D_BattleInit : UFrame.MessageCenter.Message
{
    public List<TankGroupInit> tankGroup;
}

public class D2L_BattleInit : UFrame.MessageCenter.Message
{
    public bool result;
}
