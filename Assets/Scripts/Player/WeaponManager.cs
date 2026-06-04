using System.Transactions;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour
{
    private PlayerInputActions inputActions;
    private PlayerInputHandler inputHandler;
    private ThirdPersonController controller;

    public Weapon currentWeaponHandL;
    public Weapon currentWeaponHandR;
    public Weapon currentWeaponBackL;
    public Weapon currentWeaponBackR;

    public bool IsHandLHeld { get; private set; }
    public bool IsHandRHeld { get; private set; }
    public bool IsBackLHeld { get; private set; }
    public bool IsBackRHeld { get; private set; }

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputHandler = GetComponent<PlayerInputHandler>();
        controller = GetComponent<ThirdPersonController>();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();

        inputActions.Player.LeftShoot.performed += OnFireHandL;
        inputActions.Player.LeftShoot.canceled += OnFireHandLCanceled;
        inputActions.Player.RightShoot.performed += OnFireHandR;
        inputActions.Player.RightShoot.canceled += OnFireHandRCanceled;
        inputActions.Player.LeftBackShoot.performed += OnFireBackL;
        inputActions.Player.LeftBackShoot.canceled += OnFireBackLCanceled;
        inputActions.Player.RightBackShoot.performed += OnFireBackR;
        inputActions.Player.RightBackShoot.canceled += OnFireBackRCanceled;
    }

    private void OnDisable()
    {
        inputActions.Player.LeftShoot.performed -= OnFireHandL;
        inputActions.Player.LeftShoot.canceled -= OnFireHandLCanceled;
        inputActions.Player.RightShoot.performed -= OnFireHandR;
        inputActions.Player.RightShoot.canceled -= OnFireHandRCanceled;
        inputActions.Player.LeftBackShoot.performed -= OnFireBackL;
        inputActions.Player.LeftBackShoot.canceled -= OnFireBackLCanceled;
        inputActions.Player.RightBackShoot.performed -= OnFireBackR;
        inputActions.Player.RightBackShoot.canceled -= OnFireBackRCanceled;

        inputActions.Player.Disable();
    }


    private void Update()
    {
        // Left Hand Shooting
        if(IsHandLHeld)
        {
            FireHandLWeapon();
            currentWeaponHandL.Charging();
        }
        if(inputActions.Player.LeftShoot.WasReleasedThisFrame())
        {
            currentWeaponHandL.ReleaseCharge();
        }
        
        // Right Hand Shooting
        if (IsHandRHeld)
        {
            FireHandRWeapon();
            currentWeaponHandR.Charging();
        }
        if (inputActions.Player.RightShoot.WasReleasedThisFrame())
        {
            currentWeaponHandR.ReleaseCharge();
        }

        // Left Back Shooting
        if (IsBackLHeld)
        {
            FireBackLWeapon();
            currentWeaponBackL.Charging();
        }
        if (inputActions.Player.LeftBackShoot.WasReleasedThisFrame())
        {
            currentWeaponBackL.ReleaseCharge();
        }

        // Right Back Shooting
        if (IsBackRHeld)
        {
            FireBackRWeapon();
            currentWeaponBackR.Charging();
        }
        if (inputActions.Player.RightBackShoot.WasReleasedThisFrame())
        {
            currentWeaponBackR.ReleaseCharge();
        }
    }


    // Left Hand Weapon Inputs
    private void OnFireHandL(InputAction.CallbackContext context)
    {
        if (currentWeaponHandL == null)
            return;

        IsHandLHeld = true;
        currentWeaponHandL.IsFireHeld = true;

    }
    private void OnFireHandLCanceled(InputAction.CallbackContext context)
    {
        IsHandLHeld = false;
        currentWeaponHandL.IsFireHeld = false;
    }
    private void FireHandLWeapon()
    {
        if (currentWeaponHandL == null)
            return;

        currentWeaponHandL.AimPoint = controller.CurrentAimPoint;
        currentWeaponHandL.Fire();
    }

    // Right Hand Weapon Inputs
    private void OnFireHandR(InputAction.CallbackContext context)
    {
        if (currentWeaponHandR == null)
            return;

        IsHandRHeld = true;
        currentWeaponHandR.IsFireHeld = true;

    }
    private void OnFireHandRCanceled(InputAction.CallbackContext context)
    {
        IsHandRHeld = false;
        currentWeaponHandR.IsFireHeld = false;
    }
    private void FireHandRWeapon()
    {
        if (currentWeaponHandR == null)
            return;

        currentWeaponHandR.AimPoint = controller.CurrentAimPoint;
        currentWeaponHandR.Fire();
    }

    // Left Back Weapon Inputs
    private void OnFireBackL(InputAction.CallbackContext context)
    {
        if (currentWeaponBackL == null)
            return;

        IsBackLHeld = true;
        currentWeaponBackL.IsFireHeld = true;

    }
    private void OnFireBackLCanceled(InputAction.CallbackContext context)
    {
        IsBackLHeld = false;
        currentWeaponBackL.IsFireHeld = false;
    }
    private void FireBackLWeapon()
    {
        if (currentWeaponBackL == null)
            return;

        currentWeaponBackL.AimPoint = controller.CurrentAimPoint;
        currentWeaponBackL.Fire();
    }

    // Right Back Weapon Inputs
    private void OnFireBackR(InputAction.CallbackContext context)
    {
        if (currentWeaponBackR == null)
            return;

        IsBackRHeld = true;
        currentWeaponBackR.IsFireHeld = true;

    }
    private void OnFireBackRCanceled(InputAction.CallbackContext context)
    {
        IsBackRHeld = false;
        currentWeaponBackR.IsFireHeld = false;
    }
    private void FireBackRWeapon()
    {
        if (currentWeaponBackR == null)
            return;

        currentWeaponBackR.AimPoint = controller.CurrentAimPoint;
        currentWeaponBackR.Fire();
    }

}