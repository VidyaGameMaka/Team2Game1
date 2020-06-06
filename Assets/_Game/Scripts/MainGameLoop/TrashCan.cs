using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Team2Game1;
using System.Linq;

public class TrashCan : Selectable
{
    public override void OnSelect()
    {
        Player.Instance.MoveTo(transform.position, OnInteract);
    }

    public override void OnInteract()
    {
        var holding = Player.Instance.holding;
        if (holding.Any() && holding.All(x => x.isEaten))
        {
            while (holding.Any())
            {
                GameMaster.soundFX.PlaySound(GameMaster.audioClip_SO.PlateinTrash);
                var food = holding.First();
                Destroy(food.gameObject);
                holding.RemoveAt(0);
            }
        }

        Player.Instance.SelectNext();
    }
}
