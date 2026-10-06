using UnityEngine;

public class PlayerControllerRB : MonoBehaviour
{
    public float Speed = 4;

    public float rotationSpeed = 10;

    //Este es el objeto que rota de arriba a abajo
    public Transform pivot;

    public float jumpforce = 10;

    private float xrotationpivot;
    Vector3 input;

    GroundChecker groundChecker;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
      groundChecker = GetComponent<GroundChecker>();
    }

    // Update is called once per frame
    void Update()
    {
        //Guardar el valor del movimiento en los ejes X & Z
        input = (new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"))).normalized;
        //Es para que se mueva en base al jugador, no en base al mundo. Es decir, para que se mueva donde mira
        input = transform.TransformDirection(input);



        if (Input.GetButtonDown("Jump") && groundChecker == true)
        {
            rb.AddForce(Vector3.up * jumpforce, ForceMode.VelocityChange);
        }

       

        //Rotación Horizontal
        transform.Rotate(new Vector3(0, Input.GetAxisRaw("Mouse X") * rotationSpeed, 0));

        //Rotación Vertical
        xrotationpivot -= Input.GetAxisRaw("Mouse Y") * rotationSpeed;
        xrotationpivot = Mathf.Clamp(xrotationpivot, -90, 90);
        pivot.localEulerAngles = new Vector3(xrotationpivot, 0, 0);

        

    }

    private void FixedUpdate()
    {
        Vector3 movement = input.normalized * Speed;
        movement.y = rb.linearVelocity.y;
        rb.linearVelocity = movement;
    }

}
