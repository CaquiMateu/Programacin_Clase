using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CarritoController : MonoBehaviour
{
    public ListaCarrito listaCarrito;
    public List<string> ProductosEnCarrito;
    public List<float> PreciosEnCarrito;
   
    int Índice = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Índice = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (listaCarrito.gameObject.activeInHierarchy == true)
        {
            if (listaCarrito.ListaCarro[Índice].gameObject.GetComponent<Text>().text == "")
            {
                string item = ProductosEnCarrito[Índice];

                ContarRepetidos(item);

                

                listaCarrito.ListaCarro[Índice].gameObject.GetComponent<Text>().text = item + " x" + listaCarrito.CantidadProductos[Índice].ToString() + " - $" + PreciosEnCarrito[Índice].ToString();
            }
        }
    }

    void ContarRepetidos(string nombre)
    {
        int cantidad = ProductosEnCarrito.Count(item => item == nombre);

        if (cantidad > 1)
        {
            listaCarrito.CantidadProductos[Índice] = cantidad;
        }
        else
        {
            listaCarrito.CantidadProductos[Índice] = 1;
        }
    }
}
