using UnityEngine;

public class ProjectileWeapon : Weapon
{
    private void Update()
    {
        Debug.DrawLine(
            shootOrigin.position,
            AimPoint,
            Color.green
        );

        Debug.DrawLine(
            Camera.main.transform.position,
            AimPoint,
            Color.red
        );
    }
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