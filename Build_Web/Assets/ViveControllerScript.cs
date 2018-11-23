using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViveControllerScript : MonoBehaviour {

    public Camera cam;

	// Use this for initialization
	void Start () {
		
	}

    private void FixedUpdate()
    {
        Vector3 temp = new Vector3();
        temp = Input.mousePosition;
        
        //Debug.Log("Input:" + temp);
        temp.z = 1000;
        temp = cam.ScreenToWorldPoint(temp);
        //Debug.Log("Camera:" + temp);

        transform.LookAt(temp);
    }
}
