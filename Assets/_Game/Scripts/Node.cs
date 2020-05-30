using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Node
{
    public bool IsWalkable ; //{ get; set; }
    public float G ; //{ get; set; }
    public float H ; //{ get; set; }
    public float F => G + H;
    public float Weight ; //{ get; set; } = 1;
    public Node Parent ; //{ get; set; }
    public Node N ; //{ get; set; }
    public Node S ; //{ get; set; }
    public Node E ; //{ get; set; }
    public Node W ; //{ get; set; }
    public Node NE ; //{ get; set; }
    public Node NW ; //{ get; set; }
    public Node SE ; //{ get; set; }
    public Node SW ; //{ get; set; }
    public Vector2 Position ; //{ get; set; }

    public float f;

    private void Update()
    {
        f = F;
    }

    public void ResetPathInfo()
    {
        G = 0;
        H = 0;
        Parent = null;
    }

    public List<Node> GetNeighbors()
    {
        var neighbors = new List<Node>() { N, S, E, W, NE, NW, SE, SW };
        return neighbors.Where(x => x != null).ToList();
    }

    public float GetMovementCostToNeighbor(Node neighbor)
    {
        if (!GetNeighbors().Contains(neighbor))
        {
            Debug.LogWarning($"Destination is not a neighboring node");
            return float.PositiveInfinity;
        }

        if (Position.x != neighbor.Position.x && Position.y != neighbor.Position.y)
            return 1.4f; //diagonal
        else
            return 1; //horizontal & vertical
    }
}
