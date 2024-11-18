using UnityEngine;

public class DialogoTrigger : MonoBehaviour
{
    public string[] dialogo; // Las líneas de diálogo que se mostrarán
    public string[] personajes; // Los personajes que hablan en el diálogo
    private bool jugadorDentroDelTrigger = false;

    // Referencia al DialogoManager
    public DialogoManager dialogoManager;

    void Update()
    {
        if (jugadorDentroDelTrigger && Input.GetKeyDown(KeyCode.E))
        {
            // Si el jugador presiona E, inicia el diálogo
            dialogoManager.IniciarDialogo(dialogo, personajes);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorDentroDelTrigger = true;
            // Pausar el juego y mostrar el diálogo
            Time.timeScale = 0f; // Pausa el juego
            Cursor.lockState = CursorLockMode.None; // Desbloquea el cursor
            Cursor.visible = true; // Muestra el cursor
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorDentroDelTrigger = false;
            // Puedes hacer que el juego continúe automáticamente cuando el jugador salga del trigger si lo deseas
            // Time.timeScale = 1f;
        }
    }
}
