using System;
using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using UnityEngine;

public class Dijkstra : MonoBehaviour
{
    private Waypoint[] _nodes;

    public void Awake()
    {
        GetAllNodes();
    }

    public void GetAllNodes()
    {
        _nodes = FindObjectsOfType<Waypoint>();
    }

    /// <summary>
    ///  Dijkstra path finding
    /// </summary>
    /// <param name="start"> Start Node</param>
    /// <param name="end"> End Node</param>
    /// <returns>True if path is found</returns>
    private bool DijkstraAlgorithm(Waypoint startWaypoint, Waypoint endWaypoint)
    {
        List<Node> unexplored = new List<Node>();
        Node start = null;
        Node end = null;

        foreach(Waypoint obj in _nodes)
        {
            Node node = new Node(obj);
            unexplored.Add(node);

            if (startWaypoint == obj) start = node;
            if (endWaypoint == obj) end = node;
        }

        if (start == null && end == null)
        {
            return false;
        }

        start.PathWeight = 0;

        while (unexplored.Count > 0)
        {
            unexplored.Sort((a,b) => a.PathWeight.CompareTo(b.PathWeight));

            Node current = unexplored[0];
            unexplored.RemoveAt(0);

            if (current == end) break; //We have found the shortest path, stop looping through unexplored

            /*foreach (Node neighbourNode in current.NeighbourNodes)
            {
                
            }*/
            
        }
        
        return true;
    }
}