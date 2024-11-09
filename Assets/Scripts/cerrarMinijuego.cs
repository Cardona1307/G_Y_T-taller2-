using UnityEngine;

public class CerrarMinijuego : MonoBehaviour
{
    public GameObject canvasMinijuego; // El Canvas del minijuego

    // Este método es llamado cuando el botón de "Cerrar Puzzle" es presionado
    public void CerrarMinijuegoCanvas()
    {
        if (canvasMinijuego != null)
        {
            canvasMinijuego.SetActive(false); // Desactivar el Canvas del minijuego
            Time.timeScale = 1f; // Reanudar el juego

            // Volver a bloquear el cursor y ocultarlo
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
