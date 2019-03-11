using System.Collections.Generic;
using UnityEngine;
using UFrame.QuadTree;

public class QuatTree_Test : MonoBehaviour
{


    public QuatTree_Cube DemoPhysicsBody;

    [Header("QuadTree Settings")]
    public Vector2 WorldSize = new Vector2(200, 200);
    public int BodiesPerNode = 4;
    public int MaxSplits = 6;

    public QuadTree _quadTree;
    public int MaxBodies = 6;
    private void Start()
    {
        _quadTree = new QuadTree(new Rect(0, 0, WorldSize.x, WorldSize.y), BodiesPerNode, MaxSplits);
        for (int i = 0; i < MaxBodies; i++)
        {
            var body = GameObject.Instantiate<QuatTree_Cube>(DemoPhysicsBody);
            body.transform.position = new Vector3(Random.Range(0, WorldSize.x), 0, Random.Range(0, WorldSize.y));

            _quadTree.AddBody(body); // add body to QuadTree
        }
    }

    private void Update()
    {

    }

    private void OnDrawGizmos()
    {
        if (_quadTree == null) return;
        _quadTree.DrawGizmos();
    }

}
