using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public bool IsGrounded = false;

    [SerializeField]private Vector3 BoxSize = new Vector3(0.25f, 0.1f, 0.25f);
    [SerializeField] private Vector3 offset = new Vector3(0, 0.5f, 0);
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private LayerMask groundLayers;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       IsGrounded = Physics.CheckBox(transform.position + offset, BoxSize * 0.5f, Quaternion.identity, groundLayers);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.peru;
        Gizmos.DrawWireCube(transform.position + offset, BoxSize );
    }
}
