using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private int score;

    void Start()
    {
        Lander.Instance.OnPickupCoinEvent += Lander_OnCoinPickup;
        Lander.Instance.OnLanded += Lander_OnLanded;
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


    // Update is called once per frame
    void Update()
    {
        
    }
}
