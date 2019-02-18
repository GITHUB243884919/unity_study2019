﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FixMath;
using System;

namespace UFrame.AI
{
    public abstract class MoveObjectCtr
    {
        public MoveObject moveObject { get; set; }

        public abstract void Tick(int deltaTimeMS);

        public abstract void Turn(Vector3 dir);

        public abstract void Turn(F64Vec3 dir);
    }

    public class SimpleMoveObjectCtr : MoveObjectCtr
    {
        public Vector3 dir;
        public F64Vec3 fDir;
        public override void Tick(int deltaTimeMS)
        {
            F64 fdeltaTime = new F64(deltaTimeMS);
            F64 f1000 = F64.F1000;
            if (moveObject.couldTurn)
            {
                F64 fAngle = moveObject.GetTurnSpeed(1) * fdeltaTime / f1000;
                //double angle = moveObject.GetTurnSpeed() * (float)deltaTimeMS / 1000;
                if (moveObject.GetTurnType() == TurnType.Left)
                {
                    //angle = -angle;
                    fAngle = -fAngle;
                    
                }
                Debug.LogError(fAngle.Float);
                ////原来的朝向在XZ平面饶Y旋转angle, angle是欧拉角
                //Vector3 newDir = Quaternion.AngleAxis((float)angle, Vector3.up) * moveObject.GetDir();
                //newDir.Normalize();
                //moveObject.SetDir(newDir);



                //F64Quat rot = F64Quat.FromAxisAngle(F64Vec3.Up, fAngle);
                //F64Vec3 newFdir = F64Quat.RotateVector(rot, moveObject.GetDir(1));
                //F64Vec3 newNfdir = F64Vec3.Normalize(newFdir);
                F64Vec3 newFdir = F64Vec3.RotateY(moveObject.GetDir(1), fAngle);
                F64Vec3 newNfdir = F64Vec3.Normalize(newFdir);
                moveObject.SetDir(newNfdir);
            }

            if (moveObject.couldMove)
            {
                //double delta = moveObject.GetSpeed() * deltaTimeMS / 1000;
                //Vector3 oldPos = moveObject.GetPos();
                //moveObject.SetPos(moveObject.GetPos() + (float)delta * moveObject.GetDir());



                F64 fdelta = moveObject.GetSpeed(1) * fdeltaTime / f1000;
                F64Vec3 fOldPos = moveObject.GetPos(1);
                moveObject.SetPos(fOldPos + fdelta * moveObject.GetDir(1));
                //moveObject.GetSpeed(1) * new F64((float)deltaTimeMS) / new F64((float)1000);
            }
        }



        public override void Turn(Vector3 dir)
        {
            DirByJoyDir(dir);
        }

        public override void Turn(F64Vec3 dir)
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

        void DirByJoyDir(F64Vec3 JoyDir)
        {
            //得到新方向和旧方向的夹角
            F64Vec3 oldDir = moveObject.GetDir(1);
            F64 angle = F64Vec3.Angle(oldDir, JoyDir);

            //得到新方向和旧方向的左边还是右边
            TurnType turnType;
            F64Vec3 cross = F64Vec3.Cross(oldDir, JoyDir);
            float crossY = cross.Y.Float;
            if (crossY > 0)
            {
                turnType = TurnType.Right;
            }
            else if (crossY < 0)
            {
                turnType = TurnType.Left;
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
            Debug.LogError("DirByJoyDir " + turnType);
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
