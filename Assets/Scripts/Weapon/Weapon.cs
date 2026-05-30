using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Weapon Data")]
    public WeaponData weaponData;

    [Header("References")]
    public Transform shootOrigin;

    public Vector3 AimPoint { get; set; }

    protected float nextFireTime;

    public virtual bool CanFire()
    {
        return Time.time >= nextFireTime;
    }

    protected void UpdateFireTime()
    {
        nextFireTime = Time.time + weaponData.fireRate;
    }

    public abstract void Fire();
}