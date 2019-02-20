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

        public F64Vec3 PointToLocalSpace2D(F64Vec3 p)
        {
            F64Vec2 p2 = new F64Vec2(p.X, p.Z);
            F64Matrix3x3 m = new F64Matrix3x3();
            F64Vec3 f3 = forward;
            var f2 = new F64Vec2(f3.X, f3.Z);
            var l3 = left;
            var r2 = new F64Vec2(l3.X, l3.Z);
            F64Vec2 point2 = m.PointToLocalSpace(p2,
                f2,
                r2,
                new F64Vec2(moveData.pos.X, moveData.pos.Z));
            //Debug.LogError(point2.X.Float + " " + point2.Y.Float);
            return new F64Vec3(point2.X, p.Y, point2.Y);
        }


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

