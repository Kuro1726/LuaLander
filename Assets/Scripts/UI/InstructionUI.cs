using System;
using UnityEngine;
using UnityEngine.UI;

public class InstructionUI : MonoBehaviour
{
    
    [SerializeField] private Button closeButton;

    void Awake()
    {
        closeButton.onClick.AddListener(() =>
        {
            Hide();
            SceneLoader.LoadScene(SceneLoader.Scene.MainMenuScene);
        });
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MainMenuUI.Instance.openInstruction += MainMenuUI_OnopenInstruction;
        Hide();
    }

    private void MainMenuUI_OnopenInstruction(object sender, EventArgs e)
    {
        Show();
    }
    

    void Show()
    {
        gameObject.SetActive(true);
        closeButton.Select();
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }
}
