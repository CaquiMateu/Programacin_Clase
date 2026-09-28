using System;
using UnityEngine;

public class EstanteController : MonoBehaviour
{
    [Header("Arrays")]
    public Transform[] EspaciosParaProductos;
    public GameObject[] ProductosEnEstante;

    [Header("Variables Numéricas")]
    public int posicionActual = 0;

    [Header("Bools")]
    public bool lleno = false;
    public bool vacio = true;
    

    [Header("Variables de Texto")]

    public string nombreDelProductoEnEstante;

    void Start()
    {
        // Establecer el tamaño del array de productos en estante según la cantidad de espacios disponibles
        posicionActual = 0;
        //Indicar que el estante está vacío al inicio
        vacio = true;
    }

    // Update is called once per frame
    void Update()
    {
        #region ¿Está vacío el estante?

        // Verificar si el estante está vacío
        if (posicionActual == 0)
        {
            vacio = true;
        }
        else
        {
            vacio = false;
            
        }

        #endregion

        #region ¿Está lleno el estante?

        // Verificar si el estante está lleno. Pra esto revisar si la posición actual es mayor o igual a la cantidad de espacios disponibles.
        // Si lo es, entonces el estante está lleno. Si no, entonces el estante no está lleno.
        if (posicionActual >= EspaciosParaProductos.Length)
        {
            gameObject.GetComponent<Collider>().isTrigger = false;
            lleno = true;
        }
        else
        {
            gameObject.GetComponent<Collider>().isTrigger = true;
        }

        #endregion
    }

    private void OnTriggerEnter(Collider other)
    {

        // Verificar si el objeto que entra en el trigger tiene un componente ProductoController
        if (other.gameObject.GetComponent<ProductoController>() != null)
        {
            


            // Instanciar el producto en el estante en la posición correspondiente
            GameObject productoInstaurado = other.gameObject.GetComponent<ProductoController>().ModoEstante;

            if (nombreDelProductoEnEstante == "null")
            {
                nombreDelProductoEnEstante = other.gameObject.GetComponent<ProductoController>().NombreProducto;
            }

            #region Instanciar el producto en el estante
            // Verificar si hay espacio en el estante antes de instanciar el producto
            if (posicionActual >= EspaciosParaProductos.Length)
            {
                // Si no hay espacio, mostrar un mensaje en la consola y no lee más código
                Debug.Log("No hay más espacio en el estante");
                return;
            }
            else
            {
                // Instanciar el producto en la posición correspondiente del estante
                Instantiate(productoInstaurado, EspaciosParaProductos[posicionActual].position,Quaternion.Euler(0,90,0));
                // Aumentar el índice de posición actual para el siguiente producto
                SumarÍndice();
                // Guardar el producto instanciado en el array de productos en estante
                Array.Resize(ref ProductosEnEstante, posicionActual);
                ProductosEnEstante[posicionActual - 1] = productoInstaurado;
                // Destruir el objeto original que se colocó en el estante
                Destroy(other.gameObject);

               
            }
            #endregion
        }
    }

    void SumarÍndice()
    {
        posicionActual++;
        
    }

    void RestarÍndice()
    {
        posicionActual--;
    }
   
}
