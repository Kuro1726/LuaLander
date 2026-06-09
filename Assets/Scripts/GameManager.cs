using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private int score;
    private float time;
    private bool isTimeActive = false;
    
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
    }

    private void Lander_OnStateChanged(object sender, Lander.StateChangedEventArgs e)
    {
        isTimeActive = Lander.State.Normal == e.state;
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
