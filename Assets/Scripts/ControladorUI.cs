using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorUI : MonoBehaviour
{
    
    public void CambiarEscena(string nombreEscena)
    {
        SceneManager.LoadScene(nombreEscena);
    }

    
    public void SalirDelJuego()
    {
        UnityEngine.Debug.Log("Saliendo del juego...");
        UnityEngine.Application.Quit();
    }
}
