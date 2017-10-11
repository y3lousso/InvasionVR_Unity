using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathEditor : MonoBehaviour
{

    public BezierSpline splineToFollow;
    public int splineCuts = 20;
    public List<Vector3> waypoints;
    public List<float> distanceBetweenWaypoints;

    // Use this for initialization
    void Start()
    {
        waypoints = new List<Vector3>();
        for (int i = 0; i <= splineCuts; i++)
        {
            waypoints.Add(splineToFollow.GetPoint(((float)i / splineCuts)));
        }
        distanceBetweenWaypoints = new List<float>();
        for (int i = 0; i < waypoints.Count - 1; i++)
        {
            distanceBetweenWaypoints.Add(Vector3.Distance(waypoints[i + 1], waypoints[i]));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

}
