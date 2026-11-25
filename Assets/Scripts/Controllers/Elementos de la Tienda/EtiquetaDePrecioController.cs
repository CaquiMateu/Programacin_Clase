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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ObjetoPadre = transform.parent.gameObject;
        estanteController = ObjetoPadre.GetComponent<EstanteController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (estanteController.vacio == false && panelPrecio.activeInHierarchy == true)
        {
            Text TextoInput = panelPrecio.transform.Find("InputField").GetComponent<InputField>().textComponent;
           
            if (TextoInput.text == "")
            {
                estanteController.PrecioDelProducto = 0;
                return;
            }
            else if (estanteController.PrecioDelProducto == 0)
            {
                TextoInput.text = estanteController.PrecioDelProducto.ToString();
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
            Text TextoInput = panelPrecio.transform.Find("InputField").GetComponent<InputField>().textComponent;
            TextoInput.text = estanteController.PrecioDelProducto.ToString();
            panelPrecio.SetActive(true);
        }
        else
        {
            panelPrecio.SetActive(false);
        }
    }


    
}
