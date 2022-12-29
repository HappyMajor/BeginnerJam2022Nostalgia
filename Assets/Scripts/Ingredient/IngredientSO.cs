using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IngredientData", menuName = "Character/Ingredient", order = 1)]
public class IngredientSO : ScriptableObject
{
    public string ingredientName;
    public IngredientType type;
    public Sprite sprite;

    public enum IngredientType
    {
        BASE
    }
}
