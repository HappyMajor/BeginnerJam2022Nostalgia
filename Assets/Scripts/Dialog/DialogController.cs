using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DialogController : MonoBehaviour
{
    public TMP_Text textDisplay;
    public CharacterDataSO currentCharacter;
    public float deltedTimeSeconds = 0f;

    private Coroutine messageWriteCoRoutine;
    private Coroutine stopTimeCoRoutine;

    private bool moveTime = true;


    public Dictionary<CharacterDataSO.Hint, int> hintShowedCounterDic = new Dictionary<CharacterDataSO.Hint, int>();

    private void Start()
    {
        StartCoroutine(CallUpdateEverySecond());
        ShowMessageDelayed("Hello!\nWelcome to the Holy Moe Lee Bar, I am your host, how can I help you?!");
        GameEventController.GetInstance().onGrabEvent += OnGrabEvent;
        GameEventController.GetInstance().onPourEvent += OnPourEvent;
    }

    private void Update()
    {
    }

    public void Reset(CharacterDataSO characterData)
    {
        this.currentCharacter = characterData;
        deltedTimeSeconds = 0;
    }

    private void UpdateEverySecond()
    {
        if (moveTime) deltedTimeSeconds += 1;

        foreach (CharacterDataSO.Hint hint in currentCharacter.hints)
        {
            if (hint.onEvent.eventType == CharacterDataSO.EvenType.AFTER_TIME)
            {
                int eventTime = 9999999;
                int.TryParse(hint.onEvent.arguments[0], out eventTime);
                Debug.Log("eventTime: " + eventTime + " onEvent.arguments[0] = " + hint.onEvent.arguments[0] + "  deltedTime: " + deltedTimeSeconds);
                if (deltedTimeSeconds >= eventTime)
                {
                    if(GetCounterOfHint(hint) < hint.onEvent.maxAmount)
                    {
                        ShowMessageDelayed(hint.text);
                        AddHintCounter(hint);
                    }
                }
            }
        }
    }

    private IEnumerator StopTimeFor(int seconds)
    {
        moveTime = false;
        for(int i = 0; i < seconds; i++)
        {
            yield return new WaitForSeconds(1);
        }
        moveTime = true;
    }

    public void ShowMessageDelayed(string msg)
    {
        if(messageWriteCoRoutine != null) StopCoroutine(messageWriteCoRoutine);

        messageWriteCoRoutine = StartCoroutine(DelayedMessage(msg));
    }

    public void AddHintCounter(CharacterDataSO.Hint hint)
    {
        if(hintShowedCounterDic.ContainsKey(hint))
        {
            hintShowedCounterDic[hint] = hintShowedCounterDic[hint] + 1;
        } else
        {
            hintShowedCounterDic[hint] = 1;
        }
    }

    public int GetCounterOfHint(CharacterDataSO.Hint hint)
    {
        if(hintShowedCounterDic.ContainsKey(hint))
        {
            return hintShowedCounterDic[hint];
        } else
        {
            return 0;
        }
    }

    public void OnPourEvent(GameEventController.IngredientEvent ingredientEvent)
    {
        foreach (CharacterDataSO.Hint hint in currentCharacter.hints)
        {
            if (hint.onEvent.eventType == CharacterDataSO.EvenType.POUR_IN_INGREDIENT)
            {
                foreach (string argument in hint.onEvent.arguments)
                {
                    if (argument == ingredientEvent.ingredient.ingredientSO.ingredientName)
                    {
                        if (GetCounterOfHint(hint) < hint.onEvent.maxAmount)
                        {
                            ShowMessageDelayed(hint.text);
                            AddHintCounter(hint);
                            StartCoroutine(StopTimeFor(5));
                        }
                    }
                }
            }
        }
    }

    public void OnGrabEvent(GameEventController.IngredientEvent ingredientEvent) {
        foreach(CharacterDataSO.Hint hint in currentCharacter.hints)
        {
            if(hint.onEvent.eventType == CharacterDataSO.EvenType.GRAB)
            {
                foreach(string argument in hint.onEvent.arguments)
                {
                    if(argument == ingredientEvent.ingredient.ingredientSO.ingredientName)
                    {
                        if(GetCounterOfHint(hint) < hint.onEvent.maxAmount)
                        {
                            ShowMessageDelayed(hint.text);
                            AddHintCounter(hint);
                            StartCoroutine(StopTimeFor(5));
                        } 
                    }
                }
            }
        }
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

    private IEnumerator CallUpdateEverySecond()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            UpdateEverySecond();
        }
    }
}
