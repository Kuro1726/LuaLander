using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static int totalScore = 0;
    private static int levelNumber = 1;

    private int score;
    private float time;
    private bool isTimeActive = false;

    [SerializeField] private List<GameLevel> gameLevelList;
    
    public static GameManager Instance { get; private set; }

    public event EventHandler PauseGameEvent;
    public event EventHandler UnpauseGameEvent;

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
        GameInput.Instance.OnMenuButton += GameInputs_OnMenuButton;
    }

    private void GameInputs_OnMenuButton(object sender, EventArgs e)
    {
        PauseUnpauseGame();
    }

    private void Lander_OnStateChanged(object sender, Lander.StateChangedEventArgs e)
    {
        isTimeActive = Lander.State.Normal == e.state;
    }

    private GameLevel GetGameLevel()
    {
        foreach (GameLevel gameLevel in gameLevelList)
        {
            if (gameLevel.GetLevelNumber() == levelNumber)
            {
                return gameLevel;
            }
        }
        return null;
    }

    private void LoadCurrentLevel()
    {
        GameLevel gameLevel = GetGameLevel();
        if (gameLevel != null)
        {
            GameLevel spawnGameLevel = Instantiate(gameLevel, Vector3.zero, Quaternion.identity); 
            Lander.Instance.transform.position = spawnGameLevel.GetLanderSpawnPoint();
        }
        else
        {
            Debug.LogError($"❌ Cannot find data for Level {levelNumber}.");
        }
    }

    public int GetLevelNumber()
    {
        return levelNumber;
    }

    public void GoToNextLevel()
    {
        levelNumber++;
        totalScore += score;
        Debug.Log("LEVEL NUMBER: " + levelNumber);
        Debug.Log("GAME LEVEL LIST COUNT: " + gameLevelList.Count);
        if (GetGameLevel() == null)
        {
            SceneLoader.LoadScene(SceneLoader.Scene.GameOverScene);
        }
        else
        {
            SceneLoader.LoadScene(SceneLoader.Scene.GamePlayScene);
        }
    }

    public int GetToalScore()
    {
        return totalScore;
    }

    public void RetryLevel()
    {
        SceneLoader.LoadScene(SceneLoader.Scene.GamePlayScene);
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

    private void PauseUnpauseGame()
    {
        if (Time.timeScale == 1f)
        {
            PauseGame();
        }
        else 
            UnpauseGame();
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        PauseGameEvent?.Invoke(this ,EventArgs.Empty);
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1f;
        UnpauseGameEvent?.Invoke(this, EventArgs.Empty);
    }

    public static void ResetStaticData()
    {
        levelNumber = 1;
        totalScore = 0;
    }
    
    // THÊM HÀM NÀY ĐỂ FIX LỖI TRIỆT ĐỂ
    private void OnDestroy()
    {
        if (GameInput.Instance != null)
        {
            GameInput.Instance.OnMenuButton -= GameInputs_OnMenuButton;
        }
    }
}
