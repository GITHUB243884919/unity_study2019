using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour {

    // Use this for initialization
    Animation anim = null;
	void Start ()
    {

        anim = GetComponent<Animation>();
        //anim.Play("run");

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
