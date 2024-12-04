using UnityEngine;

public class DialogoTrigger : MonoBehaviour
{
    [TextArea(3, 10)]
    public string[] dialogoTexto; // Texto de los di�logos
    public string[] nombresDePersonajes; // Nombres de los personajes
    public Sprite[] avatares; // Avatares correspondientes a los di�logos
    public DialogoManager dialogoManager; // Referencia al DialogoManager

    private bool jugadorEnRango;

    void Update()
    {
        // Si el jugador est� en rango y presiona "E"
        if (jugadorEnRango && Input.GetKeyDown(KeyCode.E))
        {
            if (dialogoManager != null)
            {
                // Solo inicia el di�logo si el panel no est� activo
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
            Debug.Log("Jugador en rango para iniciar di�logo.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Verifica si el jugador sale del rango
        if (other.CompareTag("Player"))
        {
            jugadorEnRango = false;
            Debug.Log("Jugador sali� del rango de di�logo.");
        }
    }
}
