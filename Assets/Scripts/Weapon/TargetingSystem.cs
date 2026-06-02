using UnityEngine;

public class TargetingSystem : MonoBehaviour
{
    // Targeting System to be used by missile launchers/ homing weapons
    public Transform CurrentTarget { get; private set; }

    private void Update()
    {
        UpdateTarget();
    }

    private void UpdateTarget()
    {
        CurrentTarget = null;

        Ray ray =
            Camera.main.ViewportPointToRay(
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
                CurrentTarget = target.transform;
            }
        }
    }
}