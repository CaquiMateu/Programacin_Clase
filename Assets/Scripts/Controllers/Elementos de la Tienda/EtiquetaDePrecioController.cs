using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EtiquetaDePrecio : MonoBehaviour
{
    [SerializeField] public GameObject ObjetoPadre;
    [SerializeField] EstanteController estanteController;
    [SerializeField] public Text precio;
    public GameObject panelPrecio;
    Text TextoInput;
    Text TextoProducto;
    Text DiferenciaVenta;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ObjetoPadre = transform.parent.gameObject;
        estanteController = ObjetoPadre.GetComponent<EstanteController>();
        TextoInput = panelPrecio.transform.Find("InputField").GetComponent<InputField>().textComponent;
        TextoProducto = panelPrecio.transform.Find("TextoProducto").GetComponent<Text>();
        DiferenciaVenta = panelPrecio.transform.Find("DiferenciaVenta").GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        

        if (estanteController.vacio == false && panelPrecio.activeInHierarchy == true)
        {
            float diferencial;
            float precioMercado = estanteController.PrecioDelProductoBase;
            float precioJugador = estanteController.PrecioDelProducto;

            if (TextoInput.text == "")
            {
                estanteController.PrecioDelProducto = 0;
                return;
            }
           

               estanteController.PrecioDelProducto = float.Parse(TextoInput.text);
               
            diferencial = ((precioJugador - precioMercado) / precioMercado) * 100;

            if (diferencial > 0)
            {
                DiferenciaVenta.text = "El precio de venta es " + diferencial.ToString("F2") + "% mayor al precio de mercado";
            }
            else if (diferencial < 0 && precioJugador != 0)
            {
                DiferenciaVenta.text = "El precio de venta es " + Math.Abs(diferencial).ToString("F2") + "% menor al precio de mercado";
            }
            else if (diferencial == 0)
            {
                DiferenciaVenta.text = "El precio de venta es igual al precio de mercado";
            }
            else if (precioJugador == 0 || TextoInput.text == "")
            {
                DiferenciaVenta.text = "El precio de venta no ha sido establecido";
            }


        }
        if (panelPrecio.activeInHierarchy == true && Input.GetKeyDown(KeyCode.Escape))
        {
            panelPrecio.SetActive(false);
        }
       

    }

    public void GenerarPanel()
    {
        if (estanteController.vacio == true)
        {
            return;
        }
        if (panelPrecio.activeInHierarchy == false)
        {
            
            TextoInput.text = estanteController.PrecioDelProducto.ToString();
            panelPrecio.SetActive(true);
            TextoProducto.text = "El valor de mercado del producto " + estanteController.nombreDelProductoEnEstante + " es " + estanteController.PrecioDelProductoBase.ToString() + "$";
        }
        else
        {
            
            TextoInput.text = estanteController.PrecioDelProducto.ToString();
            panelPrecio.SetActive(false);
        }
    }


    
}
