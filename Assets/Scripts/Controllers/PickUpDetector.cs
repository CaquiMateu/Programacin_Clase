using UnityEngine;

public class PickUpDetector : MonoBehaviour
{
    [SerializeField] private float detectionRange = 1;
    [SerializeField] private float SphereRadius = 1;
    [SerializeField] private LayerMask pickupLayer;
    Ray ray;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Detect()
    {
        ray = Camera.main.ViewportPointToRay(new Vector3 (0.5f, 0.5f, Camera.main.nearClipPlane));

        if (Physics.SphereCast(ray, SphereRadius,out RaycastHit hit, detectionRange, pickupLayer))
        {
            Debug.Log($"Estoy viendo al objeto {hit.collider}");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.hotPink;
        Gizmos.DrawWireSphere(ray.origin, SphereRadius);
        Gizmos.DrawRay(ray);
    }
}
