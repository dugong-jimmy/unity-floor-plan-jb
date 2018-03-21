using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawScript : MonoBehaviour {

    public GameObject point;
    public GameObject wall;
    public GameObject fill;

    private List<GameObject> points = new List<GameObject>();

    private GameObject lengthText;
    private GameObject areaText;
    private GameObject startPoint;
    private Vector3 initMouse;
    private Vector3 mouseTrack;
    private Vector3 mouseDist;
    private Transform wallScale;
    private bool trackMouse = false;
    private bool roomEnd = false;
    private int wallCount = 0;
    private float area;

	// Use this for initialization
	void Start () {
        lengthText = GameObject.Find("LengthText");
        areaText = GameObject.Find("AreaText");
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
            Debug.Log("Mouse moved " + mouseDist.magnitude);
            lengthText.GetComponent<TextMesh>().text = "";

            //CreatePoint();
            wallCount++;

            if (roomEnd)
            {
                areaText.GetComponent<TextMesh>().text = area.ToString().Substring(0, 5) + "m2";
                FillArea();
                Debug.Log(area);
            }
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

            lengthText.transform.position = new Vector3(mouseTrack.x + 0.5f, mouseTrack.y + 0.3f, mouseTrack.z);
            lengthText.GetComponent<TextMesh>().text = mouseDist.magnitude.ToString().Substring(0,5) + "m";
        }

    }

    void CreatePoint()
    {
        GameObject pointPrefab = Instantiate(point, mouseTrack, Quaternion.identity);
        if (points.Count == 0)
        {
            startPoint = pointPrefab;
            pointPrefab.name = "StartPoint";
        }
        else
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

    public void CalcArea()
    {
        float temp = 0;

        if (points.Count > 2)
        {
            roomEnd = true;
        }

        for (int i = 0; i < points.Count; i++)
        {
            if (i != points.Count - 1)
            {
                float prod1 = points[i].transform.position.x * points[i + 1].transform.position.y;
                float prod2 = points[i + 1].transform.position.x * points[i].transform.position.y;
                temp = temp + (prod1 - prod2);
            }
            else
            {
                float prod1 = points[i].transform.position.x * points[0].transform.position.y;
                float prod2 = points[0].transform.position.x * points[i].transform.position.y;
                temp = temp + (prod1 - prod2);
            }
        }

        temp *= 0.5f;
        area = Mathf.Abs(temp);
    }

    void FillArea()
    {
        GameObject fillPrefab = Instantiate(fill, startPoint.transform.position, Quaternion.identity);

        Vector3[] verticies = fillPrefab.GetComponent<MeshFilter>().mesh.vertices;

        verticies[0] = new Vector3(points[0].transform.position.x, points[0].transform.position.y, points[0].transform.position.z);
        verticies[1] = new Vector3(points[2].transform.position.x, points[2].transform.position.y, points[2].transform.position.z);
        verticies[2] = new Vector3(points[3].transform.position.x, points[3].transform.position.y, points[3].transform.position.z);
        verticies[3] = new Vector3(points[1].transform.position.x, points[1].transform.position.y, points[1].transform.position.z);

        Vector3 center = (points[0].transform.position + points[1].transform.position + points[2].transform.position + points[3].transform.position) / 4;

        fillPrefab.transform.position = center;
        fillPrefab.GetComponent<MeshFilter>().mesh.vertices = verticies;
    }
}
