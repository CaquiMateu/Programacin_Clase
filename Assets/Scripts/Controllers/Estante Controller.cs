using System;
using UnityEngine;

public class EstanteController : MonoBehaviour
{

    Transform GrupoADestruir;

    [Header("Referencias A Otros Componentes")]
    public CajaController cajaController;
    public GenerarCajas generarcajas;
    public VacíoController vacíoController;

    [Header("GameObjects")]
    public GameObject Vacío;
    GameObject ProductoHijoDeVacio;

   [Header("Arrays")]
    public Transform[] EspaciosParaProductos;
    public GameObject[] ProductosEnEstante;

    [Header("Variables Numéricas")]
    public int posicionActual = 0;
    Vector3 rotación;

    [Header("Bools")]
    public bool lleno = false;
    public bool vacio = true;
    

    [Header("Variables de Texto")]

    public string nombreDelProductoEnEstante;

    void Start()
    {
        rotación = transform.rotation.eulerAngles;
        Vacío = vacíoController.gameObject;
        generarcajas = GetComponent<GenerarCajas>();
        // Establecer el tamaño del array de productos en estante según la cantidad de espacios disponibles
        posicionActual = 0;
        //Indicar que el estante está vacío al inicio
        vacio = true;
    }

    // Update is called once per frame
    void Update()
    {
        #region Igualar Caja Controller
        
        //Actualiza el cajacontroller si detecta que es diferente al que debería tener;
        if (cajaController != generarcajas.cajacontroller)
        {
            cajaController = generarcajas.cajacontroller;
        }
        #endregion

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

        if (vacio == true)
        {
            nombreDelProductoEnEstante = ("null");
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
            lleno = false;
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
                GameObject VacioInstanciado = Instantiate(Vacío, transform.position, Quaternion.identity);
                VacioInstanciado.name = vacíoController.nombre + posicionActual;
                VacioInstanciado.transform.SetParent(this.gameObject.transform);
                // Instanciar el producto en la posición correspondiente del estante
                ProductoHijoDeVacio = Instantiate(productoInstaurado, EspaciosParaProductos[posicionActual].position,Quaternion.Euler(rotación + new Vector3(0,90,0)));
                ProductoHijoDeVacio.transform.SetParent(VacioInstanciado.transform);
                // Aumentar el índice de posición actual para el siguiente producto
                SumarÍndice();
                // Guardar el producto instanciado en el array de productos en estante
                Array.Resize(ref ProductosEnEstante, posicionActual);
                ProductosEnEstante[posicionActual - 1] = ProductoHijoDeVacio;
                // Destruir el objeto original que se colocó en el estante
                Destroy(other.gameObject);

               
            }
            #endregion
        }
    }

    public void RetirarProductoDelEstante()
    {
        if (vacio == false)
        {
            
            int NúmeroAAplicar = posicionActual - 1;
            GrupoADestruir = this.gameObject.transform.Find("Vacío" + NúmeroAAplicar);
            Destroy(GrupoADestruir.gameObject);
            RestarÍndice();
            Array.Resize(ref ProductosEnEstante, posicionActual);
           
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
