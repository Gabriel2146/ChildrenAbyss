using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject menuPanel; // Panel del men� (asignar desde el Inspector)
    private bool isMenuActive = false;

    void Start()
    {
        // Asegurarse de que el men� est� desactivado y el cursor bloqueado al inicio
        if (menuPanel != null)
        {
            menuPanel.SetActive(false);
            LockCursor(true); // Cursor bloqueado al inicio
        }
        else
        {
            Debug.LogError("MenuPanel no est� asignado en el Inspector.");
        }
    }

    void Update()
    {
        // Alternar el men� con la tecla Escape
        if (Input.GetKeyDown(KeyCode.P))
        {
            isMenuActive = !isMenuActive;

            if (menuPanel != null)
            {
                menuPanel.SetActive(isMenuActive);
            }

            // Alternar el estado del cursor
            LockCursor(!isMenuActive);
        }
    }

    // Maneja el estado del cursor
    private void LockCursor(bool lockCursor)
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor al centro de la pantalla
            Cursor.visible = false; // Oculta el cursor
        }
        else
        {
            Cursor.lockState = CursorLockMode.None; // Libera el cursor
            Cursor.visible = true; // Muestra el cursor
        }
    }
}
