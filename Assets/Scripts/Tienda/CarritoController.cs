using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CarritoController : MonoBehaviour
{
    public int Cantidad;
    public GameObject Objectolista;
    public ListaCarrito listaCarrito;
    public List<string> ProductosEnCarrito;
    public List<int> CantidadDeProductos;
    public List<float> PreciosEnCarrito;
    Text ProductoAAñadir;
    public bool CarritoBloqueado = false;
    [SerializeField] int NúmeroEnLaLista = 1;
        public List<GameObject> ObjetosEnElCarrito;


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

    public void ProductoNuevoEnElCarrito(string nombre, float precio, GameObject Producto)
    {
        if (CarritoBloqueado == true)
        {
            return;
        }
        else
        {
            ProductoAAñadir = Objectolista.transform.Find("ProductLista N" + NúmeroEnLaLista).GetComponent<Text>();
        }
        
       if (ProductoAAñadir.text != "")
       {
            NúmeroEnLaLista++;
            ProductoNuevoEnElCarrito(nombre, precio, Producto);
            return;
       }
       else
       {

            ObjetosEnElCarrito.Add(Producto); 
            ProductosEnCarrito.Add(nombre);
            PreciosEnCarrito.Add(precio);
            CantidadDeProductos.Add(1);
            ProductoAAñadir.text = "x" + 1 + " " + nombre + " - $" + precio.ToString();
        }
       
    }

    public void ProductoYaExistenteEnElCarrito(string nombre, float precio, GameObject Producto)
    {
        int NumBuscador = 1;
        string ProductoABuscar;
        
        if (CarritoBloqueado == true)
        {
            return;
        }
        else
        {
            ObjetosEnElCarrito.Add(Producto);
            ProductoABuscar = Objectolista.transform.Find("ProductLista N" + NumBuscador).GetComponent<Text>().text;
            
        }

        //if (ProductoABuscar != nombre && NumBuscador < 28)
        //{
        //    NumBuscador++;
        //    ProductoYaExistenteEnElCarrito(nombre, precio);
        //    return;
        //}
        //else
        //{
            Cantidad = ObjetosEnElCarrito.Count(nombreProducto => nombreProducto.name == nombre);
            ProductoAAñadir.text = "x" +  Cantidad + " " + nombre + " - $" + (precio * (1 + Cantidad)).ToString();
       

        //}
    }

    public void GuardarProductoGameObject(GameObject producto)
    {
        if (listaCarrito.DiccionarioCarrito.ContainsKey(producto))
        {
            listaCarrito.DiccionarioCarrito[producto] = listaCarrito.DiccionarioCarrito[producto] + 1;
        }
        else
        {
            listaCarrito.DiccionarioCarrito.Add(producto, 1);
        }
        
    }
    

   
}
