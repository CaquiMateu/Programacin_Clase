using UnityEngine;

public class GameManagerController : MonoBehaviour
{

    public GameObject Tienda;

    void Start()
    {
        //Bloquea el ratón en el centro de la pantalla
        Cursor.lockState = CursorLockMode.Locked;

        //Hace que el cursor sea invisible
        Cursor.visible = false;
    }

    void Update()
    {
        #region Recuperar el ratón con Escape
        // Opcional: Si el jugador pulsa Escape, recupera el ratón (útil para menús de pausa)
        if (Tienda.activeInHierarchy == true)
        {
            // Desbloquea y vuelve a mostrar el cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            //Bloquea el ratón en el centro de la pantalla
            Cursor.lockState = CursorLockMode.Locked;

            //Hace que el cursor sea invisible
            Cursor.visible = false;

        }
        #endregion

        if (Input.GetKeyDown(KeyCode.H))
        {
            if (Tienda.activeInHierarchy == false)
            {
                Tienda.SetActive(true);
            }
            else if (Tienda.activeInHierarchy == true)
            {
                Tienda.SetActive(false);
            }

        }
        
    }
}
