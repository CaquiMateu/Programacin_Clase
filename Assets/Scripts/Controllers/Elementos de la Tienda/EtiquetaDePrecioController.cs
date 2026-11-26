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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ObjetoPadre = transform.parent.gameObject;
        estanteController = ObjetoPadre.GetComponent<EstanteController>();
        TextoInput = panelPrecio.transform.Find("InputField").GetComponent<InputField>().textComponent;
        TextoProducto = panelPrecio.transform.Find("TextoProducto").GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        

        if (estanteController.vacio == false && panelPrecio.activeInHierarchy == true)
        {
            
           
            if (TextoInput.text == "")
            {
                estanteController.PrecioDelProducto = 0;
                return;
            }
           

               estanteController.PrecioDelProducto = float.Parse(TextoInput.text);
            
            
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
