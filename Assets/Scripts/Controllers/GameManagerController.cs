using UnityEngine;

public class GameManagerController : MonoBehaviour
{
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Desbloquea y vuelve a mostrar el cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        #endregion
    }
}
