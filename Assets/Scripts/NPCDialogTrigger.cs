using UnityEngine;

public class NPCDialogTrigger : MonoBehaviour
{
    [Header("Di�logos Generales")]
    [TextArea(3, 10)] public string[] dialogoTexto; // Di�logos principales
    public string[] nombresDePersonajes; // Nombres de los personajes en los di�logos
    public Sprite[] avatares; // Avatares asociados a los di�logos

    [Header("Di�logos de Misi�n")]
    [TextArea(3, 10)] public string[] dialogoMisionNoCompletada; // Mensajes para misi�n no completada
    public string[] nombresMisionNoCompletada; // Nombres para misi�n no completada
    public Sprite[] avataresMisionNoCompletada; // Avatares para misi�n no completada

    [TextArea(3, 10)] public string[] dialogoMisionCompletada; // Mensajes para misi�n completada
    public string[] nombresMisionCompletada; // Nombres para misi�n completada
    public Sprite[] avataresMisionCompletada; // Avatares para misi�n completada

    [Header("Misi�n")]
    public IMision mision; // Referencia a la misi�n de este NPC (asignada desde el Inspector)

    [Header("UI")]
    public UIManager uiManager; // Referencia al UI Manager

    private bool jugadorEnRango = false; // Verifica si el jugador est� cerca
    private bool dialogoPrincipalMostrado = false; // Marca si el di�logo principal ya ha sido mostrado

    void Update()
    {
        // Verificar si el jugador est� en rango y presiona la tecla de interacci�n
        if (jugadorEnRango && Input.GetKeyDown(KeyCode.E))
        {
            if (!dialogoPrincipalMostrado)
            {
                MostrarDialogoPrincipal();
                dialogoPrincipalMostrado = true;
                Debug.Log("�Misi�n iniciada!");
                ActivarMision();
            }
            else
            {
                MostrarDialogoMision();
            }
        }
    }

    // Mostrar el di�logo principal del NPC
    private void MostrarDialogoPrincipal()
    {
        DialogoManager dialogoManager = FindObjectOfType<DialogoManager>();
        if (dialogoManager != null)
        {
            dialogoManager.IniciarDialogo(dialogoTexto, nombresDePersonajes, avatares);
        }
        else
        {
            Debug.LogError("No se encontr� un DialogoManager en la escena.");
        }
    }

    // Mostrar el di�logo de misi�n, dependiendo de si est� completada o no
    private void MostrarDialogoMision()
    {
        string[] dialogoActual;
        string[] nombresActuales;
        Sprite[] avataresActuales;

        // Verificar si la misi�n est� asignada y su estado
        if (mision != null)
        {
            if (mision.EstaCompletada())
            {
                // Mostrar di�logo de misi�n completada
                dialogoActual = dialogoMisionCompletada;
                nombresActuales = nombresMisionCompletada;
                avataresActuales = avataresMisionCompletada;
            }
            else
            {
                // Mostrar di�logo de misi�n no completada
                dialogoActual = dialogoMisionNoCompletada;
                nombresActuales = nombresMisionNoCompletada;
                avataresActuales = avataresMisionNoCompletada;
            }
        }
        else
        {
            // Mostrar los di�logos generales si no hay misi�n asignada
            dialogoActual = dialogoTexto;
            nombresActuales = nombresDePersonajes;
            avataresActuales = avatares;
        }

        // Iniciar el di�logo con el DialogManager
        DialogoManager dialogoManager = FindObjectOfType<DialogoManager>();
        if (dialogoManager != null)
        {
            dialogoManager.IniciarDialogo(dialogoActual, nombresActuales, avataresActuales);
        }
        else
        {
            Debug.LogError("No se encontr� un DialogoManager en la escena.");
        }
    }

    // Activar la misi�n cuando el jugador interact�a por primera vez
    private void ActivarMision()
    {
        if (mision != null)
        {
            Debug.Log("La misi�n ha sido activada.");
            // Solo activamos la misi�n al interactuar, no la completamos de inmediato
            // mision.CompletarMision(); // Esto se desactiva porque la misi�n debe completarse m�s tarde
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
                uiManager.MostrarInteractKey(true); // Mostrar la tecla de interacci�n
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
                uiManager.MostrarInteractKey(false); // Ocultar la tecla de interacci�n
            }
        }
    }
}
