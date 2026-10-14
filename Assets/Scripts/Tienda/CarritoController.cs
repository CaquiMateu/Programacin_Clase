using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CarritoController : MonoBehaviour
{
    public ListaCarrito listaCarrito;
    public List<string> ProductosEnCarrito;
    public List<int> CantidadDeProductos;
    public List<float> PreciosEnCarrito;
    Text ProductoAAñadir;
    public bool CarritoBloqueado = false;
    [SerializeField] int NúmeroEnLaLista = 1;


    private void Update()
    {
        if (NúmeroEnLaLista >= 28)
        {
            CarritoBloqueado = true;
        }
        else
        {
            CarritoBloqueado = false;
        }
    }

    public void ProductoNuevoEnElCarrito(string nombre, float precio)
    {
        if (CarritoBloqueado == true)
        {
            return;
        }
        else
        {
            ProductoAAñadir = listaCarrito.gameObject.transform.Find("ProductLista N" + NúmeroEnLaLista).GetComponent<Text>();
        }
        
       if (ProductoAAñadir.text != "")
       {
            NúmeroEnLaLista++;
            ProductoNuevoEnElCarrito(nombre, precio);
            return;
       }
       else
       {
           
           
            ProductosEnCarrito.Add(nombre);
            PreciosEnCarrito.Add(precio);
            CantidadDeProductos.Add(1);
            ProductoAAñadir.text = "x" + 1 + " " + nombre + " - $" + precio.ToString();
        }
       
    }

    public void ProductoYaExistenteEnElCarrito(string nombre, float precio)
    {
        int NumBuscador = 1;
        string ProductoABuscar;
        
        if (CarritoBloqueado == true)
        {
            return;
        }
        else
        {
            ProductoABuscar = listaCarrito.gameObject.transform.Find("ProductLista N" + NumBuscador).GetComponent<Text>().text;
        }

        if (ProductoABuscar != nombre && NumBuscador < 28)
        {
            NumBuscador++;
            ProductoYaExistenteEnElCarrito(nombre, precio);
            return;
        }
        else
        {
            int Cantidad = listaCarrito.ListaCarroObjetos.Count(nombreProducto => nombreProducto.name == ProductoABuscar);
            ProductoABuscar = "x" + (1 + Cantidad) + " " + nombre + " - $" + (precio * (1 + Cantidad)).ToString();


        }
    }

    public void GuardarProductoGameObject(GameObject producto)
    {
        listaCarrito.ListaCarroObjetos.Add(producto);
    }
    

   
}
