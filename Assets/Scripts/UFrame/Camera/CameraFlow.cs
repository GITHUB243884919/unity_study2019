using UnityEngine;
using System.Collections;

public class CameraFlow : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;

    void Start()
    {
        //设置相对偏移
        offset = target.position - this.transform.position;
    }

    //void Update()
    //{
    //    this.transform.position = target.position - offset;
    //}

    void LateUpdate()
    {
        this.transform.position = target.position - offset;
    }
}