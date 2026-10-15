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
        if (carritoController.ProductosEnCarrito.Contains(NombreProducto) == false)
        {
            if (carritoController.listaCarrito.DiccionarioCarrito.Count <= 28)
            {
                carritoController.ProductoNuevoEnElCarrito(NombreProducto, Precio, Producto);
                carritoController.GuardarProductoGameObject(Producto);
            }
            else
            {
                Debug.Log("Carrito lleno");
            }
        }
        else
        {
            
            if (carritoController.ProductosEnCarrito.Count <= 28)
            {
                
                carritoController.GuardarProductoGameObject(Producto);
                carritoController.ProductoYaExistenteEnElCarrito(NombreProducto, Precio, Producto);
               
            }
        }
       
        

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
