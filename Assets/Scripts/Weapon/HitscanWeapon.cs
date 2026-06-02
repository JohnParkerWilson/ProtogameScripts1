using UnityEngine;

// Hitscan Weapon is of the Weapon Class
public class HitscanWeapon : Weapon
{
    public LayerMask hitMask;
    [SerializeField]
    private Tracer tracerPrefab;

    public override void Fire()
    {
        if (!CanFire())
            return;

        UpdateFireTime();

        Vector3 direction = (AimPoint - shootOrigin.position).normalized;
        Vector3 tracerEndPoint;

        if (Physics.Raycast(
            shootOrigin.position,
            direction,
            out RaycastHit hit,
            weaponData.range,
            hitMask))
        {
            tracerEndPoint = hit.point;
            Debug.Log("Hit: " + hit.collider.name);

            Health health =
                hit.collider.GetComponentInParent<Health>();

            if (health != null)
            {
                health.TakeDamage(weaponData.damage);
            }
        }
        else
        {
            tracerEndPoint =
                shootOrigin.position +
                direction * weaponData.range;
        }

        Tracer tracer = Instantiate(
            tracerPrefab
        );

        tracer.Initialize(
            shootOrigin.position,
            tracerEndPoint
        );

        Debug.DrawRay(
            shootOrigin.position,
            direction * weaponData.range,
            Color.red,
            1f
        );
    }
}