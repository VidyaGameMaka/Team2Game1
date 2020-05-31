using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortByY : MonoBehaviour {

    private SpriteRenderer sr;

    public bool ReorderOnUpdate = false;

    public void Start() {
        sr = GetComponent<SpriteRenderer>();
        DoReorder();
    }

    private void Update() {
        if (!ReorderOnUpdate) { return; }
        DoReorder();
    }

    private void DoReorder() {
        sr.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
    }


}
