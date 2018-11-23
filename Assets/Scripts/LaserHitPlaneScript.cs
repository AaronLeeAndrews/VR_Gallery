using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHitPlaneScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

    }

    private void FixedUpdate()
    {
        Vector3 posTemp = new Vector3();
        posTemp = Input.mousePosition;
        
        posTemp.z = 1000;
        posTemp = Camera.main.ScreenToWorldPoint(posTemp);
        posTemp.z = -3;
        transform.position = (posTemp);
    }
}
