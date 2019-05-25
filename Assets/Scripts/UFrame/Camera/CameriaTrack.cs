using UnityEngine;
using System.Collections;

public class CameriaTrack : MonoBehaviour
{
    private Vector3 offset = new Vector3(0, 5, 4);//相机相对于玩家的位置
    public Transform target;
    private Vector3 pos;
    public float speed = 2;
    Quaternion targetRot;
    // Use this for initialization
    void Start()
    {
        //target = GameObject.FindGameObjectWithTag("Player").transform;
        offset = target.position - this.transform.position;
        targetRot = target.rotation;

    }

    // Update is called once per frame
    void Update()
    {
        pos = target.position + offset;
        this.transform.position = Vector3.Lerp(this.transform.position, pos, speed * Time.deltaTime);//调整相机与玩家之间的距离
        Quaternion angel = Quaternion.LookRotation(target.position - this.transform.position);//获取旋转角度
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, angel, speed * Time.deltaTime);

    }

    //void LateUpdate()
    //{
    //    //var r = this.transform.rotation / targetRot;
    //    //Quaternion angel = Quaternion.LookRotation(target.position - this.transform.position);//获取旋转角度
    //    //this.transform.rotation = Quaternion.Slerp(this.transform.rotation, angel, speed * Time.deltaTime);


    //}
}
