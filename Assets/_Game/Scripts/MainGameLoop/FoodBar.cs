﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBar : MonoBehaviour {

    public GameObject foodPrefab;

    public SpriteRenderer chef;
    public Sprite cookingChef;
    public Sprite waitingChef;

    public GameObject spawn1, spawn2, spawn3, spawn4;
    public Food Food1;
    public Food Food2;
    public Food Food3;
    public Food Food4;

    public static FoodBar foodBar;

    private int numCooking;

    private void Awake() {
        foodBar = this;
    }
    
    public void RequestFood() {
        StartCoroutine(CookFood());
    }

    private IEnumerator CookFood()
    {
        numCooking++;
        yield return new WaitForSeconds(2);
        numCooking--;

        if (!Food1) { Food1 = Instantiate(foodPrefab, spawn1.transform.position, Quaternion.identity).GetComponent<Food>(); yield break; }
        if (!Food2) { Food2 = Instantiate(foodPrefab, spawn2.transform.position, Quaternion.identity).GetComponent<Food>(); yield break; }
        if (!Food3) { Food3 = Instantiate(foodPrefab, spawn3.transform.position, Quaternion.identity).GetComponent<Food>(); yield break; }
        if (!Food4) { Food4 = Instantiate(foodPrefab, spawn4.transform.position, Quaternion.identity).GetComponent<Food>(); yield break; }
    }

    private void Update()
    {
        if (numCooking > 0)
        {
            chef.sprite = cookingChef;
        }
        else
        {
            chef.sprite = waitingChef;
        }
    }
}
