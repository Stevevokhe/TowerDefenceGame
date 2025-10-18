using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    [SerializeField] public List<Transform> waypoints;
    private Transform nextWaypoint;
    private int currentWaypoint=-1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // Initlialize the list 
        waypoints = new List<Transform>();
        
        // Fill up the list
        foreach(Transform waypoint in transform)
        {
            waypoints.Add(waypoint);
        }
    }

    public Transform NextWaypoint()
    {
        if (currentWaypoint+1 >= waypoints.Count) {
            currentWaypoint = -1;
        }
        nextWaypoint = waypoints[currentWaypoint+1];
        currentWaypoint++;

        return nextWaypoint;
    }
}
