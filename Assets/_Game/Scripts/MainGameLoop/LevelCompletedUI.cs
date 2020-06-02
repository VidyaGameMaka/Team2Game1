using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Team2Game1;

public class LevelCompletedUI : MonoBehaviour {

    public TextMeshProUGUI tmp;


    private List<string> messages = new List<string> {
        "Brilliant job!",
        "Outstanding work!",
        "This is truly above and beyond.",
        "We are thrilled to have you on our team and this is exactly why we need you.",
        "This is superb! I had no idea a document could look this good.",
        "To be honest, when we started the project I wasn’t sure we could pull this off but you certainly did it and did it well.",
        "We are so fortunate to have an innovator like you on our team.",
        "This is so great I think others could benefit from learning about it. Can I share your work at our team meeting/with my peers/with my boss, etc.?",
        "You set a high bar with this one.",
        "This showcases you are a role model and leader in our organization."
    };

    private void OnEnable() {
        tmp.text = "LEVEL Completed \n Money Earned: " + GameController.Instance.score.ToString("c2") + "\n ";
        tmp.text += messages[Random.Range(0, messages.Count)];
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
