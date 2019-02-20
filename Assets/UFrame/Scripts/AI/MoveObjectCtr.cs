using System.Collections;
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
        public override void Tick(int deltaTimeMS)
        {
            F64 fdeltaTime = new F64(deltaTimeMS);
            F64 f1000 = F64.F1000;
            if (moveObject.couldTurn)
            {
                F64 fAngle = moveObject.GetTurnSpeed() * fdeltaTime / f1000;
                if (moveObject.GetTurnType() == TurnType.Left)
                {
                    fAngle = -fAngle;
                    
                }
                ////原来的朝向在XZ平面饶Y旋转angle, angle是欧拉角
                //Vector3 newDir = Quaternion.AngleAxis((float)angle, Vector3.up) * moveObject.GetDir();
                //newDir.Normalize();
                //moveObject.SetDir(newDir);

                F64Vec3 newFdir = F64Vec3.RotateY(moveObject.GetDir(), fAngle);
                F64Vec3 newNfdir = F64Vec3.Normalize(newFdir);
                moveObject.SetDir(newNfdir);
            }

            if (moveObject.couldMove)
            {
                F64 fdelta = moveObject.GetSpeed() * fdeltaTime / f1000;
                F64Vec3 fOldPos = moveObject.GetPos();
                moveObject.SetPos(fOldPos + fdelta * moveObject.GetDir());
            }

            F64Vec3 check = new F64Vec3(0, 0, 100);
            Debug.LogError("to local " + moveObject.PointToLocalSpace2D(check));

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
            //当相机旋转的时候，考虑朝向是否的相反，即是眼睛看到的向左就向左
            //如果不考虑，那么当相机面朝角色时，摇杆的左右操作和视觉上是相反的。
            Vector3 cf3 = Camera.main.transform.forward;
            Vector2 cf2 = new Vector2(cf3.x, cf3.z);
            float dot = Vector2.Dot(cf2, Vector2.up);
            if (dot < 0)
            {
                if (turnType == TurnType.Right)
                {
                    turnType = TurnType.Left;
                }
                else if (turnType == TurnType.Left)
                {
                    turnType = TurnType.Right;
                }
            }
            Debug.LogError("DirByJoyDir " + turnType);
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
            if (JoyDir.X.Float > 0)
            {
                turnType = UFrame.AI.TurnType.Right;
            }
            else if (JoyDir.X.Float < 0)
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
