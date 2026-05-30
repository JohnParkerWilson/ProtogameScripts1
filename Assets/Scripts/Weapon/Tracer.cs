using UnityEngine;

public class Tracer : MonoBehaviour
{
    [SerializeField]
    private float lifeTime = 0.05f;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void Initialize(
        Vector3 startPoint,
        Vector3 endPoint)
    {
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);

        Destroy(gameObject, lifeTime);
    }
}