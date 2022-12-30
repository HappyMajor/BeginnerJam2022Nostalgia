using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterController : MonoBehaviour
{
    public CharacterDataSO characterData;
    public PathController startPath;
    private DialogController dialogController;
    private Animator animator;

    private void Start()
    {
        dialogController = GameObject.Find("DialogController").GetComponent<DialogController>();
        DisplayHints();
        animator = GetComponent<Animator>();
        startPath.MoveTransformAlongPath(transform, () => {
            Debug.Log("MOVED SUCCESSFULLY!");
        });
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

    public int EvaluateDrink(CocktailController cocktailController)
    {
        int points = 0;
        foreach(CharacterDataSO.Preference preference in characterData.preferences)
        {

            foreach(IngredientController ingredient in cocktailController.baseIngredientAmountMap.Keys)
            {
                if(ingredient.ingredientSO.ingredientName == preference.ingredient.ingredientName)
                {
                    points += preference.points;
                }
            }

            foreach(IngredientController ingredient in cocktailController.spices)
            {
                if(ingredient.ingredientSO.ingredientName == preference.ingredient.ingredientName)
                {
                    points += preference.points;
                }
            }
        }
        return points;
    }

    public void Smile()
    {
        animator.SetBool("smile", true);
    }

    public void StandardFace()
    {
        animator.SetBool("idle", true);
    }

    public void Cry()
    {
        animator.SetBool("cry",true);
    }

}
