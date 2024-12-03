using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogoManager : MonoBehaviour
{
    public TMP_Text dialogoTexto; // Cambiado a TextMeshPro
    public Image avatarImagen; // Imagen del personaje que cambia con cada l�nea
    public TMP_Text nombrePersonaje; // Cambiado a TextMeshPro
    public GameObject panelDialogo; // Panel del di�logo
    public GameObject continueKeyPanel; // Panel para mostrar la tecla "Continuar"

    private bool dialogoActivo = false;
    private bool dialogoCompletado = false; // Estado del di�logo
    private string[] lineasDeDialogo; // L�neas de texto
    private string[] nombresPersonajes; // Nombres de los personajes
    private Sprite[] avatares; // Avatares correspondientes a las l�neas
    private int indiceActual = 0;

    // Propiedad para acceder al estado del di�logo
    public bool DialogoCompletado
    {
        get { return dialogoCompletado; }
    }

    void Update()
    {
        // Detecta la tecla "F" para avanzar en el di�logo
        if (dialogoActivo && Input.GetKeyDown(KeyCode.F))
        {
            MostrarSiguienteLinea();
        }
    }

    // M�todo para iniciar el di�logo, acepta texto, nombres y avatares
    public void IniciarDialogo(string[] nuevasLineas, string[] nombres, Sprite[] nuevosAvatares)
    {
        lineasDeDialogo = nuevasLineas;
        nombresPersonajes = nombres;
        avatares = nuevosAvatares; // Asigna los avatares
        indiceActual = 0;
        dialogoActivo = true;
        dialogoCompletado = false; // Reinicia el estado
        panelDialogo.SetActive(true); // Activa el panel del di�logo
        continueKeyPanel.SetActive(true); // Asegura que el panel de la tecla de continuar se activa
        MostrarSiguienteLinea();
    }

    // Muestra la siguiente l�nea del di�logo
    public void MostrarSiguienteLinea()
    {
        if (indiceActual < lineasDeDialogo.Length)
        {
            dialogoTexto.text = lineasDeDialogo[indiceActual];
            nombrePersonaje.text = nombresPersonajes[indiceActual];

            // Cambia la imagen del avatar seg�n la l�nea
            if (indiceActual < avatares.Length && avatares[indiceActual] != null)
            {
                avatarImagen.sprite = avatares[indiceActual];
                avatarImagen.enabled = true;
            }
            else
            {
                avatarImagen.enabled = false; // Oculta la imagen si no hay avatar
            }

            indiceActual++; // Aumenta el �ndice para la siguiente l�nea
        }
        else
        {
            TerminarDialogo();
        }
    }

    // Termina el di�logo y oculta el panel
    public void TerminarDialogo()
    {
        dialogoActivo = false;
        dialogoCompletado = true; // Marca el di�logo como completado
        panelDialogo.SetActive(false); // Desactiva el panel del di�logo
        continueKeyPanel.SetActive(false); // Oculta el panel de la tecla
    }
}
