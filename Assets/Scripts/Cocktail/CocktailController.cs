using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DigitalRuby.Tween;

public class CocktailController : MonoBehaviour
{
    int baseFill = 0;
    public IngredientController baseIngredient;
    public IngredientController middleIngredient;
    public IngredientController topIngredient;
    public Image fillImage;

    public Dictionary<IngredientController,int> baseIngredientAmountMap = new Dictionary<IngredientController,int>();

    public List<IngredientController> spices;

    public void AddBase(IngredientController ingredient, int amount)
    {
        if (baseFill >= 100) return;

        if(baseFill + amount >= 100)
        {
            amount = 100 - baseFill;
        }

        if(!baseIngredientAmountMap.ContainsKey(ingredient))
        {
            baseIngredientAmountMap[ingredient] = amount;
        } else
        {
            baseIngredientAmountMap[ingredient] = baseIngredientAmountMap[ingredient] + amount;
        }

        UpdateFill(amount);
    }

    public void UpdateFill(int amount)
    {
        fillImage.fillAmount = (float) baseFill / 100;

        TweenFactory.Tween("coolFill", baseFill, baseFill + amount,0.25f, TweenScaleFunctions.QuadraticEaseIn, (t) => fillImage.fillAmount = (float) t.CurrentValue/100, null);

        baseFill = baseFill + amount;
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
