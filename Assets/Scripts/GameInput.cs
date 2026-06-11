using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public event EventHandler OnMenuButton;
    private InputActions inputActions;
    private void Awake()
    {
        Instance = this;
        inputActions = new InputActions();
        inputActions.Enable();
        
        inputActions.Player.Menu.performed += Menu_Onperformed; 
    }

    private void Menu_Onperformed(InputAction.CallbackContext obj)
    {
        OnMenuButton?.Invoke(this, EventArgs.Empty);
    }
}
