using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Team2Game1;

public class TrashCan : Selectable
{
    public override void OnSelect()
    {
        Player.Instance.Select(this);
        Player.Instance.MoveTo(transform.position);
    }

    public override void OnInteract()
    {
        Food food = Player.Instance.holding as Food;
        if (food != null && food.isEaten)
        {
            GameMaster.soundFX.PlaySound(GameMaster.audioClip_SO.PlateinTrash);
            Destroy(food.gameObject);
        }
    }
}
