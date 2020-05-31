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
            Player.Instance.Select(GameController.Instance.NextZombie);
    }

    public override void OnInteract()
    {
        
    }

    public void Seat(Table table)
    {
        this.table = table;
        spriteRenderer.sprite = seated;
        GameController.Instance.RemoveFromLine(this);
    }
}
