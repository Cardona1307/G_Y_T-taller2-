using UnityEngine;
using UnityEngine.UI;  // Asegúrate de que esta importación esté presente

public class DialogoManager : MonoBehaviour
{
    public Text dialogoTexto;  // Esto hace referencia a UnityEngine.UI.Text
    public Image avatarImagen; // Esto hace referencia a UnityEngine.UI.Image
    public Text nombrePersonaje; // Esto hace referencia a UnityEngine.UI.Text
    public GameObject panelDialogo; // Panel de diálogo
    private bool dialogoActivo = false;
    private string[] lineasDeDialogo; // Líneas de diálogo
    private string[] nombresPersonajes; // Nombres de los personajes
    private int indiceActual = 0;

    void Update()
    {
        if (dialogoActivo && Input.GetKeyDown(KeyCode.E))
        {
            MostrarSiguienteLinea();
        }
    }

    // Método para iniciar el diálogo, llamado desde el DialogoTrigger
    public void IniciarDialogo(string[] nuevasLineas, string[] nombres)
    {
        lineasDeDialogo = nuevasLineas;
        nombresPersonajes = nombres;
        indiceActual = 0;
        dialogoActivo = true;
        panelDialogo.SetActive(true);
        MostrarSiguienteLinea();
    }

    // Muestra la siguiente línea del diálogo
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

    // Termina el diálogo y oculta el panel
    public void TerminarDialogo()
    {
        dialogoActivo = false;
        panelDialogo.SetActive(false);
    }
}
