using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelCompletedUI : MonoBehaviour {

    public TextMeshProUGUI tmp;

    private void OnEnable() {

        tmp.text = "LEVEL Completed \n Money Earned: " + GameController.Instance.score.ToString("c2") + "\n Good Jorb!";

    }


    public void BTN_MainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void BTN_Quit() {
        Application.Quit();
    }

    public void BTN_PlayAgain() {
        SceneManager.LoadScene("reloader");
    }


}
