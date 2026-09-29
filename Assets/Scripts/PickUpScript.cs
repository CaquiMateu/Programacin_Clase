using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class PickUpScript : MonoBehaviour
{
    #region Variables

    [Header("Ref. Otros Scripts")]
    public EstanteController estanteController;
    public CajaController cajaController;

    [Header("UI")]
    public TextMeshProUGUI NúmeroDeElementosEnCaja;
    public TextMeshProUGUI ObjName;
    public GameObject CajaPanel;
    public GameObject ObjNamePanel;
    public Image Puntero;
    Color OriginalPunteroColor;
    [Header("GameObjects")]
    public GameObject player;
    public GameObject ObjAgarrado;

    [Header("Layers")]
    public int layerorginal;
    private int LayerNumber;
    int LayerOriginalPlayer;

    [Header("Variables Con Objeto Agarrado")]
    public float FuerzaDeLanzamiento = 500f;
    public float RangoParaCogerObjeto = 5f;
    private float SensibilidadDeRotacion = 2f;
    public Transform holdPos;
    private Rigidbody ObjAgarradoRb;
    private bool canDrop = true;
    public bool Rotando = false;
    public float TiempoActualDeCarga = 1f;
    bool isCharging = false;
    RaycastHit hit;

    #endregion

    void Start()
    {
        // Guardar el color original del puntero para poder restaurarlo después
        OriginalPunteroColor = Puntero.color;
        // Guardar la capa original del jugador para poder restaurarla después
        LayerOriginalPlayer = LayerMask.NameToLayer("Player");
        // Guardar la capa de "Hold_Layer" para poder asignarla al objeto agarrado
        LayerNumber = LayerMask.NameToLayer("Hold_Layer");
        // Inicialmente, ocultar los paneles de UI
        ObjNamePanel.SetActive(false);
        CajaPanel.SetActive(false);
    }
    void Update()
    {

        #region Actualizar el nombre del objeto agarrado

        // Actualizar el panel de UI con el nombre del objeto agarrado si existe, de lo contrario ocultarlo
        if (ObjAgarrado != null)
        {
            ObjNamePanel.SetActive(true);
           
        }
        else
        {
            ObjNamePanel.SetActive(false);
        }

        #endregion

        #region Actualizar el número de objetos en la caja

        // Actualizar el texto que muestra la cantidad de elementos en la caja si el controlador de la caja existe
        if (cajaController != null)
            {
                NúmeroDeElementosEnCaja.text = cajaController.Cantidad_Productos_ACTUAL.ToString() + "/" + cajaController.Cantidad_Productos_MAX.ToString();


            }

            #endregion


            #region Agarrar y soltar objetos con la tecla E

            // Detectar si se presiona la tecla E para agarrar o soltar un objeto
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Si no hay un objeto agarrado, intentar agarrar uno
                if (ObjAgarrado == null)
                {

                    // Realizar un raycast desde la posición del jugador hacia adelante para detectar objetos que se puedan agarrar
                    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, RangoParaCogerObjeto))
                    {
                        // Si el objeto detectado tiene la etiqueta "CanPickUp", agarrarlo
                        if (hit.transform.gameObject.tag == "CanPickUp")
                        {
                            // Obtener el componente CajaController del objeto agarrado si existe
                            cajaController = hit.transform.gameObject.GetComponent<CajaController>();
                            // Llamar a la función PickUpObject para agarrar el objeto
                            PickUpObject(hit.transform.gameObject);
                            // Mostrar el panel de UI con el nombre del objeto agarrado
                            ObjNamePanel.SetActive(true);
                        }


                    }
                }
                // Si ya hay un objeto agarrado, intentar soltarlo
                else
                {
                    if (canDrop == true)
                    {
                        StopClipping();
                        DropObject();
                    }
                }
            }
            #endregion

            #region Cambiar el color del puntero 

            // Cambiar el color del puntero según el objeto que se esté mirando
            if (ObjAgarrado == null)
            {
                // Realizar un raycast desde la posición del jugador hacia adelante para detectar objetos que se puedan agarrar
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, RangoParaCogerObjeto))
                {
                    // Si el objeto detectado tiene la etiqueta "CanPickUp", cambiar el color del puntero a verde
                    if (hit.transform.gameObject.tag == "CanPickUp")
                    {
                        Puntero.color = Color.green;

                    }
                    else
                    {
                        Puntero.color = OriginalPunteroColor;
                    }

                    if (hit.transform.gameObject.tag == "Estante")
                    {
                        estanteController = hit.transform.gameObject.GetComponent<EstanteController>();
                        
                       if (estanteController.vacio == false)
                       {
                          Puntero.color = Color.yellow;

                        if (Input.GetKeyDown(KeyCode.Q))
                        {
                            estanteController.generarcajas.GenerarCajaDeRecogida();
                        }
                       }
                       
                    }

            }
                else
                {
                    Puntero.color = OriginalPunteroColor;
                }

            }
            else
            {
                // Si hay un objeto agarrado, verificar si es una caja y cambiar el color del puntero según el estado de la caja y el estante
                if (cajaController != null)
                {
                    // Si la caja no está agarrada, restaurar el color original del puntero
                    if (cajaController.Agarrado == false)
                    {
                        Puntero.color = OriginalPunteroColor;
                    }
                    // Si la caja está agarrada, verificar si hay un estante delante del jugador
                    else
                    {
                        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, RangoParaCogerObjeto))
                        {
                            // Si el objeto detectado tiene la etiqueta "Estante", cambiar el color del puntero a naranja y actualizar las referencias del estante y la caja
                            if (hit.transform.gameObject.tag == "Estante")
                            {
                                GenerarCajas generarCajas;
                                // Cambiar el color del puntero a naranja para indicar que se puede colocar la caja en el estante
                                Puntero.color = Color.orange;
                                // Obtener el componente EstanteController del estante detectado
                                estanteController = hit.transform.gameObject.GetComponent<EstanteController>();
                                generarCajas = hit.transform.gameObject.GetComponent<GenerarCajas>();
                               //Detecta si el estante está vacío
                               if (estanteController.vacio == true)
                               {
                                //Ahora analiza si tiene un cajacontroller, si lo tiene le cambia el scirpt a una caja que se generará a lo lejos
                                //Esto es para evitar posibles problemas de null reference
                                if (estanteController.cajaController == null)
                                {
                                    estanteController.cajaController = cajaController;
                                    if (generarCajas.cajaGenerada == false)
                                    {

                                        generarCajas.GenerarCaja();

                                    }
                                }
                                //Si detecta que el nombre de la caja es distinto al de la caja que ha generado antes, la borra y se queda con el nuevo.
                                else if (cajaController.NombreCaja != estanteController.cajaController.NombreCaja)
                                {
                                    generarCajas.cajacontroller = null;
                                    Destroy(generarCajas.cajaGenerada);
                                    estanteController.cajaController = cajaController;

                                    if (generarCajas.cajaGenerada == false)
                                    {

                                        generarCajas.GenerarCaja();

                                    }
                                }
                                
                                 
                               }
                               else
                               {
                                  if (Input.GetKeyDown(KeyCode.Q))
                                  {

                                    if (estanteController.vacio == false && cajaController.Cantidad_Productos_MAX > cajaController.Cantidad_Productos_ACTUAL)
                                    {
                                        cajaController.Cantidad_Productos_ACTUAL++;
                                        estanteController.RetirarProductoDelEstante();
                                    }

                                    

                                  }
                                 
                                   


                                 


                               }

                               // Actualizar la posición del estante en el controlador de la caja
                               cajaController.EstantePos = hit.transform;

                                // Verificar si el estante existe y si no está lleno para permitir la interacción con la caja
                                if (estanteController != null)
                                {

                                    if (estanteController.lleno == false)
                                    {
                                        cajaController.Interactuarble = true;
                                    }
                                    else
                                    {
                                        cajaController.Interactuarble = false;
                                    }
                                }
                                else
                                {
                                    estanteController = null;
                                }


                            }
                            else
                            {
                                cajaController.Interactuarble = false;
                            }
                        }
                        else
                        {
                            Puntero.color = OriginalPunteroColor;
                            estanteController = null;
                        }

                    }
                }

            }
            #endregion

            #region Acciones con el objeto agarrado

            // Si hay un objeto agarrado, permitir moverlo, rotarlo y lanzarlo
            if (ObjAgarrado != null)
            {
                MoveObject();
                RotateObject();
                // Si se presiona el botón derecho del mouse y se puede soltar el objeto, detener el clipping y comenzar a cargar la fuerza de lanzamiento
                if (Input.GetKeyDown(KeyCode.Mouse1) && canDrop == true)
                {
                    StopClipping();
                    StartTimer();

                }
                // Si se suelta el botón derecho del mouse y se puede soltar el objeto, lanzar el objeto
                if (Input.GetKeyUp(KeyCode.Mouse1) && canDrop == true)
                {
                    ThrowObject();
                }
                // Si se está cargando la fuerza de lanzamiento, aumentar el tiempo de carga hasta un máximo de 3 segundos
                if (isCharging == true)
                {
                    if (TiempoActualDeCarga < 3f)
                    {
                        TiempoActualDeCarga += 1 * Time.deltaTime;
                    }
                }

            }
        #endregion

        #region Actualizar la referencia del producto colocado en el estante


        // Actualizar la referencia del producto colocado en el estante si ambos controladores existen
        if (cajaController != null && estanteController != null)
        {
            if (estanteController.vacio == false)
            {
                if (cajaController.NombreProducto == estanteController.nombreDelProductoEnEstante)
                {
                    cajaController.DiferenteProducto = false;

                }
                else
                {
                    cajaController.DiferenteProducto = true;
                }
            }


        #endregion
        }
        void PickUpObject(GameObject pickUpObj)
        {
            if (pickUpObj.GetComponent<Rigidbody>())
            {
                if (cajaController != null)
                {
                    ActivarCaja();
                }
                layerorginal = hit.collider.gameObject.layer;
                ObjName.text = hit.transform.gameObject.name;
                ObjAgarrado = pickUpObj;
                ObjAgarradoRb = pickUpObj.GetComponent<Rigidbody>();
                ObjAgarradoRb.isKinematic = true;
                ObjAgarradoRb.transform.parent = holdPos.transform;
                ObjAgarrado.layer = LayerNumber;
                Puntero.color = OriginalPunteroColor;

                Physics.IgnoreCollision(ObjAgarrado.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
            }
        }
        void DropObject()
        {
            if (cajaController != null)
            {
                DesactivarCaja();
            }
            ObjNamePanel.SetActive(false);
            Physics.IgnoreCollision(ObjAgarrado.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
            ObjAgarrado.layer = layerorginal;
            ObjAgarradoRb.isKinematic = false;
            ObjAgarrado.transform.parent = null;
            ObjAgarrado = null;
            player.layer = 6;
            StartCoroutine(DevolverALaCapaOriginal());
        }
        void MoveObject()
        {

            ObjAgarrado.transform.position = holdPos.transform.position;
        }
        void RotateObject()
        {
            if (Input.GetKey(KeyCode.R))
            {
                canDrop = false;
                Rotando = true;


                float XaxisRotation = Input.GetAxis("Mouse X") * SensibilidadDeRotacion;
                float YaxisRotation = Input.GetAxis("Mouse Y") * SensibilidadDeRotacion;

                ObjAgarrado.transform.Rotate(Vector3.down, XaxisRotation);
                ObjAgarrado.transform.Rotate(Vector3.right, YaxisRotation);
            }
            else
            {

                canDrop = true;
                Rotando = false;
            }
        }
        void ThrowObject()
        {
            if (cajaController != null)
            {
                DesactivarCaja();
            }
            ObjNamePanel.SetActive(false);
            Physics.IgnoreCollision(ObjAgarrado.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
            ObjAgarrado.layer = layerorginal;
            ObjAgarradoRb.isKinematic = false;
            ObjAgarrado.transform.parent = null;
            ObjAgarradoRb.AddForce(transform.forward * FuerzaDeLanzamiento * TiempoActualDeCarga);
            ObjAgarrado = null;
            isCharging = false;
            TiempoActualDeCarga = 1f;
            player.layer = 6;
            StartCoroutine(DevolverALaCapaOriginal());
        }
        void StopClipping()
        {
            var clipRange = Vector3.Distance(ObjAgarrado.transform.position, transform.position);

            RaycastHit[] hits;
            hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), clipRange);

            if (hits.Length > 1)
            {

                ObjAgarrado.transform.position = transform.position + new Vector3(0f, -0.5f, 0f);
            }
        }

        void StartTimer()
        {
            isCharging = true;
        }

        IEnumerator DevolverALaCapaOriginal()
        {
            yield return new WaitForSeconds(0.5f);
            player.layer = LayerOriginalPlayer;
        }

        void ActivarCaja()
        {
            if (cajaController != null)
            {
                cajaController.Agarrado = true;
                CajaPanel.SetActive(true);
            }
        }

        void DesactivarCaja()
        {
            if (cajaController != null)
            {
                cajaController.Agarrado = false;
                cajaController = null;
                CajaPanel.SetActive(false);
            }
        }
    }
}

