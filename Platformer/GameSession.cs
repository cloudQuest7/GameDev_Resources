using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3; 
    [SerializeField] int Score = 0; 
    [SerializeField] TextMeshProUGUI livesText; 
    [SerializeField] TextMeshProUGUI scoreText;
    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject); // Ensure only one GameSession exists
        }
        else
        {
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
    }
    
    void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = Score.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    void TakeLife()
    {
        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        livesText.text = playerLives.ToString();
        SceneManager.LoadScene(currentSceneIndex); 
    }

    public void AddToScore(int pointsToAdd)
    {
        Score += pointsToAdd;
        scoreText.text = Score.ToString();
    }

    void ResetGameSession()
    {
        SceneManager.LoadScene(0); // Load the first scene (usually the main menu or start scene)
        Destroy(gameObject); // Destroy the GameSession object to reset the game state
    }
}
