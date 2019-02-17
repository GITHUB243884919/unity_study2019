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
            if (moveObject.couldTurn)
            {
                double angle = moveObject.GetTurnSpeed() * (float)deltaTimeMS / 1000;
                if (moveObject.GetTurnType() == UFrame.AI.TurnType.Left)
                {
                    angle = -angle;
                }

                Vector3 newDir = Quaternion.AngleAxis((float)angle, Vector3.up) * (moveObject.GetDir());
                newDir.Normalize();
                moveObject.SetDir(newDir);

            }

            if (moveObject.couldMove)
            {
                double delta = moveObject.GetSpeed() * deltaTimeMS / 1000;
                Vector3 oldPos = moveObject.GetPos();
                moveObject.SetPos(moveObject.GetPos() + (float)delta * moveObject.GetDir());
            }
        }

        public override void Turn(Vector3 dir)
        {
            DirByJoyDir(dir);
        }

        /// <summary>
        /// 朝向跟JoyDir一致
        /// </summary>
        /// <param name="tankCtr"></param>
        /// <param name="JoyDir"></param>
        void DirByJoyDir(Vector3 JoyDir)
        {
            //得到新方向和旧方向的夹角
            Vector3 oldDir = moveObject.GetDir();
            float angle = Vector3.Angle(oldDir, JoyDir);

            //得到新方向和旧方向的左边还是右边
            UFrame.AI.TurnType turnType;
            if (Vector3.Cross(oldDir, JoyDir).y > 0)
            {
                turnType = UFrame.AI.TurnType.Right;
            }
            else if (Vector3.Cross(oldDir, JoyDir).y < 0)
            {
                turnType = UFrame.AI.TurnType.Left;
            }
            else
            {
                //if (angle > 0)
                //{
                //    turnType = UFrame.AI.TurnType.Right;
                //}
                //else
                //{
                //    turnType = UFrame.AI.TurnType.None;
                //}
                turnType = UFrame.AI.TurnType.None;
            }

            moveObject.SetTurnType(turnType);
        }

        /// <summary>
        /// 根据JoyDir的x的正负确定转向
        /// </summary>
        /// <param name="tankCtr"></param>
        /// <param name="JoyDir"></param>
        void DirByJoyDirX(Vector3 JoyDir)
        {
            //得到新方向和旧方向的左边还是右边
            UFrame.AI.TurnType turnType;
            if (JoyDir.x > 0)
            {
                turnType = UFrame.AI.TurnType.Right;
            }
            else if (JoyDir.x < 0)
            {
                turnType = UFrame.AI.TurnType.Left;
            }
            else
            {
                turnType = UFrame.AI.TurnType.None;
            }


            moveObject.SetTurnType(turnType);
        }
    }


}

