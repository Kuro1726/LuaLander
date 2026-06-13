using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private TextMeshProUGUI finalScore;

    void Awake()
    {
        mainMenuButton.onClick.AddListener((() =>
        {
            SceneLoader.LoadScene(SceneLoader.Scene.MainMenuScene);
        }));
    }

    void Start()
    {
        int totalScore = GameManager.Instance.GetToalScore();
        finalScore.text = "FINAL SCORE: " + totalScore;
        mainMenuButton.Select();
    }
}
