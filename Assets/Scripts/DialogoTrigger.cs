using UnityEngine;

public class DialogoTrigger : MonoBehaviour
{
    [TextArea(3, 10)]
    public string[] dialogoTexto; // Texto de los diálogos
    public string[] nombresDePersonajes; // Nombres de los personajes
    public Sprite[] avatares; // Avatares correspondientes a los diálogos
    public DialogoManager dialogoManager; // Referencia al DialogoManager

    private bool jugadorEnRango;

    void Update()
    {
        // Si el jugador está en rango y presiona "E"
        if (jugadorEnRango && Input.GetKeyDown(KeyCode.E))
        {
            if (dialogoManager != null)
            {
                // Solo inicia el diálogo si el panel no está activo
                if (!dialogoManager.panelDialogo.activeSelf)
                {
                    dialogoManager.IniciarDialogo(dialogoTexto, nombresDePersonajes, avatares);
                }
            }
            else
            {
                Debug.LogError("DialogoManager no asignado en el Inspector.");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el jugador entra en el rango
        if (other.CompareTag("Player"))
        {
            jugadorEnRango = true;
            Debug.Log("Jugador en rango para iniciar diálogo.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Verifica si el jugador sale del rango
        if (other.CompareTag("Player"))
        {
            jugadorEnRango = false;
            Debug.Log("Jugador salió del rango de diálogo.");
        }
    }
}
