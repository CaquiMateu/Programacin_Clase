using UnityEngine;
using System.Collections.Generic;

public class ListaCarrito : MonoBehaviour
{
 
    public Dictionary<GameObject, int> DiccionarioCarrito;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DiccionarioCarrito = new Dictionary<GameObject, int>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
