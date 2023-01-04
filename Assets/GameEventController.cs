using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventController : MonoBehaviour
{
    private static GameEventController _this;
    public delegate void OnGrabEvent(IngredientEvent ingredientEvent);
    public delegate void OnPourEvent(IngredientEvent ingredientEvent);
    public OnGrabEvent onGrabEvent;
    public OnPourEvent onPourEvent;

    private void Start()
    {
        _this = this;
    }

    public static GameEventController GetInstance()
    {
        return _this;
    }

    public struct IngredientEvent
    {
        public IngredientEvent(IngredientController ingredient)
        {
            this.ingredient = ingredient;
        }
        public IngredientController ingredient;
    }
}
