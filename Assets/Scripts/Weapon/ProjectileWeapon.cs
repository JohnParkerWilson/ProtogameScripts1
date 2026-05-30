using UnityEngine;

public class ProjectileWeapon : Weapon
{
    public override void Fire()
    {
        if (!CanFire())
            return;

        UpdateFireTime();

        Vector3 direction =
            (AimPoint - shootOrigin.position).normalized;

        GameObject projectileObject = Instantiate(
            weaponData.projectilePrefab,
            shootOrigin.position,
            Quaternion.LookRotation(direction)
        );

        Projectile projectile =
            projectileObject.GetComponent<Projectile>();

        projectile.speed = weaponData.projectileSpeed;

        projectile.Initialize(
            direction,
            weaponData.damage
        );
    }
}