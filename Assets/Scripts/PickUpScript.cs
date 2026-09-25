using UnityEngine;
using TMPro;
using System.Collections;

public class PickUpScript : MonoBehaviour
{
  

    public GameObject player;
    public Transform holdPos;
    public int layerorginal;
    public float FuerzaDeLanzamiento = 500f; 
    public float RangoParaCogerObjeto = 5f; 
    private float SensibilidadDeRotacion = 2f; 
    public GameObject ObjAgarrado; 
    private Rigidbody ObjAgarradoRb; 
    private bool canDrop = true; 
    private int LayerNumber; 
    public bool Rotando = false;
    public float TiempoActualDeCarga = 1f;
    bool isCharging = false;
    public TextMeshProUGUI ObjName;
    public GameObject ObjNamePanel;
    RaycastHit hit;
    int LayerOriginalPlayer;
    
    
    void Start()
    {
        LayerOriginalPlayer = LayerMask.NameToLayer("Player");
        LayerNumber = LayerMask.NameToLayer("Hold_Layer"); 
        ObjNamePanel.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            if (ObjAgarrado == null) 
            {
                
               
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, RangoParaCogerObjeto)) 
                {
                    
                    if (hit.transform.gameObject.tag == "CanPickUp")
                    {
                       
                    
                        PickUpObject(hit.transform.gameObject);
                        ObjNamePanel.SetActive(true);
                    }
                }
            }
            else
            {
                if (canDrop == true)
                {
                    StopClipping(); 
                    DropObject();
                }
            }
        }
        if (ObjAgarrado != null) 
        {
            MoveObject(); 
            RotateObject();
            if (Input.GetKeyDown(KeyCode.Mouse0) && canDrop == true) 
            {
                StopClipping();
                StartTimer();
                
            }
            if (Input.GetKeyUp(KeyCode.Mouse0) && canDrop == true)
            {
                ThrowObject();
            }

            if (isCharging == true) 
            {
                if (TiempoActualDeCarga < 3f)
                {
                    TiempoActualDeCarga += 1 * Time.deltaTime;
                }
            }
           
        }
    }
    void PickUpObject(GameObject pickUpObj)
    {
        if (pickUpObj.GetComponent<Rigidbody>()) 
        {
            
            layerorginal = hit.collider.gameObject.layer;
            ObjName.text = hit.transform.gameObject.name;
            ObjAgarrado = pickUpObj; 
            ObjAgarradoRb = pickUpObj.GetComponent<Rigidbody>(); 
            ObjAgarradoRb.isKinematic = true;
            ObjAgarradoRb.transform.parent = holdPos.transform; 
            ObjAgarrado.layer = LayerNumber; 
           
            Physics.IgnoreCollision(ObjAgarrado.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        }
    }
    void DropObject()
    {
        ObjNamePanel.SetActive(false);
        Physics.IgnoreCollision(ObjAgarrado.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        ObjAgarrado.layer = layerorginal; 
        ObjAgarradoRb.isKinematic = false;
        ObjAgarrado.transform.parent = null; 
        ObjAgarrado = null;
        hit.collider.gameObject.layer = layerorginal;
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
        player .layer = LayerOriginalPlayer;
    }
}

