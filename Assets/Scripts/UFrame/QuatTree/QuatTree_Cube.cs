using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UFrame.QuadTree;

public class QuatTree_Cube : MonoBehaviour, IQuadTreeBody
{
    static int sid = 0;
    int id = 0;
    float speed = 5;

    private void Awake()
    {
        id = sid++;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (quadTree == null)
        {
            return;
        }

        bounds = quadTree._bounds;
        Move();


    }

    public Vector2 Position { get { return new Vector2(transform.position.x, transform.position.z); } }
    public bool QuadTreeIgnore { get { return false; } }
    public QuadTree quadTree { get; set; }
    public int ID { get { return id; } }
    public Rect bounds;

    Vector3 vDir;
    Vector3 force;
    private void Move()
    {
        bounds = quadTree._bounds;
        float delta = Time.deltaTime * speed;
        int dir = Random.Range(0, 3);
        switch (dir)
        {
            case 0:
                vDir = new Vector3(0, 0, 1);
                break;
            case 1:
                vDir = new Vector3(1, 0, 0);
                break;
            case 2:
                vDir = new Vector3(0, 0, -1);
                break;
            case 3:
                vDir = new Vector3(-1, 0, 0);
                break;
        }
        force = delta * vDir;
        var tmp = transform.position + force;
        if (tmp.x > 200 || tmp.x < 0 || tmp.z > 200 || tmp.z < 0)
        {
            return;
        }
        transform.position += force;

        //移动后还在原来的节点所在范围，保持在所在节点
        if (bounds.Contains(new Vector2(transform.position.x, transform.position.z)))
        {
            return;
        }

        //移动后不在原来的节点所在范围，从所在节点移除，并重新插入根节点，让其分配到子节点
        quadTree._bodies.Remove(this);
        var root = GetQTRoot(quadTree);
        root.AddBody(this);
        bounds = quadTree._bounds;
    }

    QuadTree GetQTRoot(QuadTree quadTree)
    {
        if(quadTree._parent == null)
        {
            return quadTree;
        }

        return GetQTRoot(quadTree._parent);

    }
}
