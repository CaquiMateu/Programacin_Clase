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
    public bool OtroProductoPuesto = false;

    [Header("Variables de Texto")]
    public string NombreCaja;

    [Header("GameObjects")]
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
            if (Interactuarble == true && OtroProductoPuesto == false)
            {
                
                if (Cantidad_Productos_ACTUAL > 0 && Antibucle == false && Input.GetKeyDown(KeyCode.Mouse0)) 
                {
                    Instantiate(Producto, EstantePos.position, Quaternion.identity);
                    Cantidad_Productos_ACTUAL--;
                    Antibucle = true;
                }
            }
            else
            {
                EstantePos = null;
            }
        }
    }
}

            
        
    

