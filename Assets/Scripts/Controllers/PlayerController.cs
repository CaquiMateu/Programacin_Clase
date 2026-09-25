using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float Speed = 4;
    public float MaxSpeed = 4;

    public float rotationSpeed = 10;

    //Este es el objeto que rota de arriba a abajo
    public Transform pivot;

    private float xrotationpivot;
    public Rigidbody rb;
    public PickUpScript pickUpScript;

    Vector3 input;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pickUpScript = GameObject.Find("Main Camera").GetComponent<PickUpScript>();
        
    }

    // Update is called once per frame
    void Update()
    {

        //Guardar el valor del movimiento en los ejes X & Z
        input = (new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"))).normalized;
        //Es para que se mueva en base al jugador, no en base al mundo. Es decir, para que se mueva donde mira
        input = transform.TransformDirection(input);

       

        if (pickUpScript.Rotando == false)
        {
            //Rotación Horizontal
            transform.Rotate(new Vector3(0, Input.GetAxisRaw("Mouse X") * rotationSpeed, 0));

            

            //Rotación Vertical
            xrotationpivot -= Input.GetAxisRaw("Mouse Y") * rotationSpeed;
            xrotationpivot = Mathf.Clamp(xrotationpivot, -90, 90);
            pivot.localEulerAngles = new Vector3(xrotationpivot, 0, 0);
        }
       

    }

    private void FixedUpdate()
    {
        //Ahora se mueve mediado de la física, para que se pueda hacer colisiones y demás. Se le aplica una fuerza al rigidbody del jugador
        //la cual se límita a una velocidad máxima. Se multiplica por Time.deltaTime para que la fuerza sea consistente sin importar los FPS
        if (rb.linearVelocity.magnitude < MaxSpeed)
        {
            rb.AddForce(new Vector3(input.x * Speed * Time.deltaTime, 0, input.z * Speed * Time.deltaTime), ForceMode.Impulse);
        }

        //Si la velocidad es muy baja y no hay input, se detiene el jugador para que no se deslice
        if (rb.linearVelocity.magnitude <= 2.5 && input == Vector3.zero)
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }
    }
}
