using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Team2Game1;

public class LevelCompletedUI : MonoBehaviour {

    public TextMeshProUGUI tmp;


    private List<string> messages = new List<string> {
        "Looks like the clients were thrilled to death! Good job out there!",
        "You're the life and soul of this restaurant! Although, it's not like that there's that much competition...",
        "I didn't know we could sell that many brains in one shift! Good work!",
        "To be honest, I didn't think you'd make it through your shift! Looks like you did well!",
        "I knew opening a restaurant next to the graveyard would be a hit! Thanks for helping me making it a success!",
        "This is so great! Just remember to not let clients bite the hand that feeds them! It would... complicate things.",
        "I wonder what the clients eat oustide of buisness hours... Anyways! Here's to another successful shift!",
        "Here's your cut for today! Someday we'll be making enough to fix the wall back here in the kitchen!",
        "Did you know? When I called your references before hiring you... all of them groaned! I knew you'd be a perfect fit!",
        "How do I get the ingredient for our signature dish? Sorry! Trade secret! We'd go out of buisness if the compitetion knew!",
        "Great work! Looks like you've really got the brains for this kind of work!",
        "Nice work today! But should you really be spending all that money on bubblegum?",
        "Have you noticed clients always order the same thing? Kind of weird considering our vast menu selection! Anyways, good work today!",
        "Nice one today! I put a lot of effort in the presentation of our signature dish, so thanks for always just picking up one plate at a time!",
        "Keep it going and you'll probably outlive the last waitress! I mean... you'll probably be employee of the month!"
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
