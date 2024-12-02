using UnityEngine;
using UnityEngine.UI;
using TMPro; // Importa TextMeshPro

public class DialogoManager : MonoBehaviour
{
    public TextMeshProUGUI dialogoTexto; // Cambiado a TextMeshProUGUI
    public Image avatarImagen;
    public TextMeshProUGUI nombrePersonaje; // Cambiado a TextMeshProUGUI
    public GameObject panelDialogo;

    private bool dialogoActivo = false;
    private bool dialogoCompletado = false; // Nuevo estado
    private string[] lineasDeDialogo;
    private string[] nombresPersonajes;
    private int indiceActual = 0;

    public bool DialogoCompletado
    {
        get { return dialogoCompletado; }
    }

    void Update()
    {
        if (dialogoActivo && Input.GetKeyDown(KeyCode.F))
        {
            MostrarSiguienteLinea();
        }
    }

    public void IniciarDialogo(string[] nuevasLineas, string[] nombres)
    {
        lineasDeDialogo = nuevasLineas;
        nombresPersonajes = nombres;
        indiceActual = 0;
        dialogoActivo = true;
        dialogoCompletado = false; // Reiniciar estado
        panelDialogo.SetActive(true);
        MostrarSiguienteLinea();
    }

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

    public void TerminarDialogo()
    {
        dialogoActivo = false;
        dialogoCompletado = true; // Marcar como completado
        panelDialogo.SetActive(false);
    }
}
