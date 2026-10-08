using UnityEngine;


public class StaticCamera : MonoBehaviour
{
    [SerializeField]private float Speed = 5f;
    [SerializeField] private float rotationSpeed = 5f;
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
        if (input.magnitude > 0.0001f)
        {
            Vector3 lookRotation = Quaternion.LookRotation(input).eulerAngles;
            lookRotation.x = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(lookRotation), Time.deltaTime * rotationSpeed);
        }
       
    }

    private void FixedUpdate()
    {
        Vector3 movement = input.normalized * Speed;
        movement.y = rb.linearVelocity.y;
        rb.linearVelocity = movement;
    }
}
