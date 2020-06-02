using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Team2Game1;

public class LevelCompletedUI : MonoBehaviour {

    public TextMeshProUGUI tmp;

    private void OnEnable() {

        tmp.text = "LEVEL Completed \n Money Earned: " + GameController.Instance.score.ToString("c2") + "\n Good Jorb!";

    }


    public void BTN_MainMenu() {
        SceneManager.LoadScene("MainMenu");
        GameMaster.soundFX.PlaySound(GameMaster.audioClip_SO.UI_Menu_Button_Confirm);
    }

    public void BTN_Quit() {
        Application.Quit();
    }

    public void BTN_PlayAgain() {
        SceneManager.LoadScene("reloader");
        GameMaster.soundFX.PlaySound(GameMaster.audioClip_SO.UI_Menu_Button_Confirm);
    }


}
