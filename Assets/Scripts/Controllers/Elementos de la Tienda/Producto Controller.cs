using UnityEngine;

public class ProductoController : MonoBehaviour
{
    [Header("Variables")]
    public string NombreProducto;
    public float valorDelProducto = 0.0f;

    [Header("Modo Estante")]
    public GameObject ModoEstante;
  
    void Start()
    {
        // Asignar el nombre del producto al GameObject
        gameObject.name = NombreProducto;
    }

    
    void Update()
    {
        
    }
}
