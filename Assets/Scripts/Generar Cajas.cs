using System.Collections;
using UnityEngine;

public class GenerarCajas : MonoBehaviour
{
    public EstanteController estantecontroller;
    public CajaController cajacontroller;
    public GameObject cajaGenerada;
    public Transform playerPos;
    
    GameObject cajaRecogida;


    void Start()
    {
        estantecontroller = GetComponent<EstanteController>();
        cajaGenerada = null;
    }

    
    public void GenerarCaja()
    {
        if(estantecontroller.cajaController != null)
        {
            cajaGenerada = Instantiate(estantecontroller.cajaController.gameObject, transform.position + new Vector3(200000, 200000, 200000), Quaternion.identity);
            cajacontroller = cajaGenerada.GetComponent<CajaController>();
            
           
        }
    }

    public void GenerarCajaDeRecogida()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;

        cajaRecogida = Instantiate(cajaGenerada, new Vector3 ( (playerPos.position.x +(transform.position.x - playerPos.position.x)) / 2 , 1.38f, (playerPos.position.z +(transform.position.z - playerPos.position.z))/2), Quaternion.identity);
        cajaRecogida.GetComponent<CajaController>().SePuedeColocar = true;
        cajaRecogida.GetComponent<CajaController>().Agarrado = false;
        cajaRecogida.GetComponent<Rigidbody>().isKinematic = false;
        cajaRecogida.layer = 0;
        StartCoroutine(EsperaMínima());
        
    }

    IEnumerator EsperaMínima()
    {
        yield return new WaitForSeconds(0.1f);
        cajaRecogida.GetComponent<CajaController>().Cantidad_Productos_ACTUAL = 0;

    }
}
