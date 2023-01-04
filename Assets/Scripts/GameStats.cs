using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameStats : MonoBehaviour
{
    public List<CharacterDataSO> characters;
    public CharacterController characterController;

    private static GameStats _this;
    private int currentCharacterIndex;

    public AudioSource barMusicSource;
    public AudioClip barMMusic;

    public int health = 1;

    public GameObject lostGameMenu;
    public GameObject scoreMenu;

    public GameObject renderCamera;
    public GameObject secondaryCamera;

    public AudioSource audioSource;
    public AudioClip win;
    public AudioClip lost;



    private void Start()
    {
        _this = this;
        this.secondaryCamera.SetActive(true);
        this.renderCamera.GetComponent<Camera>().targetDisplay = 0;
        this.secondaryCamera.GetComponent<Camera>().targetDisplay = 0;

        StartCoroutine(MainCameraCtivate());

    }

    public IEnumerator MainCameraCtivate()
    {
        yield return new WaitForSeconds(0.1f);
        this.renderCamera.GetComponent<Camera>().targetDisplay = 0;
        this.renderCamera.SetActive(true);
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

    public void AddScore(int amount)
    {
        score += amount;
        SetScore(score);
    }

    public void RemoveHealthPoint()
    {
        health--;
        if(health <= 0)
        {
            LostGame();
        }
    }

    public void NextCharacter()
    {
        currentCharacterIndex++;
        if (currentCharacterIndex < characters.Count)
        {
            characterController.characterData = characters[currentCharacterIndex];
            characterController.EnterBar();
        } else
        {
            //Was last character end game
            EndGame();
        }
    }

    public void EndGame()
    {
        Debug.Log("End of game!");
        scoreMenu.SetActive(true);
    }

    public void LostGame()
    {
        Debug.Log("Lost game!");
        lostGameMenu.SetActive(true);
        audioSource.PlayOneShot(lost);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
