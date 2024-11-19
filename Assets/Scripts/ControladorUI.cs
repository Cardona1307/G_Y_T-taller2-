using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorUI : MonoBehaviour
{
    // M�todo para cargar una escena por su nombre
    public void CambiarEscena(string nombreEscena)
    {
        SceneManager.LoadScene(nombreEscena);
    }

    // M�todo para salir del juego
    public void SalirDelJuego()
    {
        UnityEngine.Debug.Log("Saliendo del juego...");
        UnityEngine.Application.Quit();
    }
}
