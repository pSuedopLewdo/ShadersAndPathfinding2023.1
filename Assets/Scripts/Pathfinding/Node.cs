using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    //field
    private float _pathWeight = int.MaxValue;
    //property
    public float PathWeight
    {
        get => _pathWeight;
        set {
            _pathWeight = value; 
            Debug.Log("HI");
        }
    }

    public float PathWeightDistance => _pathWeight + Vector3.Distance(transform.position, _endPos);

    private Vector3 _endPos;
    private float _distanceToEnd;

    //private Node _waypointObj = null;

    public Node PreviousNode { get; set; }

    /*public Node(Node waypointObj)
    {
        _waypointObj = waypointObj;
    }*/
    [SerializeField] public List<Node> neighbours;

    public void ResetNode(Vector3 endPos)
    {
        _pathWeight = int.MaxValue;
        PreviousNode = null;
        _endPos = endPos;
        _distanceToEnd = Vector3.Distance(transform.position, _endPos);
    }
    
    private void OnValidate()
    {
        ValidateNeighbours();
    }

    private void ValidateNeighbours()
    {
        foreach (var waypoint in neighbours)
        {
            if(waypoint == null) continue;

            if (!waypoint.neighbours.Contains(this))
            {
                waypoint.neighbours.Add(this);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (neighbours == null) return;
        foreach (Node neighbour in neighbours)
        {
            if( neighbour == null) continue;
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, neighbour.transform.position);
        }
    }
}
