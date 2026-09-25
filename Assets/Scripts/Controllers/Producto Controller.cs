using UnityEngine;

public class ProductoController : MonoBehaviour
{
    public string NombreProducto;
    public float valorDelProducto = 0.0f;

    public GameObject ModoEstante;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      gameObject.name = NombreProducto;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
