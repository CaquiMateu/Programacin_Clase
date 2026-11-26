using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClienteBaseController : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] List<Transform> PuntosDeVenta = new List<Transform>();
    [SerializeField] List<GameObject> CarroDeLaCompra = new List<GameObject>();
    [SerializeField] float velocidad;
    [SerializeField] float dinero;
    [SerializeField] float dineroGastado;
    DineroController DineroController;
    [SerializeField] PuntoDeVentaController puntoDeVentaController;

    [Range(0f, 1f)]
    public float probabilidadBase = 0.75f; // 75% de probabilidad si el precio es igual al de mercado
    public float sensibilidadPrecio = 0.015f; // Cuánto baja/sube la probabilidad por cada 1% de diferencia
    bool Comprando = false;
    bool Comprado = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(BusquedaDePuntosDeVenta());
        dineroGastado = 0f;
        rb = GetComponent<Rigidbody>();
        DineroController = GameObject.FindGameObjectWithTag("Player").GetComponent<DineroController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Comprado == true)
        {
           DineroController.Dinero += dineroGastado;
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (PuntosDeVenta.Count > 0 && Comprando == false)
        {
            rb.MovePosition(Vector3.MoveTowards(transform.position, PuntosDeVenta[0].position, velocidad * Time.deltaTime));

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PuntoDeVenta"))
        {
            Comprando = true;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            puntoDeVentaController = other.GetComponent<PuntoDeVentaController>();
            DecidirCompra(other.GetComponent<PuntoDeVentaController>().precioMercado, other.GetComponent<PuntoDeVentaController>().precioJugador);
        }
    }

    public bool DecidirCompra(float precioMercado, float precioJugador)
    {
       
        if (dinero < precioJugador)
        {
            Debug.Log("El cliente no tiene suficiente dinero.");
            StartCoroutine(Espera());
            return false;
        }
        if (precioJugador <= 0f)
        {
            Debug.Log("El cliente no puede comprar un producto con precio 0 o negativo.");
            StartCoroutine(Espera());
            return false;

        }


        if (precioMercado <= 0f) return true;

        
        float diferenciaPorcentual = ((precioJugador - precioMercado) / precioMercado) * 100f;

        
        float probabilidadFinal = probabilidadBase - (diferenciaPorcentual * sensibilidadPrecio);

        
        probabilidadFinal = Mathf.Clamp01(probabilidadFinal);

       
        float tirada = Random.value;
        bool compraAprobada = tirada <= probabilidadFinal;

        if (compraAprobada)
        {
            dinero -= precioJugador;
            dineroGastado += precioJugador;
            Comprado = true;
            Debug.Log($"¡Compra realizada! (Probabilidad: {probabilidadFinal * 100f:F0}%)");
        }
        else
        {
            Debug.Log($"El cliente pensó que era muy caro y se fue. (Probabilidad: {probabilidadFinal * 100f:F0}%)");
            Destroy(gameObject);
        }

        return compraAprobada;
    }

    IEnumerator BusquedaDePuntosDeVenta()
    {
        yield return new WaitForSeconds(15f);
        GameObject[] puntos = GameObject.FindGameObjectsWithTag("PuntoDeVenta");
        foreach (GameObject punto in puntos)
        {
            PuntosDeVenta.Add(punto.transform);
        }
        
        StartCoroutine(BusquedaDePuntosDeVenta());
    }

    IEnumerator Espera()
    {
        yield return new WaitForSeconds(6f);
        DecidirCompra(puntoDeVentaController.GetComponent<PuntoDeVentaController>().precioMercado, puntoDeVentaController.GetComponent<PuntoDeVentaController>().precioJugador);
    }









}

