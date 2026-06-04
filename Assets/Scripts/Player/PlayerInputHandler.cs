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
    //public event Action HandLFirePressed;
    //public event Action HandRFirePressed;
    //public event Action BackLFirePressed;
    //public event Action BackRFirePressed;


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
        //inputActions.Player.LeftShoot.performed += OnFireHandL;
        //inputActions.Player.LeftShoot.canceled += OnFireHandLCanceled;
        //inputActions.Player.RightShoot.performed += OnFireHandR;
        //inputActions.Player.RightShoot.canceled += OnFireHandRCanceled;
        //inputActions.Player.LeftBackShoot.performed += OnFireBackL;
        //inputActions.Player.LeftBackShoot.canceled += OnFireBackLCanceled;
        //inputActions.Player.RightBackShoot.performed += OnFireBackR;
        //inputActions.Player.RightBackShoot.canceled += OnFireBackRCanceled;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Move.canceled -= OnMove;

        inputActions.Player.Jump.performed -= OnJump;
        inputActions.Player.Jump.canceled -= OnJumpCanceled;
        inputActions.Player.Dash.performed -= OnDash;
        //inputActions.Player.LeftShoot.performed -= OnFireHandL;
        //inputActions.Player.LeftShoot.canceled -= OnFireHandLCanceled;
        //inputActions.Player.RightShoot.performed -= OnFireHandR;
        //inputActions.Player.RightShoot.canceled -= OnFireHandRCanceled;
        //inputActions.Player.LeftBackShoot.performed -= OnFireBackL;
        //inputActions.Player.LeftBackShoot.canceled -= OnFireBackLCanceled;
        //inputActions.Player.RightBackShoot.performed -= OnFireBackR;
        //inputActions.Player.RightBackShoot.canceled -= OnFireBackRCanceled;

        inputActions.Player.Disable();
    }


    // Movement Inputs
    private void OnMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }

    // Jump Inputs
    private void OnJump(InputAction.CallbackContext context)
    {
        IsJumpHeld = true;

        JumpPressed?.Invoke();
    }

    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
        IsJumpHeld = false;
    }

    // Dash Inputs
    private void OnDash(InputAction.CallbackContext context)
    {
        DashPressed?.Invoke();
    }


}