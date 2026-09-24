using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 4;

    public float rotationSpeed = 10;

    //Este es el objeto que rota de arriba a abajo
    public Transform pivot;

    public float jumpforce = 10; 

    private float xrotationpivot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private CharacterController characterController;

    private Vector3 gravity;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Guardar el valor del movimiento en los ejes X & Z
        Vector3 input = (new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"))).normalized;
        //Es para que se mueva en base al jugador, no en base al mundo. Es decir, para que se mueva donde mira
        input = transform.TransformDirection(input);

       

        if (Input.GetButtonDown("Jump") && characterController.isGrounded == true)
        {
            input.y = Mathf.Sqrt(jumpforce * -2.0f * 9.8f);
        }

        gravity.y -= 9.8f * Time.deltaTime;

        //Rotación Horizontal
        transform.Rotate(new Vector3(0, Input.GetAxisRaw("Mouse X") * rotationSpeed,0));

        //Rotación Vertical
        xrotationpivot -= Input.GetAxisRaw("Mouse Y") * rotationSpeed;
        xrotationpivot = Mathf.Clamp(xrotationpivot, -90, 90);
        pivot.localEulerAngles = new Vector3(xrotationpivot, 0, 0);

        if (characterController.isGrounded == true)
        {
            gravity.y = 0;
        }

        characterController.Move(input * Speed * Time.deltaTime + gravity * Time.deltaTime);

    }
}
