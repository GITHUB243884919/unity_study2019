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
        public F64Vec3 pos;

        /// <summary>
        /// 朝向
        /// </summary>
        public F64Vec3 dir;
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
        public F64 speed;

        /// <summary>
        /// 转向速度
        /// </summary>
        public F64 turnSpeed;

        /// <summary>
        /// 转向类型
        /// </summary>
        public TurnType turnType;

        /// <summary>
        /// 探测距离
        /// </summary>
        public F64 detectionLen;

        /// <summary>
        /// 探测宽度
        /// </summary>
        public F64 detectionWidth;
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

        public F64Vec3 forward { get { return moveData.dir; } }

        public F64Vec3 left { get {
                F64Vec3 tmp = F64Vec3.RotateY(forward, new FixMath.F64(-90));
                return F64Vec3.Normalize(tmp);
            }
        }

        public F64Vec3 up { get { return F64Vec3.Cross(forward, left); } }

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
            return moveData.pos;
        }

        public void SetPos(F64Vec3 pos)
        {
            moveData.pos = pos;
        }

        public F64Vec3 GetDir()
        {
            return moveData.dir;
        }

        public void SetDir(F64Vec3 dir)
        {
            moveData.dir = dir;
        }

        public F64 GetSpeed()
        {
            return moveData.speed;
        }

        public void SetSpeed(F64 speed)
        {
            moveData.speed = speed;
        }

        public F64 GetTurnSpeed()
        {
            return moveData.turnSpeed;
        }

        public void SetTurnSpeed(F64 turnSpeed)
        {
            moveData.turnSpeed = turnSpeed;
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

