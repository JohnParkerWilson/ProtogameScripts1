using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInputActions inputActions;

    // Movement
    public Vector2 MoveInput { get; private set; }

    // Events
    public event Action JumpPressed;
    public event Action DashPressed;
    public event Action HandLFirePressed;
    public event Action HandRFirePressed;
    public event Action BackLFirePressed;
    public event Action BackRFirePressed;


    public bool IsJumpHeld { get; private set; }

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();

        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;

        inputActions.Player.Jump.performed += OnJump;
        inputActions.Player.Jump.canceled += OnJumpCanceled;
        inputActions.Player.Dash.performed += OnDash;
        inputActions.Player.LeftShoot.performed += OnFireHandL;
        inputActions.Player.RightShoot.performed += OnFireHandR;
        inputActions.Player.LeftBackShoot.performed += OnFireBackL;
        inputActions.Player.RightBackShoot.performed += OnFireBackR;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Move.canceled -= OnMove;

        inputActions.Player.Jump.performed -= OnJump;
        inputActions.Player.Jump.canceled -= OnJumpCanceled;
        inputActions.Player.Dash.performed -= OnDash;
        inputActions.Player.LeftShoot.performed -= OnFireHandL;
        inputActions.Player.RightShoot.performed -= OnFireHandR;
        inputActions.Player.LeftBackShoot.performed -= OnFireBackL;
        inputActions.Player.RightBackShoot.performed -= OnFireBackR;

        inputActions.Player.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        IsJumpHeld = true;

        JumpPressed?.Invoke();
    }

    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
        IsJumpHeld = false;
    }

    private void OnDash(InputAction.CallbackContext context)
    {
        DashPressed?.Invoke();
    }

    private void OnFireHandL(InputAction.CallbackContext context)
    {
        HandLFirePressed?.Invoke();
    }
    private void OnFireHandR(InputAction.CallbackContext context)
    {
        HandRFirePressed?.Invoke();
    }
    private void OnFireBackL(InputAction.CallbackContext context)
    {
        BackLFirePressed?.Invoke();
    }
    private void OnFireBackR(InputAction.CallbackContext context)
    {
        BackRFirePressed?.Invoke();
    }

}