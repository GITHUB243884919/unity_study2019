using FixPointCS;
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
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static F64Vec3 RotateY(F64Vec3 lhs, F64 angle)
        {
            //x1＝xcosθ + ysinθ, y1＝-xsinθ + ycosθ
            F64 rad = F64.DegToRad2(angle);
            F64 cos = F64.Cos(rad);
            F64 sin = F64.Sin(rad);
            F64 x = lhs.X * cos + lhs.Z * sin;
            F64 y = F64.Zero;
            F64 z = -lhs.X * sin + lhs.Z * cos;

            return new F64Vec3(x, y, z);
        }

        public Vector3 ToUnityVector3()
        {
            return new Vector3(X.Float, Y.Float, Z.Float);
        }

        public static F64Vec3 FromUnityVector3(Vector3 v)
        {
            return new F64Vec3(v.x, v.y, v.z);
        }
    }

}
