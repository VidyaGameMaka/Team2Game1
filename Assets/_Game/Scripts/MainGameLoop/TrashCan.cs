using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Destroy(food.gameObject);
        }
    }
}
