using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamBehavior : MonoBehaviour {

    bool isClicked;
    Vector2 prevPosMouse, currPosMouse;
    double rotSpeedScale;

	// Use this for initialization
	void Start () {
        isClicked = false;
        rotSpeedScale = 1;
	}
	
	// Update is called once per frame
	void Update () {
        //if(isClicked)
        {
            gameObject.GetComponent<Transform>().Rotate(new Vector3(0/*(prevPosMouse.y - currPosMouse.y)*0.1f*/, (currPosMouse.x - prevPosMouse.x), 0));
            prevPosMouse = currPosMouse;
            currPosMouse = Input.mousePosition;
            //Debug.Log("Mouse is down!");
        }

        Debug.Log(Input.mousePosition);
    }

    private void OnMouseDown()
    {
        prevPosMouse = Input.mousePosition;
        isClicked = true;
    }

    private void OnMouseUp()
    {
        isClicked = false;
    }

    private void OnMouseDrag()
    {
        gameObject.GetComponent<Transform>().Rotate(new Vector3((currPosMouse.x - prevPosMouse.x), (currPosMouse.y - prevPosMouse.y), 0));
    }
}
