﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : Selectable
{
    public enum State
    {
        Available,
        Ordering,
        ReadyToOrder,
        WaitingOnFood,
        Eating,
        Dirty
    }
    
    public State currentState;

    public Transform platePosition;
    public Transform zombiePosition;
    
    private Zombie zombie;
    private Food food;

    public override void OnSelect()
    {
        var selected = Player.Instance.selected;

        switch (currentState)
        {
            case State.Available:
                if (selected is Zombie)
                {
                    selected.transform.position = zombiePosition.position;
                    Player.Instance.selected = null;
                    zombie = selected as Zombie;
                    zombie.Seat(this);
                    StartCoroutine(OrderFood());
                }
                break;
            case State.ReadyToOrder:
            case State.WaitingOnFood:
            case State.Dirty:
                Player.Instance.Select(this);
                Player.Instance.MoveTo(transform.position);
                break;
        }
    }

    public override void OnInteract()
    {
        var holding = Player.Instance.holding;

        switch (currentState)
        {
            case State.ReadyToOrder:
                //TODO: take order
                zombie.spriteRenderer.sprite = zombie.seated;
                currentState = State.WaitingOnFood;
                break;
            case State.WaitingOnFood:
                Food f = holding as Food;
                if (f != null && !f.isEaten)
                {
                    food = holding as Food;
                    Player.Instance.holding = null;
                    holding.transform.SetParent(null);
                    holding.transform.position = platePosition.position;
                    StartCoroutine(Eat());
                }
                break;
            case State.Dirty:
                //TODO: clean table
                food.OnInteract();
                food = null;
                currentState = State.Available;
                break;
            default:
                break;
        }
    }

    private IEnumerator OrderFood()
    {
        currentState = State.Ordering;
        yield return new WaitForSeconds(2);
        currentState = State.ReadyToOrder;
        zombie.spriteRenderer.sprite = zombie.raisedHand;
    }

    private IEnumerator Eat()
    {
        currentState = State.Eating;
        yield return new WaitForSeconds(2);
        currentState = State.Dirty;
        food.OnEaten();
        Destroy(zombie.gameObject);
        zombie = null;
    }
}
