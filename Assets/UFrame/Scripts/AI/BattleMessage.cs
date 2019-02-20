using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UFrame.MessageCenter;
using GameName.Lua.Config;
public enum BattleMessageID : int
{
    L2D_BattleInit = 1,
    D2L_BattleInit = 2,
    L2D_TankPos,

    JOY_Press,
}

public class TankGroupInit
{
    public int id;

    public bool isPlayer;
    public bool isCaptain;
    public bool isSelf;

    public int tank_type;
    public Vector3 pos;
    public Vector3 dir;
    public float detectionLen;
}

public class TankPos
{
    public int id;
    public Vector3 pos;
    public Vector3 dir;
}

public class Avoidance
{
    //public int id;
    public Vector3 pos;
    public float rad;
    
}

public class L2D_BattleInit : UFrame.MessageCenter.Message
{
    public L2D_BattleInit()
    {
        messageID = (int)BattleMessageID.L2D_BattleInit;
    }
    public List<TankGroupInit> tankGroup;
    public List<Avoidance> avoidances;
}

public class D2L_BattleInit : UFrame.MessageCenter.Message
{
    public D2L_BattleInit()
    {
        messageID = (int)BattleMessageID.D2L_BattleInit;
    }
    public bool result;
}

public class L2D_TankPos : UFrame.MessageCenter.Message
{
    public L2D_TankPos()
    {
        messageID = (int)BattleMessageID.L2D_TankPos;
        tankGroup = new List<TankPos>();
    }
    public List<TankPos> tankGroup;
}

public class JOY_Press : UFrame.MessageCenter.Message
{
    public JOY_Press()
    {
        messageID = (int)BattleMessageID.JOY_Press;
    }

    public Vector3 dir;
    public int tankID;
    public bool couldMove;
    public bool couldTurn;
}