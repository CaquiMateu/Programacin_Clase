using UnityEngine;

public class FPSShoot : MonoBehaviour
{
    [SerializeField] private float fireRate = 10;
    [SerializeField] private float WeaponRange = 100;

    private float timeToShoot = 0;

    private Camera cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= timeToShoot)
        {
            timeToShoot = Time.time + 1/ fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, cam.nearClipPlane));

        Debug.DrawRay(ray.origin, ray.direction * WeaponRange, Color.firebrick, 1);

        if (Physics.Raycast(ray, out RaycastHit hit, WeaponRange))
        {
            Debug.Log($"He Disparado a: {hit.collider}");
        }
    }
}
