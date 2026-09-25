using UnityEngine;

public class EstanteController : MonoBehaviour
{
    public Transform[] EspaciosParaProductos;
    
    public int posicionActual = 0;
    void Start()
    {
        posicionActual = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (posicionActual >= EspaciosParaProductos.Length)
        {
            gameObject.GetComponent<Collider>().isTrigger = false;
        }
        else
        {
            gameObject.GetComponent<Collider>().isTrigger = true;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.GetComponent<ProductoController>() != null)
        {
            if (posicionActual >= EspaciosParaProductos.Length)
            {
                Debug.Log("No hay más espacio en el estante");
                return;
            }
            else
            {
                Instantiate(other.gameObject.GetComponent<ProductoController>().ModoEstante, EspaciosParaProductos[posicionActual].position,Quaternion.Euler(0,90,0));
                SumarÍndice();
                Destroy(other.gameObject);
            }
            
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
