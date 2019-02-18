﻿using System;
using FixPointCS;

namespace FixMath
{
    public partial struct F64 : IComparable<F64>, IEquatable<F64>
    {
        public static F64 F180 = new F64(180);

        public static F64 F1000 = new F64(1000);

        /// <summary>
        /// a是欧拉角，转弧度
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static F64 DegToRad2(F64 a) { return a * Pi / F180; }

        /// <summary>
        /// a是弧度，转欧拉角
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static F64 RadToDeg2(F64 a) { return a * F180 / Pi; }

    }

}
