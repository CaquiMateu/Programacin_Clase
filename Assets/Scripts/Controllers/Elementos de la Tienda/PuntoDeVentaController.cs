using UnityEngine;

public class PuntoDeVentaController : MonoBehaviour
{
    EstanteController estanteController;
    [SerializeField] public float precioMercado;
    [SerializeField] public float precioJugador;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        estanteController = transform.parent.GetComponent<EstanteController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (estanteController != null) 
        {
            precioJugador = estanteController.PrecioDelProducto;
            precioMercado = estanteController.PrecioDelProductoBase;

        }
        
    }
}
