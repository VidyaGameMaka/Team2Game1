using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Team2Game1;

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
                FoodBar.foodBar.RequestFood(); //Spawn food on the foodbar
                //zombie.spriteRenderer.sprite = zombie.seated;               
                currentState = State.WaitingOnFood;

                break;
            case State.WaitingOnFood:
                Food f = holding as Food;
                if (f != null && !f.isEaten)
                {
                    GameMaster.soundFX.PlaySound(GameMaster.audioClip_SO.PlateinTrash);

                    food = holding as Food;
                    Player.Instance.holding = null;
                    holding.transform.SetParent(null);
                    holding.transform.position = platePosition.position;
                    StartCoroutine(Eat());
                }
                break;
            case State.Dirty:
                if (Player.Instance.holding == null)
                {
                    food.OnInteract();
                    food = null;
                    currentState = State.Available;
                    //Add to Score
                    GameController.Instance.score += Random.Range(5f, 10f);
                }
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
        //zombie.spriteRenderer.sprite = zombie.raisedHand;
        zombie.anim.SetInteger("state", 2); // Ready to order animation

        AudioClip clipchoice = GameMaster.audioClip_SO.ZombieSoundGroup[Random.Range(0, GameMaster.audioClip_SO.ZombieSoundGroup.Length)];
        GameMaster.soundFX.PlaySound(clipchoice);
    }

    private IEnumerator Eat()
    {
        zombie.anim.SetInteger("state", 1); // Table Idle

        currentState = State.Eating;
        yield return new WaitForSeconds(2);
        currentState = State.Dirty;
        food.OnEaten();
        Destroy(zombie.gameObject);
        zombie = null;
    }
}
