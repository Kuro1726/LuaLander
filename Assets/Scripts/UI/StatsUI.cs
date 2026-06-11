using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statsTextMesh;

    [SerializeField] private GameObject rightArrowGameObject;
    [SerializeField] private GameObject leftArrowGameObject;
    [SerializeField] private GameObject upArrowGameObject;
    [SerializeField] private GameObject downArrowGameObject;

    [SerializeField] private Image fuelAmountImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void UpdateStatsTextMesh()
    {
        rightArrowGameObject.SetActive(Lander.Instance.getSpeedX() > 0);
        leftArrowGameObject.SetActive(Lander.Instance.getSpeedX() < 0);
        upArrowGameObject.SetActive(Lander.Instance.getSpeedY() > 0);
        downArrowGameObject.SetActive(Lander.Instance.getSpeedY() < 0);
        statsTextMesh.text = GameManager.Instance.GetLevelNumber() + "\n"
            + GameManager.Instance.getScore() + "\n"
                                                             + Mathf.Round(GameManager.Instance.getTime()) + "\n"
                                                             + Mathf.Round(Lander.Instance.getSpeedX() * 10) + "\n"
                                                             + Mathf.Round(Lander.Instance.getSpeedY() * 10);
        fuelAmountImage.fillAmount = Lander.Instance.GetFuelAmountNormalized();
    }
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStatsTextMesh();
    }
}
