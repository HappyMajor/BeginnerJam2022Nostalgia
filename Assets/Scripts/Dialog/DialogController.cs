using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DialogController : MonoBehaviour
{
    public TMP_Text textDisplay;

    private void Start()
    {
        ShowMessageDelayed("Hello!\nWelcome to the Holy Moe Lee Bar, I am your host, how can I help you?!");
    }

    public void ShowMessageDelayed(string test)
    {
        StopAllCoroutines();
        StartCoroutine(DelayedMessage(test));
    }

    private IEnumerator DelayedMessage(string text)
    {
        textDisplay.text = "";
        for(int i = 0; i < text.Length; i++)
        {
            float delay;
            if(Char.IsWhiteSpace(text[i]))
            {
                delay = 0.1f;
            } else
            {
                delay = 0.03f;
            }

            textDisplay.text += text[i];

            yield return new WaitForSeconds(delay);
        }
    }

}
