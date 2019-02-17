using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial struct FixVector3
{
    public FixVector3(float x, float y, float z)
    {
        this.x = (Fix64)x;
        this.y = (Fix64)y;
        this.z = (Fix64)z;
    }

    public static Fix64 Angle(FixVector3 from, FixVector3 to)
    {
        Fix64 dot = Dot(from, to);
        Fix64 sin = Fix64.One - Fix64.Pow(dot, 2);
        sin = Fix64.Sqrt(sin);
        //Fix64.Asin(sin);
        Fix64 asin = Fix64.Asin(sin);
        //Debug.LogError(asin);
        Fix64 Rad2Deg = (Fix64)360 / (Fix64)(3.1415926 * 2);

        ////return asin * (Fix64)Mathf.Rad2Deg;
        ////return asin * Fix64.Rad2Deg;
        //Fix64 tmp = asin * Fix64.Deg2Rad;

        //Debug.LogError((float)tmp);
        return asin * Rad2Deg;
    }

    public static Fix64 Dot(FixVector3 lhs, FixVector3 rhs)
    {
        return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
    }

    public static FixVector3 Cross(FixVector3 lhs, FixVector3 rhs)
    {
        Fix64 x = lhs.y * rhs.z - lhs.z * rhs.y;
        Fix64 y = lhs.z * rhs.x - lhs.x * rhs.z;
        Fix64 z = lhs.x * rhs.y - lhs.y * rhs.x;
        return new FixVector3(x, y, z);
    }

    public static FixVector3 Rotate(FixVector3 lhs, Fix64 angle)
    {
        //x1＝xcosθ + ysinθ, y1＝-xsinθ + ycosθ
        Fix64 x = lhs.x * Fix64.Cos(angle) + lhs.z * Fix64.Sin(angle);
        Fix64 y = -lhs.x * Fix64.Sin(angle) + lhs.z * Fix64.Cos(angle);
        Fix64 z = Fix64.Zero;

        return new FixVector3(x, y, z);
    }


}
