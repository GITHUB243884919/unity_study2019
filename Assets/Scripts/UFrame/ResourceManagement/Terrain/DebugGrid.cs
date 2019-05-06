using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UFrame.ResourceManagement
{
    public class DebugGrid : MonoBehaviour
    {
        public int width = 200;
        public int perCellWidth = 25;
        public void OnDrawGizmos()
        {
            int Num = width / perCellWidth;
            for (int i = 0; i <= Num; i++)
            {
                Gizmos.color = Color.blue;
                int temp = perCellWidth * i;

                Vector3 begin = new Vector3(0, 0, temp);
                Vector3 end = new Vector3(width, 0, temp);
                Gizmos.DrawLine(begin, end);

                begin = new Vector3(temp, 0, 0);
                end = new Vector3(temp, 0, width);
                Gizmos.DrawLine(begin, end);
            }
        }
    }
}

