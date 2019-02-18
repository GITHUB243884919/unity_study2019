using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FixMath;
namespace UFrame.AI
{
    public class PostionData
    {
        /// <summary>
        /// 位置
        /// </summary>
        public F64Vec3 fPos;

        /// <summary>
        /// 朝向
        /// </summary>
        public F64Vec3 fDir;
    }

    public enum TurnType
    {
        None,
        Left,
        Right,
    }

    public class MoveData : PostionData
    {
        /// <summary>
        /// 速度
        /// </summary>
        public F64 fSpeed;

        /// <summary>
        /// 转向速度
        /// </summary>
        public F64 fTurnSpeed;

        /// <summary>
        /// 转向类型
        /// </summary>
        public TurnType turnType;
    }

    public class LogicObject
    {
        static int sID;
        public LogicObject()
        {
            ID = sID++;
        }
        public int ID;
    }

    public class MoveObject : LogicObject
    {
        public MoveData moveData;

        public bool couldMove;

        public bool couldTurn;

        public MoveObject()
        {
            moveData = new MoveData();
        }
        public int GetID()
        {
            return ID;
        }

        public F64Vec3 GetPos()
        {
            return moveData.fPos;
        }

        public void SetPos(F64Vec3 pos)
        {
            moveData.fPos = pos;
        }

        public F64Vec3 GetDir()
        {
            return moveData.fDir;
        }

        public void SetDir(F64Vec3 dir)
        {
            moveData.fDir = dir;
        }

        public F64 GetSpeed()
        {
            return moveData.fSpeed;
        }

        public void SetSpeed(F64 speed)
        {
            moveData.fSpeed = speed;
        }

        public F64 GetTurnSpeed()
        {
            return moveData.fTurnSpeed;
        }

        public void SetTurnSpeed(F64 turnSpeed)
        {
            moveData.fTurnSpeed = turnSpeed;
        }

        public TurnType GetTurnType()
        {
            return moveData.turnType;
        }

        public void SetTurnType(TurnType turnType)
        {
            moveData.turnType = turnType;
        }
    }
}

