using System.Collections;
using UnityEngine;

public class CajaController : MonoBehaviour
{
    [Header("Variables Numéricas")]
    public int Cantidad_Productos_MAX = 0;
    public int Cantidad_Productos_ACTUAL = 0;

    [Header("Variables de Posición")]
    public Transform EstantePos;

    [Header("Bools")]
    public bool Interactuarble = false; 
    public bool Agarrado = false;
    public bool DiferenteProducto = false;
    public bool SePuedeColocar = true;

    [Header("Variables de Texto")]
    public string NombreCaja;
    public string NombreProducto;

    [Header("GameObjects")]
    public GameObject Producto;

    private void Start()
    {
        // Establecer el nombre del objeto en la jerarquía de Unity
        gameObject.name = NombreCaja;
        // Inicializar la cantidad actual de productos con la cantidad máxima
        Cantidad_Productos_ACTUAL = Cantidad_Productos_MAX;

    }
    void Update()
    {

        
        // Variable para evitar que se instancien múltiples productos al mismo tiempo
        bool Antibucle = false;

        #region Instanciación de productos en el estante

        // Verificar si hay un objeto agarrado
        if (Agarrado == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                // Verificar si el estante es interactuable y si no hay otro producto distinto ya puesto
                if (Interactuarble == true && DiferenteProducto == false)
                {
                    // Verificar si hay productos disponibles para instanciar,
                    // si no se ha instanciado un producto en este frame y si se presiona el botón izquierdo del mouse
                    if (Cantidad_Productos_ACTUAL > 0 && Antibucle == false)
                    {
                        if (SePuedeColocar == true)
                        {
                            SePuedeColocar = false;
                            // Instanciar el producto en la posición del estante con rotación por defecto
                            Instantiate(Producto, EstantePos.position, Quaternion.identity);
                            // Disminuir la cantidad actual de productos disponibles
                            Cantidad_Productos_ACTUAL--;
                            // Indicar que ya se ha instanciado un producto en este frame
                            Antibucle = true;
                            StartCoroutine(Espera());
                        }


                    }
                }
            }
            
            else
            {
                // Si el estante no es interactuable o si ya hay otro producto distinto puesto, se desactiva la referencia al estante
                EstantePos = null;
            }
        }

        #endregion
    }

    IEnumerator Espera()
    {
        yield return new WaitForSeconds(0.001f);
        SePuedeColocar = true;
    }
}



            
        
    

