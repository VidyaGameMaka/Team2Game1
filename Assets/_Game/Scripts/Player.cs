using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public Node currentNode;
    public float speed = 1;
    public float stepSize = 0.01f;
    
    // Start is called before the first frame update
    //void Start()
    //{
    //    Node n = AStar.Instance.FindShortestPath(start.position, end.position);

    //    //while (n != null)
    //    //{
    //    //    var obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
    //    //    obj.transform.position = n.Position;
    //    //    n = n.Parent;
    //    //}

    //}
    
    public void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentNode = AStar.Instance.FindShortestPath(transform.position, worldPosition);
        }
    }

    private void FixedUpdate()
    {
        if (currentNode != null)
        {
            //Vector2 direction = currentNode.Position - (Vector2)transform.position;
            //rb.velocity = direction * speed;

            transform.position = Vector2.MoveTowards(transform.position, currentNode.Position, stepSize);
            if ((Vector2)transform.position == currentNode.Position)
            {
                currentNode = currentNode.Parent;
            }
        }
    }

    public void SetPath(Vector3 destination)
    {
        currentNode = AStar.Instance.FindShortestPath(transform.position, destination);
    }
}
