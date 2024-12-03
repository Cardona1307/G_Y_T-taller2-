using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogoManager : MonoBehaviour
{
    public TMP_Text dialogoTexto; // Cambiado a TextMeshPro
    public Image avatarImagen; // Imagen del personaje que cambia con cada línea
    public TMP_Text nombrePersonaje; // Cambiado a TextMeshPro
    public GameObject panelDialogo; // Panel del diálogo
    public GameObject continueKeyPanel; // Panel para mostrar la tecla "Continuar"

    private bool dialogoActivo = false;
    private bool dialogoCompletado = false; // Estado del diálogo
    private string[] lineasDeDialogo; // Líneas de texto
    private string[] nombresPersonajes; // Nombres de los personajes
    private Sprite[] avatares; // Avatares correspondientes a las líneas
    private int indiceActual = 0;

    // Propiedad para acceder al estado del diálogo
    public bool DialogoCompletado
    {
        get { return dialogoCompletado; }
    }

    void Update()
    {
        // Detecta la tecla "F" para avanzar en el diálogo
        if (dialogoActivo && Input.GetKeyDown(KeyCode.F))
        {
            MostrarSiguienteLinea();
        }
    }

    // Método para iniciar el diálogo, acepta texto, nombres y avatares
    public void IniciarDialogo(string[] nuevasLineas, string[] nombres, Sprite[] nuevosAvatares)
    {
        lineasDeDialogo = nuevasLineas;
        nombresPersonajes = nombres;
        avatares = nuevosAvatares; // Asigna los avatares
        indiceActual = 0;
        dialogoActivo = true;
        dialogoCompletado = false; // Reinicia el estado
        panelDialogo.SetActive(true); // Activa el panel del diálogo
        continueKeyPanel.SetActive(true); // Asegura que el panel de la tecla de continuar se activa
        MostrarSiguienteLinea();
    }

    // Muestra la siguiente línea del diálogo
    public void MostrarSiguienteLinea()
    {
        if (indiceActual < lineasDeDialogo.Length)
        {
            dialogoTexto.text = lineasDeDialogo[indiceActual];
            nombrePersonaje.text = nombresPersonajes[indiceActual];

            // Cambia la imagen del avatar según la línea
            if (indiceActual < avatares.Length && avatares[indiceActual] != null)
            {
                avatarImagen.sprite = avatares[indiceActual];
                avatarImagen.enabled = true;
            }
            else
            {
                avatarImagen.enabled = false; // Oculta la imagen si no hay avatar
            }

            indiceActual++; // Aumenta el índice para la siguiente línea
        }
        else
        {
            TerminarDialogo();
        }
    }

    // Termina el diálogo y oculta el panel
    public void TerminarDialogo()
    {
        dialogoActivo = false;
        dialogoCompletado = true; // Marca el diálogo como completado
        panelDialogo.SetActive(false); // Desactiva el panel del diálogo
        continueKeyPanel.SetActive(false); // Oculta el panel de la tecla
    }
}
