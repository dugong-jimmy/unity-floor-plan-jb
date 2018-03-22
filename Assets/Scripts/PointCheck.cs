using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCheck : MonoBehaviour {
    private GameObject drawObj;
    private DrawScript drawScript;

    // Use this for initialization
    void Start () {
        drawObj = GameObject.Find("Main Camera");
        drawScript = drawObj.GetComponent<DrawScript>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseEnter()
    {
        if (name == "StartPoint")
        {
            //Debug.Log("Mouse over point");
            drawScript.CalcArea();
        }
    }
}
