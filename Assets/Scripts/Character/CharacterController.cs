using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterController : MonoBehaviour
{
    public CharacterDataSO characterData;
    private DialogController dialogController;

    private void Start()
    {
        dialogController = GameObject.Find("DialogController").GetComponent<DialogController>();
        DisplayHints();
    }

    private void DisplayHints()
    {
        foreach(CharacterDataSO.Hint hint in characterData.hints)
        {
            StartCoroutine(DoLater(hint.afterTime, () => dialogController.ShowMessageDelayed(hint.text)));
        }
    }

    private IEnumerator DoLater(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action();
    }

}
