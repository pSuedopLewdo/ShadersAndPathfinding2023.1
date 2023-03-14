using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : Dijkstra
{
    private void GetAllNodes()
    {
        _nodes = FindObjectsOfType<Node>();
    }

    private new List<Node> FindShortestPath(Node start, Node end)
    {
        GetAllNodes();
        if (RunAlgorithm(start, end))
        {
            List<Node> result = new List<Node>();
            Node currentNode = end;
            do
            {
                result.Insert(0, currentNode);
                currentNode = currentNode.PreviousNode;
            } while (currentNode != null);

            return result;
        }

        return null;
    }

    private void Start()
    {
        List<Node> path = FindShortestPath(StartNode, EndNode);


        for (int index = 0; index < path.Count - 1; index++)
        {
            Debug.DrawLine(path[index].transform.position + Vector3.up, path[index + 1].transform.position + Vector3.up,
                Color.red, 10f);
            Debug.Log(path[index].gameObject.name);
        }

        Debug.Log(path[path.Count - 1].gameObject.name);

    }

    /// <summary>
    ///  Dijkstra path finding
    /// </summary>
    /// <param name="startWaypoint"></param>
    /// <param name="endWaypoint"></param>
    /// <returns>True if path is found</returns>
    protected override bool RunAlgorithm(Node startWaypoint, Node endWaypoint)
    {
        var unexplored = new List<Node>();
        Node start = null;
        Node end = null;

        foreach (var obj in _nodes)
        {
            var node = obj.GetComponent<Node>();
            if (node == null) continue;
            //TODO: reset node from last search here
            node.ResetNode(endWaypoint.transform.position);
            unexplored.Add(node);

            if (startWaypoint == obj) start = node;
            if (endWaypoint == obj) end = node;
        }

        if (start == null && end == null)
        {
            return false;
        }

        if (start != null) start.PathWeight = 0;

        while (unexplored.Count > 0)
        {
            unexplored.Sort((a, b) => a.PathWeight.CompareTo(b.PathWeight));

            var current = unexplored[0];
            unexplored.RemoveAt(0);





            foreach (var neighbourNode in current.neighbours)
            {
                if (!unexplored.Contains(neighbourNode)) continue;



                var possibleNeighbourWeight =
                    Vector3.Distance(neighbourNode.transform.position, current.transform.position);

                possibleNeighbourWeight += current.PathWeight;

                if (possibleNeighbourWeight < neighbourNode.PathWeight)
                {
                    neighbourNode.PathWeight = possibleNeighbourWeight;
                    //TODO: neighbourNode.previousNode = current
                    neighbourNode.PreviousNode = current;
                }
            }

            if (current == end) break; //We have found the shortest path, stop looping through unexplored
        }

        return true;
    }
}