using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawScript : MonoBehaviour {

    public GameObject point;
    public GameObject wall;

    private List<GameObject> points = new List<GameObject>();

    private Vector3 initMouse;
    private Vector3 mouseTrack;
    private Vector3 mouseDist;
    private Transform wallScale;
    private bool trackMouse = false;
    private int wallCount = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        
        if (Input.GetMouseButtonDown(0))
        {
            initMouse = Input.mousePosition;
            initMouse.z = 9.5f;
            initMouse = Camera.main.ScreenToWorldPoint(initMouse);
            mouseTrack = initMouse;
            //Create corner point and wall prefab. Add corner to list 
            GameObject pointPrefab =  Instantiate(point, mouseTrack, Quaternion.identity);
            GameObject wallPrefab = Instantiate(wall, mouseTrack, Quaternion.identity);
            pointPrefab.name = "Point" + points.Count;
            wallPrefab.name = "Wall" + wallCount;
            wallScale = wallPrefab.transform;
            points.Add(pointPrefab);
            
            trackMouse = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            trackMouse = false;
            Debug.Log("Mouse moved " + mouseDist + " while button was down.");
            GameObject pointPrefab = Instantiate(point, mouseTrack, Quaternion.identity);
            pointPrefab.name = "Point" + points.Count;
            points.Add(pointPrefab);
            wallCount++;
        }
        
        if (trackMouse)
        {
            mouseTrack = Input.mousePosition;
            mouseTrack.z = 9.5f;
            mouseTrack = Camera.main.ScreenToWorldPoint(mouseTrack);
            if (initMouse != mouseTrack)
            {
                mouseDist = mouseTrack - initMouse;
                string currWallStr = "Wall" + wallCount;
                GameObject currWall = GameObject.Find(currWallStr);
                currWall.transform.position = initMouse + (mouseDist) / 2.0f;
                currWall.transform.localScale = new Vector3(wallScale.localScale.x, mouseDist.magnitude / 1.0f, wallScale.localScale.z);
                currWall.transform.rotation = Quaternion.FromToRotation(Vector3.up, mouseDist);
            }
        }

    }
}
