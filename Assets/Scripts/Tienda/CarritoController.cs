using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CarritoController : MonoBehaviour
{
    public int ÍndiceDeBusqueda =0;
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
        ÍndiceDeBusqueda = 0;
        Text ProductoABuscar;
        

        if (CarritoBloqueado == true)
        {
            return;
        }
        else
        {
            ObjetosEnElCarrito.Add(Producto);
            ÍndiceDeBusqueda = ProductosEnCarrito.IndexOf(nombre);
            ProductoABuscar = Objectolista.transform.Find("ProductLista N" + (ÍndiceDeBusqueda +1)).GetComponent<Text>();
            Cantidad = ObjetosEnElCarrito.Count(nombreProducto => nombreProducto.name == nombre);
            ProductoABuscar.text = "x" + Cantidad + " " + nombre + " - $" + (precio *  Cantidad).ToString();
        }

        
            
               


            
        
       
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

    public void VaciarCarrito()
    {

    }
    

   
}
