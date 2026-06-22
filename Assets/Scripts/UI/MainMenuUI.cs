using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button quitGameButton;
    [SerializeField] private Button instructionButton;

    public event EventHandler openInstruction;
    public static MainMenuUI Instance;

    void Awake()
    {
        Instance = this;
        Time.timeScale = 1f;
        startGameButton.onClick.AddListener(() =>
        {
            GameManager.ResetStaticData();
            SceneLoader.LoadScene(SceneLoader.Scene.GamePlayScene);
        });
        quitGameButton.onClick.AddListener((() =>
        {
            Application.Quit();
        }));
        instructionButton.onClick.AddListener(() =>
        {
            openInstruction?.Invoke(this, EventArgs.Empty);
        });
    }

    void Start()
    {
        startGameButton.Select();;
    }
    
    
}
