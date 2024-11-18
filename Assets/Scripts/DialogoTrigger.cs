using UnityEngine;

public class DialogoTrigger : MonoBehaviour
{
    [TextArea(3, 10)]
    public string[] dialogoTexto;
    public string[] nombresDePersonajes;
    public DialogoManager dialogoManager;

    private bool jugadorEnRango;

    void Update()
    {
        if (jugadorEnRango && Input.GetKeyDown(KeyCode.E))
        {
            if (dialogoManager != null && !dialogoManager.DialogoCompletado)
            {
                if (!dialogoManager.panelDialogo.activeSelf)
                {
                    dialogoManager.IniciarDialogo(dialogoTexto, nombresDePersonajes);
                }
            }
            else
            {
                Debug.Log("El diálogo ya fue completado y no puede activarse nuevamente.");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !jugadorEnRango)
        {
            Debug.Log("Jugador activó el diálogo por primera vez.");
            jugadorEnRango = true;
            if (dialogoManager != null)
            {
                dialogoManager.IniciarDialogo(dialogoTexto, nombresDePersonajes);
            }
            else
            {
                Debug.LogError("DialogoManager no está asignado en el Inspector.");
            }
            gameObject.SetActive(false);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnRango = false;
        }
    }
}
