using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject menuPausaCanvas; // Referencia al Canvas del menú de pausa
    private bool juegoEnPausa = false; // Estado del juego

    void Update()
    {
        // Detecta si se presiona la tecla ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoEnPausa)
            {
                ReanudarJuego();
            }
            else
            {
                PausarJuego();
            }
        }
    }

    public void PausarJuego()
    {
        juegoEnPausa = true;
        Time.timeScale = 0f; // Detiene el tiempo
        Cursor.lockState = CursorLockMode.None; // Desbloquea el cursor
        Cursor.visible = true; // Muestra el cursor
        menuPausaCanvas.SetActive(true); // Activa el Canvas de pausa
    }

    public void ReanudarJuego()
    {
        juegoEnPausa = false;
        Time.timeScale = 1f; // Restaura el tiempo
        Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor
        Cursor.visible = false; // Oculta el cursor
        menuPausaCanvas.SetActive(false); // Desactiva el Canvas de pausa
    }
}
