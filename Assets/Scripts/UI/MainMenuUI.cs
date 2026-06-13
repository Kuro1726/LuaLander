using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button quitGameButton;

    void Awake()
    {
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
    }

    void Start()
    {
        startGameButton.Select();;
    }
}
