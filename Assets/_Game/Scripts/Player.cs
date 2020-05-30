using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    
    public float stepSize = 0.01f;
    public Transform hand;
    public Selectable holding;
    public Selectable selected;

    public Animator anim;

    private List<Vector2> path = new List<Vector2>();

    #region MonoBehavior
    private void Start()
    {
        Instance = this;
    }

    public void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            path = AStar.Instance.FindShortestPath(transform.position, worldPosition);

            if (path.Count == 0)
            {
                OnDestinationReached();
            }
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
    #endregion

    public void MoveTo(Vector2 position)
    {
        path = AStar.Instance.FindShortestPath(transform.position, position);

        if (path.Count == 0)
        {
            OnDestinationReached();
        }
    }

    public void Select(Selectable selected)
    {
        this.selected = selected;
    }

    private void OnDestinationReached()
    {
        if (selected != null)
        {
            selected.OnInteract();
            selected = null;
        }
    }

    public bool TryPickUp(Selectable obj)
    {
        if (holding == null)
        {
            obj.transform.SetParent(hand);
            obj.transform.localPosition = Vector3.zero;
            holding = obj;
            return true;
        }

        return false;
    }
}
