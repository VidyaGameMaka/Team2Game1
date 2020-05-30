using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Selectable
{
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
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
