using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Team2Game1;

public class Food : Selectable
{
    public BoxCollider2D boxCollider;
    public SpriteRenderer spriteRenderer;
    public Sprite emptyPlate;
    public bool isEaten = false;

    private void Start() {     
        GameMaster.soundFX.PlaySound(GameMaster.audioClip_SO.BrainSquish);
    }

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
