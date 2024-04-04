using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Typer typer;
    public WordBank wordBank;
    public AudioSource audioSource;

    [Header("Timer")]
    [SerializeField] TextMeshProUGUI timerTxt;
    [SerializeField] float currentTime;

    [Header("Score")]
    public float bestTimeValue;
    public TextMeshProUGUI currentTimeScoreTxt;
    public TextMeshProUGUI BestTimeScoreTxt;
    public GameObject gameOverPanel;

    [Header("PausePanel")]
    public GameObject pausePanel;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        currentTime = 0f;
        bestTimeValue = PlayerPrefs.GetFloat("player_HighScore", 0f);
        Debug.Log("Loaded High Score: " + bestTimeValue);

    }
    private void Update()
    {
        currentTime += Time.deltaTime;
        float truncatedTime = Mathf.Floor(currentTime * 1000) / 1000; // Include milliseconds
        int minutes = Mathf.FloorToInt(truncatedTime / 60);
        int seconds = Mathf.FloorToInt(truncatedTime % 60);
        int milliseconds = Mathf.FloorToInt((truncatedTime * 1000) % 1000); // Extract milliseconds
        timerTxt.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds / 10); // Include milliseconds with two decimal places
    }
    public void GameOver()
    {
        Debug.Log("Game Over!");
        Time.timeScale = 0;
        ScoringTime();
        
    }

    public void WrongLetter()
    {
        audioSource.Play();
    }

    public void ScoringTime()
    {
        gameOverPanel.SetActive(true);

        if (currentTime < bestTimeValue || bestTimeValue == 0)
        {
            bestTimeValue = currentTime;
            PlayerPrefs.SetFloat("player_HighScore", bestTimeValue);
            Debug.Log("New High Score: " + bestTimeValue);
        }
        else
        {
            Debug.Log("Not a Highscore.");
        }

        currentTimeScoreTxt.text = currentTime.ToString("F2");
        BestTimeScoreTxt.text = bestTimeValue.ToString("F2");
    }

    public void PausedGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void UnpausedGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

}

