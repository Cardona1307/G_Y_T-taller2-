using UnityEngine;

public class NPCDialogTrigger : MonoBehaviour
{
    [Header("Di�logo Principal")]
    [TextArea(3, 10)]
    public string[] dialogoTexto; // Texto del di�logo cuando la misi�n est� completada
    public string[] nombresDePersonajes; // Nombres de los personajes
    public Sprite[] avatares; // Avatares de los personajes

    [Header("Di�logo Misi�n Incompleta")]
    [TextArea(3, 10)]
    public string[] dialogoMisionIncompleta; // Mensaje personalizado cuando la misi�n no est� completada

    [Header("Referencias")]
    public DialogoManager dialogoManager; // Referencia al sistema de di�logos
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
                        // Mostrar el di�logo principal al completar la misi�n
                        dialogoManager.IniciarDialogo(dialogoTexto, nombresDePersonajes, avatares);
                    }
                    else
                    {
                        // Mostrar el mensaje de misi�n incompleta
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
