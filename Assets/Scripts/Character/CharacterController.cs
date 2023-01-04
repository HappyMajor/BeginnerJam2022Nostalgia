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

    public AudioSource audioSource;
    public AudioClip win;
    public AudioClip lose;

    private void Start()
    {
        dialogController = GameObject.Find("DialogController").GetComponent<DialogController>();
        EnterBar();
    }

    public void EnterBar()
    {
        dialogController.Reset(characterData);
        GetComponent<SpriteRenderer>().sprite = characterData.idle;
        StopAllCoroutines();
        animator = GetComponent<Animator>();
        startPath.MoveTransformAlongPath(transform, () => {
        });
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
        if(characterData.win != null)
        {
            GetComponent<SpriteRenderer>().sprite = characterData.win;
        }
    }


    public void StandardFace()
    {
        animator.SetBool("idle", true);
    }

    public void Cry()
    {
        animator.SetBool("cry",true);
        dialogController.ShowMessageDelayed("Ewww!!!!! Worst drink ever!!!");
    }

}
