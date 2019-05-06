using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UFrame.ResourceManagement
{
    public class TerrainSlicingData : ScriptableObject
    {
        [SerializeField]
        public string terrainName;
        [SerializeField]
        public int slicingSize;
        [SerializeField]
        public Vector3 terrainSize;
    }
}

