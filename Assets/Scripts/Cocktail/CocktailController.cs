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
        AddColor(ingredient.ingredientSO.baseColor, amount);
    }

    public void AddColor(Color color, int amount)
    {
        Vector3 mixedColor = new Vector3(0f, 0f, 0f);
        int wholeAmount = 0;

        foreach (IngredientController key in baseIngredientAmountMap.Keys)
        {
            mixedColor += new Vector3(key.ingredientSO.baseColor.r, key.ingredientSO.baseColor.g, key.ingredientSO.baseColor.b) * baseIngredientAmountMap[key];
            wholeAmount += baseIngredientAmountMap[key];
        }
        mixedColor = (mixedColor / wholeAmount);
        fillImage.color = new Color(mixedColor.x, mixedColor.y, mixedColor.y, fillImage.color.a);
    }

    public void UpdateFill(int amount)
    {
        fillImage.fillAmount = (float) baseFill / 100;

        TweenFactory.Tween("coolFill", baseFill, baseFill + amount,0.25f, TweenScaleFunctions.QuadraticEaseIn, (t) => fillImage.fillAmount = (float) t.CurrentValue/100, null);

        baseFill = baseFill + amount;
    }

    public bool isFull()
    {
        if(baseFill >= 100)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public void ResetDrink()
    {
        baseIngredientAmountMap = new Dictionary<IngredientController, int>();
        fillImage.fillAmount = 0;
        fillImage.color = Color.white;
        baseFill = 0;
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
