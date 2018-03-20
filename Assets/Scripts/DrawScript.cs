using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawScript : MonoBehaviour {

    public GameObject point;
    public GameObject wall;

    private List<GameObject> points = new List<GameObject>();

    private GameObject text;
    private GameObject startPoint;
    private Vector3 initMouse;
    private Vector3 mouseTrack;
    private Vector3 mouseDist;
    private Transform wallScale;
    private bool trackMouse = false;
    private int wallCount = 0;

	// Use this for initialization
	void Start () {
        text = GameObject.Find("Text");
    }
	
	// Update is called once per frame
	void Update () {

        
        if (Input.GetMouseButtonDown(0))
        {
            initMouse = Input.mousePosition;
            initMouse.z = 9.5f;
            initMouse = Camera.main.ScreenToWorldPoint(initMouse);
            mouseTrack = initMouse;
            CreatePoint();
            CreateWall();
            trackMouse = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            
            trackMouse = false;
            Debug.Log("Mouse moved " + mouseDist.magnitude + " while button was down.");
            text.GetComponent<TextMesh>().text = "";
            CreatePoint();
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

            text.transform.position = new Vector3(mouseTrack.x + 0.5f, mouseTrack.y + 0.3f, mouseTrack.z);
            text.GetComponent<TextMesh>().text = mouseDist.magnitude.ToString().Substring(0,5) + "m";
        }

    }

    void CreatePoint()
    {
        GameObject pointPrefab = Instantiate(point, mouseTrack, Quaternion.identity);
        if (points.Count == 0)
            startPoint = pointPrefab;

        pointPrefab.name = "Point" + points.Count;
        points.Add(pointPrefab);
    }

    void CreatePoints(Vector3 pos)
    {
        GameObject pointPrefab = Instantiate(point, pos, Quaternion.identity);
        pointPrefab.name = "Point" + points.Count;
        points.Add(pointPrefab);
    }

    void CreateWall()
    {
        GameObject wallPrefab = Instantiate(wall, mouseTrack, Quaternion.identity);
        wallPrefab.name = "Wall" + wallCount;
        wallScale = wallPrefab.transform;
    }
}
