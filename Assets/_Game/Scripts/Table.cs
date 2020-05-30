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
        //maybe we should only have one of these last two
        ReadyForCheck,  //customer waits for you to let them leave and you immediately pick up the dirty dishes
        Dirty           //or, customer leaves money and dishes on the table when done eating and you just pick up the dishes
    }

    public override void OnInteract()
    {
        
    }
}
