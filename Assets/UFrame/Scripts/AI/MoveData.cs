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
        public float speed;

        /// <summary>
        /// 转向速度
        /// </summary>
        public float turnSpeed;

        /// <summary>
        /// 转向类型
        /// </summary>
        public TurnType turnType;
    }

    public class LogicObject
    {
        public int ID;
    }

    public class MoveObject : LogicObject
    {
        public MoveData moveData;

        public int GetID()
        {
            return ID;
        }

        public void SetID(int ID)
        {
            this.ID = ID;
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

        public float GetSpeed()
        {
            return moveData.speed;
        }

        public void SetSpeed(float speed)
        {
            moveData.speed = speed;
        }

        public float GetTurnSpeed()
        {
            return moveData.turnSpeed;
        }

        public void SetTurnSpeed(float turnSpeed)
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
    }

    public class SimpleMoveObjectCtr : MoveObjectCtr
    {
        public override void Tick(int deltaTimeMS)
        {
            float delta = moveObject.GetSpeed() * deltaTimeMS / 1000;
            moveObject.SetPos(delta * moveObject.GetDir());
        }
    }


}

