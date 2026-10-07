using UnityEngine;

public class BotónController : MonoBehaviour
{
    public CarritoController carritoController;
    public GameObject Producto;
    public string NombreProducto;
    public Transform PuntoSpawn;
    public float Precio;
    bool CarritoAbierto = false;
    public GameObject TiendaProductos;
    public GameObject Carrito;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        carritoController = GameObject.Find("GameManager").GetComponent<CarritoController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AñadirProductoAlCarrito()
    {
        carritoController.ProductosEnCarrito.Add(NombreProducto);
        carritoController.PreciosEnCarrito.Add(Precio);
       
    }
    public void AbrirCarrito()
    {
        if (CarritoAbierto == false)
        {
            CarritoAbierto = true;
            TiendaProductos.SetActive(false);
            Carrito.SetActive(true);
        }
        else
        {
            CarritoAbierto = false;
            TiendaProductos.SetActive(true);
            Carrito.SetActive(false);
        }
    }

}
