using UnityEngine;

public class NPCDialogTrigger : MonoBehaviour
{
    [Header("Diálogos Generales")]
    [TextArea(3, 10)] public string[] dialogoTexto; // Diálogos principales
    public string[] nombresDePersonajes; // Nombres de los personajes en los diálogos
    public Sprite[] avatares; // Avatares asociados a los diálogos

    [Header("Diálogos de Misión")]
    [TextArea(3, 10)] public string[] dialogoMisionNoCompletada; // Mensajes para misión no completada
    public string[] nombresMisionNoCompletada; // Nombres para misión no completada
    public Sprite[] avataresMisionNoCompletada; // Avatares para misión no completada

    [TextArea(3, 10)] public string[] dialogoMisionCompletada; // Mensajes para misión completada
    public string[] nombresMisionCompletada; // Nombres para misión completada
    public Sprite[] avataresMisionCompletada; // Avatares para misión completada

    [Header("Misión")]
    public IMision mision; // Referencia a la misión de este NPC (asignada desde el Inspector)

    [Header("UI")]
    public UIManager uiManager; // Referencia al UI Manager

    private bool jugadorEnRango = false; // Verifica si el jugador está cerca
    private bool dialogoPrincipalMostrado = false; // Marca si el diálogo principal ya ha sido mostrado

    void Update()
    {
        // Verificar si el jugador está en rango y presiona la tecla de interacción
        if (jugadorEnRango && Input.GetKeyDown(KeyCode.E))
        {
            if (!dialogoPrincipalMostrado)
            {
                MostrarDialogoPrincipal();
                dialogoPrincipalMostrado = true;
                Debug.Log("¡Misión iniciada!");
                ActivarMision();
            }
            else
            {
                MostrarDialogoMision();
            }
        }
    }

    // Mostrar el diálogo principal del NPC
    private void MostrarDialogoPrincipal()
    {
        DialogoManager dialogoManager = FindObjectOfType<DialogoManager>();
        if (dialogoManager != null)
        {
            dialogoManager.IniciarDialogo(dialogoTexto, nombresDePersonajes, avatares);
        }
        else
        {
            Debug.LogError("No se encontró un DialogoManager en la escena.");
        }
    }

    // Mostrar el diálogo de misión, dependiendo de si está completada o no
    private void MostrarDialogoMision()
    {
        string[] dialogoActual;
        string[] nombresActuales;
        Sprite[] avataresActuales;

        // Verificar si la misión está asignada y su estado
        if (mision != null)
        {
            if (mision.EstaCompletada())
            {
                // Mostrar diálogo de misión completada
                dialogoActual = dialogoMisionCompletada;
                nombresActuales = nombresMisionCompletada;
                avataresActuales = avataresMisionCompletada;
            }
            else
            {
                // Mostrar diálogo de misión no completada
                dialogoActual = dialogoMisionNoCompletada;
                nombresActuales = nombresMisionNoCompletada;
                avataresActuales = avataresMisionNoCompletada;
            }
        }
        else
        {
            // Mostrar los diálogos generales si no hay misión asignada
            dialogoActual = dialogoTexto;
            nombresActuales = nombresDePersonajes;
            avataresActuales = avatares;
        }

        // Iniciar el diálogo con el DialogManager
        DialogoManager dialogoManager = FindObjectOfType<DialogoManager>();
        if (dialogoManager != null)
        {
            dialogoManager.IniciarDialogo(dialogoActual, nombresActuales, avataresActuales);
        }
        else
        {
            Debug.LogError("No se encontró un DialogoManager en la escena.");
        }
    }

    // Activar la misión cuando el jugador interactúa por primera vez
    private void ActivarMision()
    {
        if (mision != null)
        {
            Debug.Log("La misión ha sido activada.");
            // Solo activamos la misión al interactuar, no la completamos de inmediato
            // mision.CompletarMision(); // Esto se desactiva porque la misión debe completarse más tarde
        }
    }

    // Detectar cuando el jugador entra en el rango del NPC
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnRango = true;
            if (uiManager != null)
            {
                uiManager.MostrarInteractKey(true); // Mostrar la tecla de interacción
            }
        }
    }

    // Detectar cuando el jugador sale del rango del NPC
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnRango = false;
            if (uiManager != null)
            {
                uiManager.MostrarInteractKey(false); // Ocultar la tecla de interacción
            }
        }
    }
}
