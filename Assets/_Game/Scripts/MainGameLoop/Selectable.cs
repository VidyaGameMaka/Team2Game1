using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Selectable : MonoBehaviour
{
    private void OnMouseDown()
    {
        Player.Instance.OnClick(this);
    }

    public abstract void OnSelect();
    public abstract void OnInteract();
}
