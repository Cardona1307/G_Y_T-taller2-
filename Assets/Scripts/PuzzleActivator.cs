using UnityEngine;

public class PuzzleActivator : MonoBehaviour
{
    public GameObject canvasMinijuego;  // El Canvas del minijuego
    public KeyCode teclaActivacion = KeyCode.E;  // Tecla para activar el minijuego
    public float distanciaActivacion = 3f;  // Distancia de activación

    private bool jugadorCerca = false; // Si el jugador está cerca del objeto activador

    [SerializeField]
    private GameManager gameManager;

    void Update()
    {
        // Comprobamos si el jugador está dentro del rango de activación
        if (jugadorCerca && Input.GetKeyDown(teclaActivacion))
        {
            ActivarMinijuego(); // Si está cerca y presionó la tecla E, activar el minijuego
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Comprobar si el objeto que entra al área tiene la etiqueta "Player"
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;  // El jugador está cerca del activador
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Si el jugador sale del área, desactivamos la cercanía
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;  // El jugador ya no está cerca
        }
    }

    void ActivarMinijuego()
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

