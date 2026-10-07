using UnityEngine;


public class StaticCamera : MonoBehaviour
{
    [SerializeField]private float Speed = 5f;
    Rigidbody rb;
    Vector3 input;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    
    void Update()
    {
        input = (new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"))).normalized;
        input = Camera.main.transform.TransformDirection(input);

        transform.rotation = Quaternion.LookRotation(new Vector3(0, input.y, 0));
    }

    private void FixedUpdate()
    {
        Vector3 movement = input.normalized * Speed;
        movement.y = rb.linearVelocity.y;
        rb.linearVelocity = movement;
    }
}
