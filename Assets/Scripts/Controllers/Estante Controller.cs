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
    public bool productoColocado = false;

    [Header("Variables de Texto")]

    public string nombreDelProductoEnEstante;

    void Start()
    {
        posicionActual = 0;
        vacio = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (posicionActual == 0)
        {
            vacio = true;
        }
        else
        {
            vacio = false;
        }

        if (posicionActual >= EspaciosParaProductos.Length)
        {
            gameObject.GetComponent<Collider>().isTrigger = false;
            lleno = true;
        }
        else
        {
            gameObject.GetComponent<Collider>().isTrigger = true;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.GetComponent<ProductoController>() != null)
        {
            if (vacio == true)
            {
                nombreDelProductoEnEstante = other.gameObject.GetComponent<ProductoController>().NombreProducto;
            }
            else 
            {
                if (nombreDelProductoEnEstante != other.gameObject.GetComponent<ProductoController>().NombreProducto)
                {
                    Debug.Log("No se puede colocar un producto diferente en el estante");
                    productoColocado = true;
                    return;
                }
                else
                {
                    productoColocado = false;
                }
                    
            }
            GameObject productoInstaurado = other.gameObject.GetComponent<ProductoController>().ModoEstante;

            if (posicionActual >= EspaciosParaProductos.Length)
            {
                Debug.Log("No hay más espacio en el estante");
                return;
            }
            else
            {
                
                Instantiate(productoInstaurado, EspaciosParaProductos[posicionActual].position,Quaternion.Euler(0,90,0));
                SumarÍndice();
                Array.Resize(ref ProductosEnEstante, posicionActual);
                ProductosEnEstante[posicionActual - 1] = productoInstaurado;

                Destroy(other.gameObject);
            }
            
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
