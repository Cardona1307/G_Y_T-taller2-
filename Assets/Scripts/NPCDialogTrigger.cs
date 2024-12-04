using UnityEngine;

public class NPCDialogTrigger : MonoBehaviour
{
    [Header("Diálogo Principal")]
    [TextArea(3, 10)]
    public string[] dialogoTexto; // Texto del diálogo cuando la misión está completada
    public string[] nombresDePersonajes; // Nombres de los personajes
    public Sprite[] avatares; // Avatares de los personajes

    [Header("Diálogo Misión Incompleta")]
    [TextArea(3, 10)]
    public string[] dialogoMisionIncompleta; // Mensaje personalizado cuando la misión no está completada

    [Header("Referencias")]
    public DialogoManager dialogoManager; // Referencia al sistema de diálogos
    public UIManager uiManager; // Referencia al sistema de UI para mostrar la tecla "E"
    public MissionManager missionManager; // Referencia al sistema de misiones

    private bool jugadorEnRango;

    void Update()
    {
        if (jugadorEnRango && Input.GetKeyDown(KeyCode.E))
        {
            if (dialogoManager != null)
            {
                if (missionManager != null)
                {
                    if (missionManager.MisionCompletada())
                    {
                        // Mostrar el diálogo principal al completar la misión
                        dialogoManager.IniciarDialogo(dialogoTexto, nombresDePersonajes, avatares);
                    }
                    else
                    {
                        // Mostrar el mensaje de misión incompleta
                        dialogoManager.IniciarDialogo(dialogoMisionIncompleta, nombresDePersonajes, avatares);
                    }
                }
                else
                {
                    Debug.LogError("MissionManager no asignado en el Inspector.");
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
        if (other.CompareTag("Player"))
        {
            jugadorEnRango = true;
            if (uiManager != null)
            {
                // Mostrar la tecla "E" en la pantalla
                uiManager.MostrarInteractKey(true);
            }
            else
            {
                Debug.LogError("UIManager no asignado en el Inspector.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnRango = false;
            if (uiManager != null)
            {
                // Ocultar la tecla "E" de la pantalla
                uiManager.MostrarInteractKey(false);
            }
        }
    }
}
