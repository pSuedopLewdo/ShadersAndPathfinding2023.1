using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    //field
    private float _pathWeight = int.MaxValue;
    //property
    public float PathWeight
    {
        get { return _pathWeight; }
        set {
            _pathWeight = value; 
            Debug.Log("HI");
        }
    }

    private Waypoint _waypointObj = null;

    public Node(Waypoint waypointObj)
    {
        _waypointObj = waypointObj;
    }
    
}
