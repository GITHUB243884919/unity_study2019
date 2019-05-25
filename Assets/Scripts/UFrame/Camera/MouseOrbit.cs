using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOrbit : MonoBehaviour
{


    //偏移量和中心店
    Vector3 offest;
    Vector3 target;

    //记录第一坐标
    Vector3 P1;

    float distance = 15.0f;

    float xSpeed = 250.0f;
    float ySpeed = 120.0f;

    float x = 0.0f;
    float y = 0.0f;

    //相机移动速度
    float Speed = 40.0f;

    // Use this for initialization
    void Start()
    {

        var angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        //旋转中心初始点
        target = new Vector3(0, 0, 0);
    }

    private void Update()
    {
    }
    void LateUpdate()
    {
        if (Input.GetMouseButton(0))
        {

            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            //返回一个四元数 绕某个轴旋转某个角度
            var rotation = Quaternion.Euler(y, x, 0);
            var position = rotation * new Vector3(0.0f, 0.0f, -distance) + target;

            transform.rotation = rotation;
            transform.position = position;

        }
        else if (Input.GetMouseButton(1))
        {
            
            float x;
            float y;
            x = Input.GetAxis("Mouse X");
            y = Input.GetAxis("Mouse Y");
            transform.Translate(new Vector3(-x, -y, 0) * Time.deltaTime * Speed);
            //print("转换过的：" + Camera.main.ScreenToWorldPoint(new Vector3(x, y, 0)));

        }


        if (Input.GetMouseButtonDown(1))
        {

            P1 = transform.position;

        }
        if (Input.GetMouseButtonUp(1))
        {

            ////利用射线检测来获取屏幕中心点坐标
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider)
                {
                    target = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                    distance = (hit.point - transform.position).magnitude;
                    print(hit.collider.name);
                }
            }
            else
            {

                offest = transform.position - P1;
                target = target + offest;
                distance = (target - transform.position).magnitude;
            }


        }

    }
}
