using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Team2Game1;

public class Zombie : Selectable
{
    public Table table;
    public SpriteRenderer spriteRenderer;
    public Sprite seated;
    public Sprite raisedHand;

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
        spriteRenderer.sprite = seated;
        GameController.Instance.RemoveFromLine(this);
    }
}
