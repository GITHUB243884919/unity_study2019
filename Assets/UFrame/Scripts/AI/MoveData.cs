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
        public Vector3 pos;

        /// <summary>
        /// 朝向
        /// </summary>
        public Vector3 dir;

        public F64Vec3 fPos;
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
        public double speed;

        

        /// <summary>
        /// 转向速度
        /// </summary>
        public double turnSpeed;

        public F64 fSpeed;
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

        public Vector3 GetPos()
        {
            return moveData.pos;
        }

        public void SetPos(Vector3 pos)
        {
            moveData.pos = pos;
        }

        public Vector3 GetDir()
        {
            return moveData.dir;
        }

        public void SetDir(Vector3 dir)
        {
            moveData.dir = dir;
        }

        public double GetSpeed()
        {
            return moveData.speed;
        }

        public void SetSpeed(double speed)
        {
            moveData.speed = speed;
        }

        public double GetTurnSpeed()
        {
            return moveData.turnSpeed;
        }

        public void SetTurnSpeed(double turnSpeed)
        {
            moveData.turnSpeed = turnSpeed;
        }

        #region
        public F64Vec3 GetPos(int A = 0)
        {
            return moveData.fPos;
        }

        public void SetPos(F64Vec3 pos)
        {
            moveData.fPos = pos;
        }

        public F64Vec3 GetDir(int A = 0)
        {
            return moveData.fDir;
        }

        public void SetDir(F64Vec3 dir)
        {
            moveData.fDir = dir;
        }

        public F64 GetSpeed(int A = 0)
        {
            return moveData.fSpeed;
        }

        public void SetSpeed(F64 speed)
        {
            moveData.fSpeed = speed;
        }

        public F64 GetTurnSpeed(int A = 0)
        {
            return moveData.fTurnSpeed;
        }

        public void SetTurnSpeed(F64 turnSpeed)
        {
            moveData.fTurnSpeed = turnSpeed;
        }
        #endregion

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

