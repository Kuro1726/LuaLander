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
    
    private int lastLevel = -1;
    private int lastScore = -1;
    private int lastTime = -1;
    private int lastSpeedX = -1;
    private int lastSpeedY = -1;

    private void UpdateStatsTextMesh()
    {
        if (Lander.Instance == null || GameManager.Instance == null) return;
        
        float currentSpeedX = Lander.Instance.getSpeedX();
        float currentSpeedY = Lander.Instance.getSpeedY();
        
        
        rightArrowGameObject.SetActive(currentSpeedX > 0);
        leftArrowGameObject.SetActive(currentSpeedX < 0);
        upArrowGameObject.SetActive(currentSpeedY > 0);
        downArrowGameObject.SetActive(currentSpeedY < 0);
        
        fuelAmountImage.fillAmount = Lander.Instance.GetFuelAmountNormalized();
        
        int currentLevelInt = GameManager.Instance.GetLevelNumber();
        int currentScoreInt = GameManager.Instance.getScore();
        int currentTimeInt = Mathf.RoundToInt(GameManager.Instance.getTime());
        int currentSpeedXInt = Mathf.RoundToInt(currentSpeedX * 10);
        int currentSpeedYInt = Mathf.RoundToInt(currentSpeedY * 10);
        
        if (currentLevelInt != lastLevel || currentScoreInt != lastScore || 
            currentTimeInt != lastTime || currentSpeedXInt != lastSpeedX || currentSpeedYInt != lastSpeedY)
        {
            // Gán lại giá trị cũ
            lastLevel = currentLevelInt;
            lastScore = currentScoreInt;
            lastTime = currentTimeInt;
            lastSpeedX = currentSpeedXInt;
            lastSpeedY = currentSpeedYInt;

            // Cập nhật giao diện
            statsTextMesh.text = $"{currentLevelInt}\n{currentScoreInt}\n{currentTimeInt}\n{currentSpeedXInt}\n{currentSpeedYInt}";
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStatsTextMesh();
    }
}
