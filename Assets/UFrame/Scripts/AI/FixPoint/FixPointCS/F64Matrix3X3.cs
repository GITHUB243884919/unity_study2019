using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FixPointCS;

namespace FixMath
{
    public class F64Matrix3x3
    {
        private class Matrix
        {
            public F64 _11, _12, _13;
            public F64 _21, _22, _23;
            public F64 _31, _32, _33;

            public Matrix()
            {
                _11 = F64.Zero; _12 = F64.Zero; _13 = F64.Zero;
                _21 = F64.Zero; _22 = F64.Zero; _23 = F64.Zero;
                _31 = F64.Zero; _32 = F64.Zero; _33 = F64.Zero;
            }

        };

        Matrix m_Matrix = new Matrix();

        void _11(F64 val) { m_Matrix._11 = val; }
        void _12(F64 val) { m_Matrix._12 = val; }
        void _13(F64 val) { m_Matrix._13 = val; }

        void _21(F64 val) { m_Matrix._21 = val; }
        void _22(F64 val) { m_Matrix._22 = val; }
        void _23(F64 val) { m_Matrix._23 = val; }

        void _31(F64 val) { m_Matrix._31 = val; }
        void _32(F64 val) { m_Matrix._32 = val; }
        void _33(F64 val) { m_Matrix._33 = val; }


        public void TransformVector2Ds(ref F64Vec2 vPoint)
        {
            //          AgentHeading.X               AgentHeading.Y             Dot(-AgentPosition, AgentHeading)
            F64 tempX = (m_Matrix._11 * vPoint.X) + (m_Matrix._21 * vPoint.Y) + (m_Matrix._31);

            F64 tempY = (m_Matrix._12 * vPoint.X) + (m_Matrix._22 * vPoint.Y) + (m_Matrix._32);

            vPoint.X = tempX;

            vPoint.Y = tempY;
        }

        //--------------------- PointToLocalSpace --------------------------------
        //
        //------------------------------------------------------------------------
        public F64Vec2 PointToLocalSpace(F64Vec2 point,
                                F64Vec2 AgentHeading,
                                F64Vec2 AgentSide,
                                F64Vec2 AgentPosition)
        {

            //make a copy of the point
            F64Vec2 TransPoint = point;

            //create a transformation matrix
            F64Matrix3x3 matTransform = new F64Matrix3x3();

            F64 Tx = F64Vec2.Dot(-AgentPosition, AgentHeading);
            F64 Ty = F64Vec2.Dot(-AgentPosition, AgentSide);

            //create the transformation matrix
            matTransform._11(AgentHeading.X); matTransform._12(AgentSide.X);
            matTransform._21(AgentHeading.Y); matTransform._22(AgentSide.Y);
            matTransform._31(Tx);           matTransform._32(Ty);
	
            //now transform the vertices
            matTransform.TransformVector2Ds(ref TransPoint);

            return TransPoint;
        }

    }

}