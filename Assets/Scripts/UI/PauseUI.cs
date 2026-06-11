using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;

    void Awake()
    {
        resumeButton.onClick.AddListener((() =>
        {
            GameManager.Instance.UnpauseGame();
        }));
    }

    void Start()
    {
        GameManager.Instance.PauseGameEvent += GameManager_OnPauseGameEvent;
        GameManager.Instance.UnpauseGameEvent += GameManager_OnUnpauseGameEvent;
        Hide();
    }

    private void GameManager_OnUnpauseGameEvent(object sender, EventArgs e)
    {
        Hide();
    }

    private void GameManager_OnPauseGameEvent(object sender, EventArgs e)
    {
        Show();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
