using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JugarController : MonoBehaviour
{
    public Button PlayButton;
    
    public void CragarEscenaJuego()
    {
        SceneManager.LoadScene(1);
    }
}
