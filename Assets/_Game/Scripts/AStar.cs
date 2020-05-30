using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AStar : MonoBehaviour
{
    public static AStar Instance { get; private set; }

    public bool includeDiagonals;
    public float nodeDistance = 1;
    public Transform firstNode;
    public Transform lastNode;
    //public Transform testStart;
    //public Transform testGoal;
    //public Transform testParentBlocker;

    public List<Transform> blockers;
    //=> testParentBlocker.GetComponentsInChildren<Transform>().Where(x => x != testParentBlocker).ToList();

    private PathBlocker[] pathBlockers;

    //public Material pathMat;
    //public Material blockMat;

    private Dictionary<Vector2, Node> nodes;
    
    //private class Node
    //{
    //    public bool IsWalkable { get; set; }
    //    public float G { get; set; }
    //    public float H { get; set; }
    //    public float F => G + H;
    //    public float Weight { get; set; } = 1;
    //    public GameObject Obj { get; set; }
    //    public Node Parent { get; set; }
    //    public Node N { get; set; }
    //    public Node S { get; set; }
    //    public Node E { get; set; }
    //    public Node W { get; set; }
    //    public Node NE { get; set; }
    //    public Node NW { get; set; }
    //    public Node SE { get; set; }
    //    public Node SW { get; set; }
    //    public Vector2 Position { get; set; }

    //    public void ResetPathInfo()
    //    {
    //        G = 0;
    //        H = 0;
    //        Parent = null;
    //    }

    //    public List<Node> GetNeighbors()
    //    {
    //        var neighbors = new List<Node>() { N, S, E, W, NE, NW, SE, SW };
    //        return neighbors.Where(x => x != null).ToList();
    //    }

    //    public float GetMovementCostToNeighbor(Node neighbor)
    //    {
    //        if (!GetNeighbors().Contains(neighbor))
    //        {
    //            Debug.LogWarning($"Destination is not a neighboring node");
    //            return float.PositiveInfinity;
    //        }

    //        if (Position.x != neighbor.Position.x && Position.y != neighbor.Position.y)
    //            return 1.4f; //diagonal
    //        else
    //            return 1; //horizontal & vertical
    //    }
    //}

    private void Start()
    {
        Instance = this;
        pathBlockers = FindObjectsOfType<PathBlocker>();

        InitializeNodes(firstNode.position, lastNode.position);



        //old test code vvv

        //Node current = FindShortestPath(testStart.position, testGoal.position);

        //VisualizeNodes();
        //VisualizeParents();

        //while (current != null)
        //{
        //    current.gameObject.GetComponent<Renderer>().material = pathMat;
        //    current = current.Parent;
        //}
    }

    //private void VisualizeNodes()
    //{
    //    //foreach (var n in nodes)
    //    //{
    //    //    var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
    //    //    cube.transform.parent = transform;
    //    //    cube.transform.localScale = Vector3.one * 0.9f;
    //    //    cube.transform.position = n.Key;

    //    //    if (!n.Value.IsWalkable)
    //    //        cube.GetComponent<Renderer>().material = blockMat;
            
    //    //    n.Value.Obj = cube;
    //    //}
    //}

    //private void VisualizeParents()
    //{
    //    foreach (var node in nodes)
    //    {
    //        Node n = node.Value;
    //        if (n.Parent != null)
    //            Debug.DrawRay((Vector3)n.Position + Vector3.back, (Vector3)(n.Parent.Position - n.Position).normalized * 0.5f + Vector3.back, Color.black, 60);
    //    }
    //}

    /// <summary>
    /// Builds a grid of nodes
    /// </summary>
    private void InitializeNodes(Vector2 firstPos, Vector2 lastPos)
    {
        nodes = new Dictionary<Vector2, Node>();
        float left = Mathf.Min(firstPos.x, lastPos.x);
        float right = Mathf.Max(firstPos.x, lastPos.x);
        float bottom = Mathf.Min(firstPos.y, lastPos.y);
        float top = Mathf.Max(firstPos.y, lastPos.y);

        for (float x = left; x <= right; x += nodeDistance)
        {
            for (float y = bottom; y <= top; y += nodeDistance)
            {
                Vector2 position = new Vector2(x, y);
                bool walkable = !pathBlockers.Any(b => b.boxCollider.OverlapPoint(position));

                //var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                //cube.transform.parent = transform;
                //cube.transform.localScale = Vector3.one * 0.9f;
                //cube.transform.position = position;

                //if (!walkable)
                //    cube.GetComponent<Renderer>().material = blockMat;

                Node n = new Node();
                n.Position = position;
                n.IsWalkable = walkable;

                nodes.Add(position, n);
            }
        }

        for (float x = left; x <= right; x += nodeDistance)
        {
            for (float y = bottom; y <= top; y += nodeDistance)
            {
                Vector2 position = new Vector2(x, y);
                Node n = nodes[position];

                if (!n.IsWalkable)
                    continue;

                if (x > left)
                {
                    n.W = GetWalkableNode(position + Vector2.left * nodeDistance);
                }
                if (x < right)
                {
                    n.E = GetWalkableNode(position + Vector2.right * nodeDistance);
                }
                if (y > bottom)
                {
                    n.S = GetWalkableNode(position + Vector2.down * nodeDistance);
                }
                if (y < top)
                {
                    n.N = GetWalkableNode(position + Vector2.up * nodeDistance);
                }

                if (includeDiagonals)
                {
                    if (x > left && y > bottom && n.S != null && n.W != null)
                    {
                        n.SW = GetWalkableNode(position + (Vector2.left + Vector2.down) * nodeDistance);
                    }
                    if (x > left && y < top && n.N != null && n.W != null)
                    {
                        n.NW = GetWalkableNode(position + (Vector2.left + Vector2.up) * nodeDistance);
                    }
                    if (x < right && y > bottom && n.S != null && n.E != null)
                    {
                        n.SE = GetWalkableNode(position + (Vector2.right + Vector2.down) * nodeDistance);
                    }
                    if (x < right && y < top && n.N != null && n.E != null)
                    {
                        n.NE = GetWalkableNode(position + (Vector2.right + Vector2.up) * nodeDistance);
                    }
                }
            }
        }
    }

    private Node GetWalkableNode(Vector2 position)
    {
        var node = nodes[position];
        if (node.IsWalkable)
            return node;
        return null;
    }

    private Vector2 Round(Vector2 position)
    {
        return new Vector2(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y));
    }

    /*
     * 1.   Calculate heuristic values of all nodes
     * 
     * 2.   Put starting node on open list
     * 
     * Loop:
     * 
     * 3.   Set smallest F value node on open list to current node
     * 
     * 4.   Take current node off open, onto closed list
     * 
     * 5.   If next to goal node
     *          parent goal node to current
     *          return goal node (path)
     *          
     * 6.   Calculate movement costs of nodes around current node (excluding closed list nodes)
     *      If on open list
     *          if it's G is bigger than current node + cost
     *              reparent to current
     *              recalculate G from current
     *      else
     *          parent to current
     *          calculate G from current
     */

    public Node FindShortestPath(Vector2 start, Vector2 destination)
    {
        start = Round(start);
        destination = Round(start);

        //Initialize
        ClearPathData();

        var openList = new Dictionary<Vector2, Node>();
        var closedList = new Dictionary<Vector2, Node>();

        //1.   Calculate heuristic values of all nodes
        ComputeHueristics(destination);

        //2.   Put starting node on open list
        openList.Add(start, nodes[start]);

        while (openList.Count > 0)
        {
            //3.   Select smallest F value node on open list
            Node current = GetMinFNode(openList);

            //4.   Take current node off open, onto closed list
            openList.Remove(current.Position);
            closedList.Add(current.Position, current);

            //5.    Check if next to goal node
            var neighbors = current.GetNeighbors().Where(x => !closedList.ContainsKey(x.Position)).ToList();
            var goal = nodes[destination];

            if (neighbors.Contains(goal))
            {
                goal.Parent = current;
                return goal;
            }

            //6.   Calculate movement costs of surrounding nodes (excluding closed list nodes)
            foreach (var n in neighbors)
            {
                float moveCostFromCurrentNode = current.GetMovementCostToNeighbor(n);
                float g = current.G + moveCostFromCurrentNode;

                if (openList.ContainsKey(n.Position))
                {
                    //Reparent and recalculate G if this path is better than it's previous path
                    if (g < n.G)
                    {
                        n.G = g;
                        n.Parent = current;
                    }
                }
                else
                {
                    //First time visiting this node: add to openList and calculate G
                    openList.Add(n.Position, n);
                    n.G = g;
                    n.Parent = current;
                }
            }
        }

        Debug.LogError("Failed to find valid path");
        return null;
    }

    private void ClearPathData()
    {
        foreach (var n in nodes)
        {
            n.Value.ResetPathInfo();
        }
    }

    /// <summary>
    /// Number of spaces to get from each node to end point
    /// </summary>
    private void ComputeHueristics(Vector2 destination)
    {
        foreach (var node in nodes)
        {
            var pos = node.Key;
            node.Value.H = Mathf.Abs(pos.x - destination.x) + Mathf.Abs(pos.y - destination.y);
        }
    }
    
    private Node GetMinFNode(Dictionary<Vector2, Node> openList)
    {
        Node min = null;
        foreach (var n in openList)
        {
            if (min == null || min.F > n.Value.F)
            {
                min = n.Value;
            }
        }
        return min;
    }
}
