using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Points", menuName = "PointList", order = 1)]
public class PointData : ScriptableObject
{
    public List<Vector2> points = new List<Vector2>();
}
