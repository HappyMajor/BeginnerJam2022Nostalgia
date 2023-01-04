using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ServeBtn : MonoBehaviour
{
    public CocktailController cocktailController;
    public CharacterController characterController;

    public void OnClick()
    {
        int points = characterController.EvaluateDrink(cocktailController);
        GameStats.GetInstance().AddScore(points);
        if(points == 0)
        {
            characterController.Cry();
            GameStats.GetInstance().RemoveHealthPoint();
            GameStats.GetInstance().audioSource.PlayOneShot(GameStats.GetInstance().lost);
            GameStats.GetInstance().NextCharacter();
        } else if(points >= 10)
        {
            GameStats.GetInstance().audioSource.PlayOneShot(GameStats.GetInstance().win);

            if (characterController.characterData.win != null)
            {
                ServeBtn serveBtn = this;
                StartCoroutine(DoLater(2f, () => {
                    characterController.Smile();
                }));
            }
            StartCoroutine(DoLater(5f, () =>
            {
                GameStats.GetInstance().NextCharacter();
            }));
        }

        cocktailController.ResetDrink();
    }

    public IEnumerator DoLater(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action();
    }

    public void Test()
    {

    }
}
