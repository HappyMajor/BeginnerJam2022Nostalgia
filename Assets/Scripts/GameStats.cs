using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStats : MonoBehaviour
{

    private static GameStats _this;

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


}
