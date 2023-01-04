using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Character/Character", order = 1)]
public class CharacterDataSO : ScriptableObject
{
    public string characterName;

    public Sprite idle;
    public Sprite win;

    public List<Hint> hints;

    [SerializeField]
    public List<Preference> preferences;

    [Serializable]
    public struct Preference
    {
        public IngredientSO ingredient;
        public int points;
    }

    [Serializable]
    public struct Hint
    {
        public string text;
        [SerializeField]
        public Event onEvent;
    }

    [Serializable]
    public struct Event
    {
        public EvenType eventType;
        public List<string> arguments;
        public int maxAmount;
    }

    [Serializable]
    public enum EvenType
    {
        EMPTY, AFTER_TIME, HOVER, GRAB, POUR_IN_WRONG_INGREDIENT, POUR_IN_INGREDIENT,
    }
}
