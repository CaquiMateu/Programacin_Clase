using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EtiquetaDePrecio : MonoBehaviour
{
    [SerializeField] public GameObject ObjetoPadre;
    [SerializeField] EstanteController estanteController;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ObjetoPadre = transform.parent.gameObject;
        estanteController = ObjetoPadre.GetComponent<EstanteController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    
}
