using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UFrame.ResourceManagement;

public class UIReturnHome : MonoBehaviour
{

    public Button btn_Return;
	// Use this for initialization
	void Start ()
    {
        btn_Return.onClick.AddListener(() => {
            ResHelper.LoadScene("scenes/home");
        });

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
