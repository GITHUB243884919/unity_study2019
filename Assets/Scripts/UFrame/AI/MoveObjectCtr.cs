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

        public abstract void Turn(F64Vec3 dir);
    }


    public class SimpleMoveObjectCtr : MoveObjectCtr
    {
        GameName.Battle.Logic.BattleLogic logic;

        public SimpleMoveObjectCtr(GameName.Battle.Logic.BattleLogic logic)
        {
            this.logic = logic;
        }

        public override void Tick(int deltaTimeMS)
        {
            F64 fdeltaTime = new F64(deltaTimeMS);

            TickAvoidance2(fdeltaTime);
            TickTurn(fdeltaTime);
            TickMove(fdeltaTime);
        }

        void TickAvoidance(F64 fdeltaTime, bool isMove = false)
        {
            foreach(var v in logic.logicDataManager.GetAvoidances())
            {
                //在检查范围外
                F64 sqrDis = F64Vec3.LengthSqr(moveObject.GetPos() - v.pos);
                F64 sqrDetectionLenEx = moveObject.moveData.detectionLen + v.radius;
                sqrDetectionLenEx *= sqrDetectionLenEx;
                if (sqrDis > (sqrDetectionLenEx))
                {
                    continue;
                }

                //在后面
                var local = F64Vec3.PointToLocalSpace2D(v.pos, moveObject.forward, moveObject.left, moveObject.GetPos());
                if (local.X < F64.Zero)
                {
                    continue;
                }

                //没碰到
                F64 absZ = F64.Abs(local.Z);
                F64 detectionWidthEx = moveObject.moveData.detectionWidth + v.radius;
                if (absZ > (detectionWidthEx))
                {
                    continue;                    
                }

                Debug.LogError("碰到 " + local + " " + v.radius + " " + this.moveObject.moveData.detectionWidth);

                //关闭超控
                moveObject.couldMove = false;
                moveObject.couldTurn = false;

                //计算侧向力
                F64 fAngle = moveObject.GetTurnSpeed() * fdeltaTime / F64.F1000;
                if (local.Z < F64.Zero)
                {
                    fAngle = -fAngle;
                }
                //fAngle *= (moveObject.GetSpeed() / absZ);
                //转向
                F64Vec3 dir = F64Vec3.RotateY(moveObject.GetDir(), fAngle);
                dir = F64Vec3.Normalize(dir);
                moveObject.SetDir(dir);

                //向避开的方向走， 不精确
                if (isMove)
                {
                    F64 fdelta = moveObject.GetSpeed() * fdeltaTime / F64.F1000;
                    F64Vec3 pos = moveObject.GetPos();
                    F64Vec3 force = fdelta * moveObject.GetDir();
                    pos += force;
                    moveObject.SetPos(pos);
                }
            }
        }

        void TickAvoidance2(F64 fdeltaTime, bool isMove = false)
        {
            foreach (var v in logic.logicDataManager.GetAvoidances())
            {
                //在检查范围外
                F64 sqrDis = F64Vec3.LengthSqr(moveObject.GetPos() - v.pos);
                F64 sqrDetectionLenEx = moveObject.moveData.detectionLen + v.radius;
                sqrDetectionLenEx *= sqrDetectionLenEx;
                if (sqrDis > (sqrDetectionLenEx))
                {
                    continue;
                }

                //在后面
                var local = F64Vec3.PointToLocalSpace2D(v.pos, moveObject.forward, moveObject.left, moveObject.GetPos());
                if (local.X < F64.Zero)
                {
                    continue;
                }

                //没碰到
                F64 absZ = F64.Abs(local.Z);
                F64 detectionWidthEx = moveObject.moveData.detectionWidth + v.radius;
                if (absZ > (detectionWidthEx))
                {
                    continue;
                }

                Debug.LogError("碰到 " + local + " " + v.radius + " " + this.moveObject.moveData.detectionWidth);

                //关闭超控
                moveObject.couldMove = false;
                moveObject.couldTurn = false;

                F64 newZ = F64.Zero;
                if (F64.Approximately(local.Z, F64.Zero))
                {
                    newZ = local.Z - v.radius - moveObject.moveData.detectionWidth;
                }
                else if (local.Z < F64.Zero)
                {
                    newZ = local.Z - v.radius - moveObject.moveData.detectionWidth;
                }
                else
                {
                    newZ = local.Z + v.radius + moveObject.moveData.detectionWidth;
                }

                

                F64Vec3 wantDir = new F64Vec3(local.X, local.Y, newZ);
                wantDir = F64Vec3.PointToWorldSpace2D(wantDir, moveObject.forward, moveObject.left, moveObject.GetPos());
                Debug.DrawLine(moveObject.GetPos().ToUnityVector3(), wantDir.ToUnityVector3(), Color.red);


            }
        }


        public void TickTurn(F64 fdeltaTime)
        {
            if (moveObject.couldTurn)
            {
                F64 fAngle = moveObject.GetTurnSpeed() * fdeltaTime / F64.F1000;
                if (moveObject.GetTurnType() == TurnType.Left)
                {
                    fAngle = -fAngle;

                }

                F64Vec3 dir = F64Vec3.RotateY(moveObject.GetDir(), fAngle);
                dir = F64Vec3.Normalize(dir);
                moveObject.SetDir(dir);
            }
        }

        public void TickMove(F64 fdeltaTime)
        {
            if (moveObject.couldMove)
            {
                F64 fdelta = moveObject.GetSpeed() * fdeltaTime / F64.F1000;
                F64Vec3 pos = moveObject.GetPos();
                F64Vec3 force = fdelta * moveObject.GetDir();
                pos += force;
                moveObject.SetPos(pos);
            }



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
        void DirByJoyDir(F64Vec3 JoyDir)
        {

            //得到新方向和旧方向的夹角
            F64Vec3 oldDir = moveObject.GetDir();
            F64 angle = F64Vec3.Angle(oldDir, JoyDir);

            //得到新方向和旧方向的左边还是右边
            TurnType turnType;
            //todo 可以优化，只取y就只计算叉乘的y部分
            F64Vec3 cross = F64Vec3.Cross(oldDir, JoyDir);
            //顺时针
            if (cross.Y > F64.Zero)
            {
                turnType = TurnType.Right;
            }
            else if (cross.Y < F64.Zero)
            {
                turnType = TurnType.Left;
            }
            else
            {
                turnType = TurnType.None;
            }
            //当相机旋转的时候，考虑朝向是否的相反，即是眼睛看到的向左就向左
            //如果不考虑，那么当相机面朝角色时，摇杆的左右操作和视觉上是相反的。
            //Vector3 cf3 = Camera.main.transform.forward;
            //Vector2 cf2 = new Vector2(cf3.x, cf3.z);
            //float dot = Vector2.Dot(cf2, Vector2.up);
            //if (dot < 0)
            //{
            //    if (turnType == TurnType.Right)
            //    {
            //        turnType = TurnType.Left;
            //    }
            //    else if (turnType == TurnType.Left)
            //    {
            //        turnType = TurnType.Right;
            //    }
            //}
            ////Debug.LogError("DirByJoyDir " + turnType);
            moveObject.SetTurnType(turnType);
        }

        /// <summary>
        /// 根据JoyDir的x的正负确定转向
        /// </summary>
        /// <param name="tankCtr"></param>
        /// <param name="JoyDir"></param>
        void DirByJoyDirX(F64Vec3 JoyDir)
        {
            //得到新方向和旧方向的左边还是右边
            TurnType turnType;
            if (JoyDir.X > F64.Zero)
            {
                turnType = TurnType.Right;
            }
            else if (JoyDir.X < F64.Zero)
            {
                turnType = TurnType.Left;
            }
            else
            {
                turnType = TurnType.None;
            }

            moveObject.SetTurnType(turnType);
        }
    }



}
