using UnityEngine;
using UnityEngine.UI;

public class DineroController : MonoBehaviour
{

    float Dinero = 0f;
    public Text DineroText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Dinero = 300f;
    }

    // Update is called once per frame
    void Update()
    {
        DineroText.text = Dinero.ToString() + "$"; 
    }
}
