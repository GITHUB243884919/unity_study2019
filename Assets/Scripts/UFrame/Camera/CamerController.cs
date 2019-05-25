using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerController : MonoBehaviour
{
    public float distance;
    Vector3 cameraPosition = Vector3.zero;

    // Use this for initialization
    void Start () {
		
	}

    void Update()
    {
        //m_CameraPosition = transform.position - Camera.main.transform.forward * distance;
        //Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, m_CameraPosition, Time.deltaTime * 3f);

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //m_Agent.SetDestination(hit.point);
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = hit.point;
                //cameraPosition = hit.point - Camera.main.transform.forward * distance;
                cameraPosition = Camera.main.transform.forward * distance -  hit.point;
                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, cameraPosition, Time.deltaTime * 3f);
            }
            
        }
    }
}
