using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button quitGameButton;

    void Awake()
    {
        startGameButton.onClick.AddListener(() =>
        {
            SceneLoader.LoadScene(SceneLoader.Scene.GamePlayScene);
        });
        quitGameButton.onClick.AddListener((() =>
        {
            Application.Quit();
        }));
    }
}
