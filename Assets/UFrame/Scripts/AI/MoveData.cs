using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        public TurnType GetTurnType()
        {
            return moveData.turnType;
        }

        public void SetTurnType(TurnType turnType)
        {
            moveData.turnType = turnType;
        }

    }

    public abstract class MoveObjectCtr
    {
        public MoveObject moveObject { get; set; }
        
        public abstract void Tick(int deltaTimeMS);

        public abstract void Turn(Vector3 dir);
    }

    public class SimpleMoveObjectCtr : MoveObjectCtr
    {
        public Vector3 dir;
        public override void Tick(int deltaTimeMS)
        {
            //if (moveObject.couldTurn)
            //{
            //    float turnAngle = Vector3.Angle(moveObject.GetPos(), dir);
            //    Vector3.Lerp(moveObject.GetPos(), dir, (float)deltaTimeMS / 1000);

            //}

            if (moveObject.couldMove)
            {
                double delta = moveObject.GetSpeed() * deltaTimeMS / 1000;
                Vector3 oldPos = moveObject.GetPos();
                moveObject.SetPos(moveObject.GetPos() + (float)delta * moveObject.GetDir());

            }
        }

        public override void Turn(Vector3 dir)
        {
            this.dir = dir;
        }
    }


}

