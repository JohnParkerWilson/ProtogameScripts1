using UnityEngine;

public class MissileWeapon : Weapon
{
    public HomingMissile missilePrefab;

    public override void Fire()
    {
        if (!CanFire())
            return;

        UpdateFireTime();

        Transform target =
            FindTarget();

        HomingMissile missile =
            Instantiate(
                missilePrefab,
                shootOrigin.position,
                shootOrigin.rotation
            );

        missile.AOE = weaponData.radius;

        missile.Initialize(
            target,
            weaponData.damage
        );
    }

    private Transform FindTarget()
    {
        Ray ray = Camera.main.ViewportPointToRay(
            new Vector3(0.5f, 0.5f)
        );

        if (Physics.Raycast(
            ray,
            out RaycastHit hit,
            1000f))
        {
            Targetable target =
                hit.collider.GetComponent<Targetable>();

            if (target != null)
            {
                return target.transform;
            }
        }

        return null;
    }
}