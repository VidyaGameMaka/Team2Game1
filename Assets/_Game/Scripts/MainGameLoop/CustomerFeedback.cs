using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomerFeedback : MonoBehaviour
{
    public static CustomerFeedback Instance { get; private set; }

    public Canvas canvas;
    public TextMeshProUGUI textMesh;

    private List<string> options = new List<string>()
    {
        "What a time to be dead!",
        "Nothing like living ressurection in the fast lane!",
        "You make the fast bucks, I cut to the quick!",
        "You're the life and soul of this restaurant!",
        "Death is short and time is swift!",
        "Good! You're juicing them back to life!",
        "You're the light of their ressurection!",
        "It's feeding time at the zoo!",
        "Nice seeing you dancing with ressurection!",
        "Nice! The clients are thrilled to death!",
        "Feels like ressurection warming up!",
        "You've got this!",
        "Keep it up!",
        "Nice going!",
        "There you go!",
        "Let's do this!",
        "Another brain, another happy client!",
        "I'm glad I hired you!",
        "Keep them coming!",
        "This is good for buisiness!",
        "Let me know if you need anything!",
        "Hurry! Serve it while it's dead!",
        "You've got to risk life and limb!",
        "Ressurection isn't all sunshine and rainbows!",
        "Ressurection got you down?",
        "Pick up the pace!",
        "It's a matter of life and resurrection!",
        "Don't pester the life out of our clients!",
        "Don't give me a line, just take the brain!",
        "You have to pick up the pace!",
        "Try being more efficient!",
        "We have to roll up our sleeves now!",
        "Let's focus on the next clients!",
        "The money won't make itself!",
        "You've got to get a hold on things!"
    };

    private void Start()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void TriggerFeedback()
    {
        if (!gameObject.activeInHierarchy && GameController.Instance.restaurantOpen)
        {
            gameObject.SetActive(true);
            StartCoroutine(Display());
        }
    }

    private IEnumerator Display()
    {
        int i = Random.Range(0, options.Count);
        textMesh.text = options[i];

        yield return new WaitForSeconds(3);

        gameObject.SetActive(false);
    }
}
