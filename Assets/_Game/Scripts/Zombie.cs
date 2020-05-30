using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Selectable
{
    public Table table;
    public SpriteRenderer spriteRenderer;
    public Sprite seated;
    public Sprite raisedHand;

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
        spriteRenderer.sprite = seated;
    }
}
