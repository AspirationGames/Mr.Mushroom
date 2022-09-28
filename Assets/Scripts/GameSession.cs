using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int playerScore = 0;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    public static Vector2 lastCheckPoint = new Vector2 (22, -8.5f);
    
    private void Awake() 
    {

        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        
        GameObject.FindGameObjectWithTag("Player").transform.position = lastCheckPoint;
        
        
    }

    private void Start() 
    {
        livesText.text = "Lives: " + playerLives;
        scoreText.text = "Score: " + playerScore;
            
    }
    public void ProcessPlayerDeath()
    {
        if(playerLives > 1)
        {
           Invoke("TakeLife", 2f); 
           //TakeLife();
        }
        else
        {
            Debug.Log("Game Over");
            lastCheckPoint = new Vector2 (22, -8.5f);
            ResetGameSession();
        }
    }

    public void UpdateScore(int points)
    {
        playerScore += points;
        scoreText.text = "Score: " + playerScore;
    }
    private void TakeLife()
    {
        playerLives--;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        livesText.text = "Lives: " + playerLives;
    }
    private void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();

        //Demo Set-Up
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


        //Multiple Level Set-Up
        //SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
