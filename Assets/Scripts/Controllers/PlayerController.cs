using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [Header("Variables de Movimiento")]
    public float SpeedToAply = 4;
    public float MaxSpeed = 4;
    public float rotationSpeed = 10;
    private float xrotationpivot;

    [Header("Transform")]
    public Transform pivot;

    [Header("Rigidbody")]
    public Rigidbody rb;

    [Header("Ref. Otros Scripts")]
    public PickUpScript pickUpScript;

    Vector3 input;

    
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
        SpeedToAply = MaxSpeed - (rb.linearVelocity.magnitude);

       

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
        if (rb.linearVelocity.magnitude <= MaxSpeed)
        {
            rb.AddForce(new Vector3(input.x * SpeedToAply * Time.deltaTime, 0, input.z * SpeedToAply * Time.deltaTime), ForceMode.Impulse);
        }

        
    }
}
