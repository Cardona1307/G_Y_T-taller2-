using UnityEngine;
using UnityEngine.UI;  // Aseg�rate de que esta importaci�n est� presente

public class DialogoManager : MonoBehaviour
{
    public Text dialogoTexto;  // Esto hace referencia a UnityEngine.UI.Text
    public Image avatarImagen; // Esto hace referencia a UnityEngine.UI.Image
    public Text nombrePersonaje; // Esto hace referencia a UnityEngine.UI.Text
    public GameObject panelDialogo; // Panel de di�logo
    private bool dialogoActivo = false;
    private string[] lineasDeDialogo; // L�neas de di�logo
    private string[] nombresPersonajes; // Nombres de los personajes
    private int indiceActual = 0;

    void Update()
    {
        if (dialogoActivo && Input.GetKeyDown(KeyCode.E))
        {
            MostrarSiguienteLinea();
        }
    }

    // M�todo para iniciar el di�logo, llamado desde el DialogoTrigger
    public void IniciarDialogo(string[] nuevasLineas, string[] nombres)
    {
        lineasDeDialogo = nuevasLineas;
        nombresPersonajes = nombres;
        indiceActual = 0;
        dialogoActivo = true;
        panelDialogo.SetActive(true);
        MostrarSiguienteLinea();
    }

    // Muestra la siguiente l�nea del di�logo
    public void MostrarSiguienteLinea()
    {
        if (indiceActual < lineasDeDialogo.Length)
        {
            dialogoTexto.text = lineasDeDialogo[indiceActual];
            nombrePersonaje.text = nombresPersonajes[indiceActual];
            indiceActual++;
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
        panelDialogo.SetActive(false);
    }
}
