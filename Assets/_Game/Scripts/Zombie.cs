using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Selectable
{
    public Table table;

    public override void OnSelect()
    {
        if (table == null)
            Player.Instance.Select(this);
    }

    public override void OnInteract()
    {
        
    }

    public void Seat(Table table)
    {
        this.table = table;

    }

    public void OnStateChanged()
    {

    }
}
