﻿using FixPointCS;
using System;
using UnityEngine;

namespace FixMath
{

    public partial struct F64Vec3 : IEquatable<F64Vec3>
    {
        public F64Vec3(float x, float y, float z)
        {
            this.RawX = F64.FromFloat(x).Raw;
            this.RawY = F64.FromFloat(y).Raw;
            this.RawZ = F64.FromFloat(z).Raw;
        }

        /// <summary>
        /// from 到 to 的夹角0,180
        /// from 到 to 必须是单位向量
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static F64 Angle(F64Vec3 from, F64Vec3 to)
        {
            F64 dot = Dot(from, to);
            F64 angle = F64.Acos(dot);

            return F64.RadToDeg2(angle);
        }

        /// <summary>
        /// lhs 是单位向量
        /// angle 是旋转的欧拉角
        /// 乘以旋转矩阵
        /// Y 轴旋转 x1 = xcosθ + zsinθ, y1=y, z1＝-xsinθ + zcosθ
        /// X 轴旋转 x1 = x, y1=ycos - zsin, z1 = ysin + zcos 
        /// Z 轴旋转 x1 = xcos - ysin, y1=xsin+ycos, z1=z
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static F64Vec3 RotateY(F64Vec3 lhs, F64 angle)
        {
            F64 rad = F64.DegToRad2(angle);
            F64 cos = F64.Cos(rad);
            F64 sin = F64.Sin(rad);
            F64 x = lhs.X * cos + lhs.Z * sin;
            F64 y = lhs.Y;
            F64 z = -lhs.X * sin + lhs.Z * cos;

            return new F64Vec3(x, y, z);
        }

        public static F64Vec3 RotateZ(F64Vec3 lhs, F64 angle)
        {
            //Z 轴旋转 x1 = xcos - ysin, y1=xsin+ycos, z1=z
            F64 rad = F64.DegToRad2(angle);
            F64 cos = F64.Cos(rad);
            F64 sin = F64.Sin(rad);
            F64 x = lhs.X * cos - lhs.Y * sin;
            F64 y = lhs.X * sin + lhs.Y * cos;
            F64 z = lhs.Z;

            return new F64Vec3(x, y, z);
        }

        /// <summary>
        /// XZ平面下把p转到局部坐标，后三个参数是2d坐标轴定义参数
        /// </summary>
        /// <param name="p"></param>
        /// <param name="forward"></param>
        /// <param name="left"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        public static F64Vec3 PointToLocalSpace2D(F64Vec3 p, F64Vec3 forward, F64Vec3 left, F64Vec3 o)
        {
            var p2 = new F64Vec2(p.X, p.Z);
            var f2 = new F64Vec2(forward.X, forward.Z);
            var l2 = new F64Vec2(left.X, left.Z);
            var o2 = new F64Vec2(o.X, o.Z);
            var X = F64Vec2.Dot(f2, p2) - F64Vec2.Dot(o2, f2);
            var Z = F64Vec2.Dot(l2, p2) - F64Vec2.Dot(o2, l2);
            return new F64Vec3(X, p.Y, Z);
        }

        /// <summary>
        /// XZ平面下把p从局部坐标转到世界坐标，后三个参数是2d坐标轴定义参数
        /// </summary>
        /// <param name="p"></param>
        /// <param name="forward"></param>
        /// <param name="left"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        public static F64Vec3 PointToWorldSpace2D(F64Vec3 p, F64Vec3 forward, F64Vec3 left, F64Vec3 o)
        {
            var X = forward.X * p.X + left.X * p.Z + o.X;
            var Z = forward.Z * p.X + left.Z * p.Z + o.Z;
            return new F64Vec3(X, p.Y, Z);
        }

        /// <summary>
        /// XZ平面下把向量p从局部坐标转到世界坐标，后三个参数是2d坐标轴x，z
        /// </summary>
        /// <param name="p"></param>
        /// <param name="forward"></param>
        /// <param name="left"></param>
        /// <returns></returns>
        public static F64Vec3 VectorToWorldSpace2D(F64Vec3 p, F64Vec3 forward, F64Vec3 left)
        {
            var X = forward.X * p.X + left.X * p.Z;
            var Z = forward.Z * p.X + left.Z * p.Z;
            return new F64Vec3(X, p.Y, Z);
        }

        public static F64Vec3 LineIntersectionCircle()
        {

            ////now to do a line/circle intersection test. The center of the 
            ////circle is represented by (cX, cY). The intersection points are 
            ////given by the formula x = cX +/-sqrt(r^2-cY^2) for y=0. 
            ////We only need to look at the smallest positive value of x because
            ////that will be the closest point of intersection.
            //double cX = LocalPos.x;
            //double cY = LocalPos.y;

            ////we only need to calculate the sqrt part of the above equation once
            //double SqrtPart = sqrt(ExpandedRadius * ExpandedRadius - cY * cY);

            //double ip = cX - SqrtPart;

            //if (ip <= 0.0)
            //{
            //    ip = cX + SqrtPart;
            //}

            return F64Vec3.Zero;
        }

        public Vector3 ToUnityVector3()
        {
            return new Vector3(X.Float, Y.Float, Z.Float);
        }

        public static F64Vec3 FromUnityVector3(Vector3 v)
        {
            return new F64Vec3(v.x, v.y, v.z);
        }

        public F64Vec2 ToF64Vec2()
        {
            return new F64Vec2(X, Z);
        }

        public static F64Vec3 FromF64Vec2(F64Vec2 v)
        {
            return new F64Vec3(v.X, F64.Zero, v.Y);
        }
    }

}
