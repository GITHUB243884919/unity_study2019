using UnityEngine;
using UFrame.ResourceManagement;

public class TerrainNiceTrunkTest : MonoBehaviour
{
    void LateUpdate()
    {
        if (transform.position.x > 25
            && transform.position.x < 175
            && transform.position.z > 25 &&
            transform.position.z < 175)
        {
            TerrainManager.GetInstance().SetTrunkEdgeNum(3);
            TerrainManager.GetInstance().LoadSlicingMapTileAsync("terrain", transform.position);
        }

    }
    
}