using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Selectable : MonoBehaviour
{
    private void OnMouseDown()
    {
        OnSelect();
    }

    public abstract void OnSelect();
    public abstract void OnInteract();
}
