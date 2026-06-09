using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LandedUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleTextMesh;

    [SerializeField] private TextMeshProUGUI statTextMesh;

    [SerializeField] private Button nextGameButton;

    void Awake()
    {
        nextGameButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Lander.Instance.OnLanded += OnLanded;
        ;
        Hide();
    }

    private void OnLanded(object sender, Lander.OnLandedEventArgs e)
    {
        if (e.landingType == Lander.LandingType.Success)
        {
            titleTextMesh.text = "SUCCESSFUL LANDING!";
        }
        else
        {
            titleTextMesh.text = "<color=#ff0000>CRASH!!!</color>";
        }
        statTextMesh.text = Mathf.Round(e.landingSpeed * 3) + "\n"
                                           + Mathf.Round(e.landingAngle * 2) + "\n"
                                           + e.multiplier + "\n"
                                           + e.score;
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
