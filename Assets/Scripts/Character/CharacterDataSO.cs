using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Character/Character", order = 1)]
public class CharacterDataSO : ScriptableObject
{
    public string characterName;

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
        public float afterTime;
    }
}
