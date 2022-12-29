using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CocktailController : MonoBehaviour
{
    public IngredientController baseIngredient;
    public IngredientController middleIngredient;
    public IngredientController topIngredient;

    public List<IngredientController> spices;

    public void AddIngredient(IngredientController ingredient)
    {
        Debug.Log("Add Ingredient: " + ingredient.ingredientSO.name);
        if(ingredient.ingredientSO.type == IngredientSO.IngredientType.BASE)
        {
            AddBase(ingredient);
        }
    }

    public void AddBase(IngredientController ingredient)
    {
        if(ingredient.ingredientSO.type == IngredientSO.IngredientType.BASE)
        {
            baseIngredient = ingredient;
        }
    }

    public void AddMiddle(IngredientController ingredient)
    {

    }

    public void AddTop(IngredientController ingredient)
    {

    }

    public void addSpcie(IngredientController ingredient)
    {

    }
}
