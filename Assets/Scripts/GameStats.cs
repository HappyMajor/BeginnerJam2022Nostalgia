using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStats : MonoBehaviour
{
    public List<CharacterDataSO> characters;
    public CharacterController characterController;

    private static GameStats _this;
    private int currentCharacterIndex;

    public int health = 3;

    public GameObject endGameMenu;

    private void Start()
    {
        _this = this;
    }

    public static GameStats GetInstance()
    {
        return _this;
    }

    public static int score;
    public TMP_Text scoreDisplay;

    public void SetScore(int newScore)
    {
        scoreDisplay.text = "score: " + newScore;
        score = newScore;
    }

    public void NextCharacter()
    {
        currentCharacterIndex++;
        if (currentCharacterIndex < characters.Count)
        {
            characterController.characterData = characters[currentCharacterIndex];
        } else
        {
            //Was last character end game
        }
    }

    public void EndGame()
    {
        Debug.Log("End of game!");
    }

    public void LostGame()
    {
        Debug.Log("Lost game!");
    }

}
