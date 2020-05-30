using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Selectable : MonoBehaviour
{
    private void OnMouseDown()
    {
        Player.Instance.Select(this);
    }

    public abstract void OnInteract();
}
