using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Team2Game1;
using System.Linq;

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
        switch (currentState)
        {
            case State.Available:
                if (Player.Instance.isZombieSelected)
                {
                    zombie = GameController.Instance.NextZombie;
                    zombie.Seat(this);
                    StartCoroutine(OrderFood());
                }
                Player.Instance.SelectNext();
                break;
            case State.ReadyToOrder:
            case State.WaitingOnFood:
            case State.Dirty:
                Player.Instance.MoveTo(transform.position, OnInteract);
                break;
            default:
                Player.Instance.SelectNext();
                break;
        }
    }

    public override void OnInteract()
    {
        switch (currentState)
        {
            case State.ReadyToOrder:               
                FoodBar.foodBar.RequestFood();
                currentState = State.WaitingOnFood;

                break;
            case State.WaitingOnFood:
                Food f = Player.Instance.holding.FirstOrDefault();
                if (f != null && !f.isEaten)
                {
                    GameMaster.soundFX.PlaySound(GameMaster.audioClip_SO.PlateinTrash);

                    Player.Instance.holding.RemoveAt(0);
                    f.transform.SetParent(null);
                    f.transform.position = platePosition.position;
                    f.transform.rotation = Quaternion.identity;

                    food = f;
                    StartCoroutine(Eat());
                    zombie.anim.SetInteger("state", 3); //eating

                    CustomerFeedback.Instance.TriggerFeedback();
                }
                break;
            case State.Dirty:
                if (Player.Instance.holding.All(x => x.isEaten))
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
