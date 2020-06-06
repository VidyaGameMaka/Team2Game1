using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Credits : MonoBehaviour
{
    [SerializeField] private float timeMultiplier;
    private TextMeshProUGUI tmptxt;

    private float state = 0;

    private void Start()
    {
        tmptxt = GetComponent<TextMeshProUGUI>();
        tmptxt.text = "";
        tmptxt.color = new Color(1,1,1,0);
    }

    private void Update() {
        
        switch (state)
        {
            case 0:
                state = 0.1f;
                tmptxt.text = "Coding: \n FedoraLord \n VidyaGameMaka";
                StartCoroutine(stateFade(tmptxt));
                break;
            case 1:
                state = 1.1f;
                tmptxt.text = "Art: \n Pankek \n Evil Tomato";
                StartCoroutine(stateFade(tmptxt));
                break;
            case 2:
                state = 2.2f;
                tmptxt.text = "Music: \n Codex97";
                StartCoroutine(stateFade(tmptxt));
                break;
            case 3:
                state = 3.3f;
                tmptxt.text = "Writing: \n AnimeWeeb.5 \n Scientist Sam";
                StartCoroutine(stateFade(tmptxt));
                break;
        }


    }


    private IEnumerator stateFade(TextMeshProUGUI textToUse)
    {
        yield return StartCoroutine(FadeInText(1f, textToUse));
        yield return new WaitForSeconds(3f);
        yield return StartCoroutine(FadeOutText(1f, textToUse));
        //End of transition, do some extra stuff!!

        switch (state)
        {
            case 0.1f:
                state = 1;
                break;
            case 1.1f:
                state = 2;
                break;
            case 2.2f:
                state = 3;
                break;
            case 3.3f:
                state = 0;
                break;
        }
    }







    private IEnumerator FadeInText(float timeSpeed, TextMeshProUGUI text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        while (text.color.a < 1.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime * timeSpeed));
            yield return null;
        }
    }
    private IEnumerator FadeOutText(float timeSpeed, TextMeshProUGUI text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime * timeSpeed));
            yield return null;
        }
    }

}
