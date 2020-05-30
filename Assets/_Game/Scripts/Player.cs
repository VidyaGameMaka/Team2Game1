using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float stepSize = 0.01f;
    
    private List<Vector2> path = new List<Vector2>();
    
    public void Update()
    {
        if (/*path.Count == 0 && */Input.GetButtonDown("Fire1"))
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            path = AStar.Instance.FindShortestPath(transform.position, worldPosition);
        }
    }

    private void FixedUpdate()
    {
        if (path.Count > 0)
        {
            Vector2 target = path[0];
            transform.position = Vector2.MoveTowards(transform.position, target, stepSize);

            if ((Vector2)transform.position == target)
            {
                path.RemoveAt(0);

                if (path.Count == 0)
                {
                    OnDestinationReached();
                }
            }
        }
    }

    private void OnDestinationReached()
    {

    }
}
