using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Clock : MonoBehaviour {
    
    public GameObject clockHand;
    public TextMeshPro restaurantText;

    private float calculatedRotation;

    private void Update() {

        if (GameController.Instance.restaurantOpen) {
            restaurantText.text = "Restaurant: Open";
        } else {
            restaurantText.text = "Restaurant: Closed";
        }

        if (GameController.Instance.restaurantOpen) {

            calculatedRotation = calculatedRotation + (-GameController.Instance.clockSpeed * Time.deltaTime);

            if (calculatedRotation < -90) { GameController.Instance.restaurantOpen = false; }

            //Update Clock Position
            clockHand.transform.eulerAngles = new Vector3(0, 0, calculatedRotation);
        }
    }  

}
