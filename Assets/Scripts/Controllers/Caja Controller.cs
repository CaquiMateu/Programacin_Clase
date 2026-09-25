using UnityEngine;

public class CajaController : MonoBehaviour
{
    public string NombreCaja;
    public int Cantidad_Productos_MAX = 0;
    public int Cantidad_Productos_ACTUAL = 0;
    public bool Agarrado = false;
    public GameObject Producto;

    private void Start()
    {
        gameObject.name = NombreCaja;
        Cantidad_Productos_ACTUAL = Cantidad_Productos_MAX;

    }
    void Update()
    {
        bool Antibucle = false;

        if (Agarrado == true)
        {
            if (Cantidad_Productos_ACTUAL > 0 && Antibucle == false && Input.GetKeyDown(KeyCode.Mouse0)) 
            {
                Instantiate(Producto, transform.position + transform.forward * 1.5f, Quaternion.identity);
                Cantidad_Productos_ACTUAL--;
                Antibucle = true;
            }
        }
    }
}
