using System;
using System.Numerics;
using UnityEngine.InputSystem;

public class InputManager
{
    private PlayerControls playerControls;

    public event Action OnPlayerMove;
    public event Action OnPlayerControllMove;

    public InputManager()
    {
        playerControls = new PlayerControls();
        playerControls.Gameplay.Enable();

        playerControls.Gameplay.Movement.performed += OnMovementPerformed;
    }

    private void OnMovementPerformed(InputAction.CallbackContext obj)
    {
        OnPlayerMove?.Invoke();
    }
}
