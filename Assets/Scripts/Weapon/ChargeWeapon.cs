using UnityEngine;

public class ChargeWeapon : Weapon
{
    [Header("Charge")]
    public float maxChargeTime = 2f;

    [Header("Damage")]
    public float minDamage = 20f;
    public float maxDamage = 100f;

    private float chargeTime;
    private bool charging;

    //public override void StartCharging()
    //{
    //    chargeTime = 0f;
    //    charging = true;
    //}

    private void Start()
    {
        chargeTime = 0f;
    }

    public override void Charging()
    {
        chargeTime += Time.deltaTime;

        chargeTime = Mathf.Min(
            chargeTime,
            maxChargeTime
        );
    }

    public override void ReleaseCharge()
    {


        float chargePercent =
            chargeTime / maxChargeTime;

        FireChargedShot(chargePercent);
        chargeTime = 0;
    }

    private void FireChargedShot(float chargePercent)
    {
        float ratio = Mathf.Lerp(
            minDamage,
            maxDamage,
            chargePercent
        );
        float curDamage = weaponData.damage * ratio/100;
        Debug.Log(
            $"Charged Shot: {curDamage}"
        );

        // Spawn projectile here
        Vector3 direction =
            (AimPoint - shootOrigin.position).normalized;


        // Shoots a projectile 
        GameObject projectileObject = Instantiate(
            weaponData.projectilePrefab,
            shootOrigin.position,
            Quaternion.LookRotation(direction)
        );

        Projectile projectile =
            projectileObject.GetComponent<Projectile>();

        projectile.speed = weaponData.projectileSpeed;
        projectile.AOE = weaponData.radius;

        projectile.Initialize(
            direction,
            weaponData.damage
        );
    }

    public override void Fire()
    {
        // Not used
    }
}