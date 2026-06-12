using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button soundButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private TextMeshProUGUI soundButtonText;
    [SerializeField] private TextMeshProUGUI musicButtonText;
    

    void Awake()
    {
        resumeButton.onClick.AddListener((() =>
        {
            GameManager.Instance.UnpauseGame();
        }));
        soundButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeSoundValue();
            soundButtonText.text = "SOUND " + SoundManager.Instance.GetSoundVolume();
        });
        musicButton.onClick.AddListener(() =>
        {
            
        });
    }

    void Start()
    {
        GameManager.Instance.PauseGameEvent += GameManager_OnPauseGameEvent;
        GameManager.Instance.UnpauseGameEvent += GameManager_OnUnpauseGameEvent;
        soundButtonText.text = "SOUND " + SoundManager.Instance.GetSoundVolume();
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
