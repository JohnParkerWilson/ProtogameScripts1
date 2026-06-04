using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Weapon Data")]
    public WeaponData weaponData;

    [Header("References")]
    public Transform shootOrigin;

    public Vector3 AimPoint { get; set; }

    protected float nextFireTime;
    public bool IsFireHeld { get; set; }

    public virtual bool CanFire()
    {
        return Time.time >= nextFireTime;
    }

    protected void UpdateFireTime()
    {
        nextFireTime = Time.time + weaponData.fireRate;
    }

    //public virtual void StartCharging() { }

    public virtual void Charging() { }

    public virtual void ReleaseCharge() { }

    public abstract void Fire();
}