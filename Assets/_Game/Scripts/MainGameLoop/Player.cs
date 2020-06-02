using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public float stepSize = 0.01f;
    public Transform hand;
    public Animator anim;
    public SpriteRenderer spriteRenderer;

    [HideInInspector]
    public List<Food> holding;
    [HideInInspector]
    public List<Selectable> selectionQueue;
    [HideInInspector]
    public bool isZombieSelected;

    private Action onDestinationReached;
    private bool followPath;
    private List<Vector2> path = new List<Vector2>();

    #region MonoBehavior
    private void Start()
    {
        Instance = this;
    }
    
    private void FixedUpdate()
    {
        if (followPath)
        {
            if (path.Count == 0)
            {
                OnDestinationReached();
                return;
            }

            anim.SetInteger("state", 1);

            Vector2 target = path[0];
            float xDirection = (target - (Vector2)transform.position).x;
            float xScale = xDirection < 0 ? -1 : 1;
            transform.localScale = new Vector3(xScale, 1, 1);

            transform.position = Vector2.MoveTowards(transform.position, target, stepSize);

            if ((Vector2)transform.position == target)
            {
                path.RemoveAt(0);
            }
        }
        else
        {
            anim.SetInteger("state", 0);
        }
    }
    #endregion

    public void OnClick(Selectable selected)
    {
        selectionQueue.Add(selected);

        if (selectionQueue.Count == 1)
            selected.OnSelect();
    }

    public void MoveTo(Vector2 position, Action onDestinationReached)
    {
        this.onDestinationReached = onDestinationReached;
        followPath = true;
        path = AStar.Instance.FindShortestPath(transform.position, position);
    }

    private void OnDestinationReached()
    {
        followPath = false;
        onDestinationReached();
        SelectNext();
    }
    
    public void SelectNext()
    {
        RemoveCurrentSelection();
        if (selectionQueue.Any())
        {
            selectionQueue.First().OnSelect();
        }
    }

    private void RemoveCurrentSelection()
    {
        if (selectionQueue.Any())
        {
            selectionQueue.RemoveAt(0);
        }
    }

    public bool TryPickUp(Food obj)
    {
        if (holding.Count == 0 || (obj.isEaten && holding.All(x => x.isEaten)))
        {
            obj.transform.SetParent(hand);
            obj.transform.localPosition = Vector3.zero;
            holding.Add(obj);
            return true;
        }

        return false;
    }
}
