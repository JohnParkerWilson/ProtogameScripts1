using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private PlayerInputHandler inputHandler;
    private ThirdPersonController controller;

    public Weapon currentWeaponHandL;
    public Weapon currentWeaponHandR;
    public Weapon currentWeaponBackL;
    public Weapon currentWeaponBackR;

    private void Awake()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
        controller = GetComponent<ThirdPersonController>();
    }

    private void OnEnable()
    {
        inputHandler.HandLFirePressed += FireLHandWeapon;
        inputHandler.HandRFirePressed += FireRHandWeapon;
        inputHandler.BackLFirePressed += FireLBackWeapon;
        inputHandler.BackRFirePressed += FireRBackWeapon;
    }

    private void OnDisable()
    {
        inputHandler.HandLFirePressed -= FireLHandWeapon;
        inputHandler.HandRFirePressed -= FireRHandWeapon;
        inputHandler.BackLFirePressed -= FireLBackWeapon;
        inputHandler.BackRFirePressed -= FireRBackWeapon;
    }

    private void FireLHandWeapon()
    {
        if (currentWeaponHandL == null)
            return;

        currentWeaponHandL.AimPoint = controller.CurrentAimPoint;

        currentWeaponHandL.Fire();
    }
    private void FireRHandWeapon()
    {
        if (currentWeaponHandL == null)
            return;

        currentWeaponHandR.AimPoint = controller.CurrentAimPoint;

        currentWeaponHandR.Fire();
    }
    private void FireLBackWeapon()
    {
        if (currentWeaponHandL == null)
            return;

        currentWeaponBackL.AimPoint = controller.CurrentAimPoint;

        currentWeaponBackL.Fire();
    }
    private void FireRBackWeapon()
    {
        if (currentWeaponHandL == null)
            return;

        currentWeaponBackR.AimPoint = controller.CurrentAimPoint;

        currentWeaponBackR.Fire();
    }
}