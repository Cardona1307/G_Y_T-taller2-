using UnityEngine;

public class PuzzleActivator : MonoBehaviour
{
    public GameObject canvasMinijuego;  // El Canvas del minijuego
    

    private bool jugadorCerca = false; // Si el jugador est� cerca del objeto activador

    [SerializeField]
    private GameManager gameManager;

    

    void OnTriggerEnter(Collider other)
    {
        // Comprobar si el objeto que entra al �rea tiene la etiqueta "Player"
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;  // El jugador est� cerca del activador
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Si el jugador sale del �rea, desactivamos la cercan�a
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;  // El jugador ya no est� cerca
        }
    }

    public void ActivarMinijuego()
    {
        // Activamos el Canvas del minijuego y pausamos el juego
        if (canvasMinijuego != null)
        {
            gameManager.isInPuzzle = true;
            canvasMinijuego.SetActive(true);  // Mostrar el Canvas del minijuego
            Time.timeScale = 0f;  // Pausar el juego para jugar el minijuego

            // Mostrar el cursor
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}

