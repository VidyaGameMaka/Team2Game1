using System.Collections;
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

    private State currentState;
    public State CurrentState
    {
        get => currentState;
        set
        {
            currentState = value;
            OnStateChanged();
        }
    }

    public Transform platePosition;
    public Transform zombiePosition;
    
    private Zombie zombie;

    public override void OnSelect()
    {
        var selected = Player.Instance.selected;

        switch (CurrentState)
        {
            case State.Available:
                if (selected is Zombie)
                {
                    selected.transform.position = zombiePosition.position;
                    Player.Instance.selected = null;
                    zombie = selected as Zombie;
                    zombie.Seat(this);
                    StartCoroutine(ChangeState(State.Ordering, 2));
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

        switch (CurrentState)
        {
            case State.ReadyToOrder:
                //TODO: take order
                CurrentState = State.WaitingOnFood;
                break;
            case State.WaitingOnFood:
                if (holding is Food)
                {
                    Player.Instance.holding = null;
                    holding.transform.SetParent(platePosition);
                    holding.transform.localPosition = Vector3.zero;
                    CurrentState = State.Eating;
                }
                break;
            case State.Dirty:
                //TODO: clean table
                CurrentState = State.Available;
                break;
            default:
                break;
        }
    }

    private IEnumerator ChangeState(State state, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        CurrentState = state;
    }

    private void OnStateChanged()
    {
        switch (CurrentState)
        {
            case State.ReadyToOrder:
                //raise hand
                break;
            case State.WaitingOnFood:
                //lower hand
                break;
            case State.Dirty:
                //spawn trash
                break;
            default:
                break;
        }
    }
}
