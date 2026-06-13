using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LandedUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleTextMesh;
    [SerializeField] private TextMeshProUGUI statTextMesh;
    [SerializeField] private TextMeshProUGUI nextGameButtonTextMesh;
    [SerializeField] private Button nextGameButton;

    private Action nextButtonClickAcion;

    private void Awake()
    {
        nextGameButton.onClick.AddListener(() =>
        {
            nextButtonClickAcion();
        });
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Lander.Instance.OnLanded += OnLanded;
        Hide();
    }

    private void OnLanded(object sender, Lander.OnLandedEventArgs e)
    {
        if (e.landingType == Lander.LandingType.Success)
        {
            titleTextMesh.text = "SUCCESSFUL LANDING!";
            nextGameButtonTextMesh.text = "NEXT LEVEL";
            nextButtonClickAcion = GameManager.Instance.GoToNextLevel;
        }
        else
        {
            titleTextMesh.text = "<color=#ff0000>CRASH!!!</color>";
            nextGameButtonTextMesh.text = "RETRY";
            nextButtonClickAcion = GameManager.Instance.RetryLevel;
        }
        statTextMesh.text = GameManager.Instance.GetLevelNumber() + "\n"
            + Mathf.Round(e.landingSpeed * 3) + "\n"
                                           + Mathf.Round(e.landingAngle * 2) + "\n"
                                           + e.multiplier + "\n"
                                           + e.score;
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
        nextGameButton.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
