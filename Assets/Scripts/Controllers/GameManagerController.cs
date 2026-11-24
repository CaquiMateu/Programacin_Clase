using UnityEngine;

public class GameManagerController : MonoBehaviour
{

    public PlayerController playerController;
    
    void Start()
    {
        playerController = GameObject.Find("player").GetComponent<PlayerController>();
        //Bloquea el ratón en el centro de la pantalla
        Cursor.lockState = CursorLockMode.Locked;

        //Hace que el cursor sea invisible
        Cursor.visible = false;
    }

    void Update()
    {
        #region Recuperar el ratón con Escape
        // Opcional: Si el jugador pulsa Escape, recupera el ratón (útil para menús de pausa)
        if (playerController.panelActivo == true)
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
            if (playerController.panelActivo == false)
            {
                playerController.PanelesUI[0].SetActive(true);
            }
            else if (playerController.panelActivo == true)
            {
                playerController.PanelesUI[0].SetActive(false);
               
            }

        }
        
    }
}
