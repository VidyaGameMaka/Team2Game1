using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PathBlocker : MonoBehaviour
{
    public BoxCollider2D boxCollider;

    private void OnValidate()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }
}
