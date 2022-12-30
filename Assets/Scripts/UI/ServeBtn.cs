using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServeBtn : MonoBehaviour
{
    public CocktailController cocktailController;
    public CharacterController characterController;

    public void OnClick()
    {
        int points = characterController.EvaluateDrink(cocktailController);
        GameStats.GetInstance().SetScore(points);

        if(points == 0)
        {
            characterController.Cry();
        } else if(points >= 10 && points <= 20)
        {
            characterController.Smile();
        }
    }

    public void Test()
    {

    }
}
