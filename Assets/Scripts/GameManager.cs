using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private int score;
    private float time;
    private bool isTimeActive = false;

    private static int levelNumber = 1;
    [SerializeField] private List<GameLevel> gameLevelList;
    
    public static GameManager Instance;

    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        Lander.Instance.OnPickupCoinEvent += Lander_OnCoinPickup;
        Lander.Instance.OnLanded += Lander_OnLanded;
        Lander.Instance.StateChanged += Lander_OnStateChanged;
        LoadCurrentLevel();
        
    }

    private void Lander_OnStateChanged(object sender, Lander.StateChangedEventArgs e)
    {
        isTimeActive = Lander.State.Normal == e.state;
    }

    private void LoadCurrentLevel()
    {
        foreach (GameLevel gameLevel in gameLevelList)
        {
            if (gameLevel.GetLevelNumber() == levelNumber)
            {
                GameLevel spawnGameLevel = Instantiate(gameLevel, Vector3.zero, Quaternion.identity);
                Lander.Instance.transform.position = spawnGameLevel.GetLanderSpawnPoint();
            }
        }
    }

    public int GetLevelNumber()
    {
        return levelNumber;
    }

    public void GoToNextLevel()
    {
        levelNumber++;
        SceneManager.LoadScene(0);
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(0);
    }

    void Update()
    {
        if (isTimeActive)
        {
            time += Time.deltaTime;
            
        }
    }

    private void Lander_OnLanded(object sender, Lander.OnLandedEventArgs e)
    {
        AddScore(e.score);
    }

    private void Lander_OnCoinPickup(object sender, EventArgs e)
    {
        int coinScore = 500;
        AddScore(coinScore);
    }

    public void AddScore(int scoreAmount)
    {
        score += scoreAmount;
        Debug.Log(score);
    }

    public int getScore()
    {
        return score;
    }

    public float getTime()
    {
        return time;
    }
}
