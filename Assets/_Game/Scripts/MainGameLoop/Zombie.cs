using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Team2Game1;

public class Zombie : Selectable
{
    public Table table;
    public SpriteRenderer spriteRenderer;
    public Animator anim;
    public Sprite seated;
    public Sprite raisedHand;
    public BoxCollider2D box;

    public override void OnSelect() {
        if (table == null) {
            Player.Instance.Select(GameController.Instance.NextZombie);
        }

        AudioClip clipchoice = GameMaster.audioClip_SO.ZombieSoundGroup[Random.Range(0, GameMaster.audioClip_SO.ZombieSoundGroup.Length)];          
        GameMaster.soundFX.PlaySound(clipchoice);
    }

    public override void OnInteract() {
        
    }

    public void Seat(Table table)
    {
        this.table = table;
        //spriteRenderer.sprite = seated;
        box.enabled = false; //disable zombie box collider since we don't need to click it anymore
        spriteRenderer.flipX = true;
        anim.SetInteger("state", 1); //Seated animation on 1
        GameController.Instance.RemoveFromLine(this);
    }
}
