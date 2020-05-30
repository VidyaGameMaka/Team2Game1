using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Selectable
{
    public BoxCollider2D boxCollider;
    public SpriteRenderer spriteRenderer;
    public Sprite emptyPlate;
    public bool isEaten = false;

    public override void OnSelect()
    {
        Player.Instance.Select(this);
        Player.Instance.MoveTo(transform.position);
    }

    public override void OnInteract()
    {
        bool success = Player.Instance.TryPickUp(this);
        if (success)
        {
            boxCollider.enabled = false;
        }
    }

    public void OnEaten()
    {
        spriteRenderer.sprite = emptyPlate;
        isEaten = true;
    }
}
