using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogoManager : MonoBehaviour
{
    public TMP_Text dialogoTexto;
    public Image avatarImagen;
    public TMP_Text nombrePersonaje;
    public GameObject panelDialogo;
    public GameObject continueKeyPanel; // Para mostrar la tecla de continuar

    private string[] lineasDeDialogo;
    private string[] nombresPersonajes;
    private Sprite[] avatares;
    private int indiceActual = 0;

    // Método para iniciar el diálogo
    public void IniciarDialogo(string[] nuevasLineas, string[] nombres, Sprite[] nuevosAvatares)
    {
        lineasDeDialogo = nuevasLineas;
        nombresPersonajes = nombres;
        avatares = nuevosAvatares;
        indiceActual = 0;
        panelDialogo.SetActive(true);
        continueKeyPanel.SetActive(true);
        MostrarSiguienteLinea();
    }

    void Update()
    {
        if (panelDialogo.activeSelf && Input.GetKeyDown(KeyCode.F)) // Tecla F para avanzar
        {
            MostrarSiguienteLinea();
        }
    }

    void MostrarSiguienteLinea()
    {
        if (indiceActual < lineasDeDialogo.Length)
        {
            dialogoTexto.text = lineasDeDialogo[indiceActual];
            nombrePersonaje.text = nombresPersonajes[indiceActual];
            if (indiceActual < avatares.Length && avatares[indiceActual] != null)
            {
                avatarImagen.sprite = avatares[indiceActual];
                avatarImagen.enabled = true;
            }
            else
            {
                avatarImagen.enabled = false;
            }
            indiceActual++;
        }
        else
        {
            TerminarDialogo();
        }
    }

    void TerminarDialogo()
    {
        panelDialogo.SetActive(false);
        continueKeyPanel.SetActive(false);
    }
}
