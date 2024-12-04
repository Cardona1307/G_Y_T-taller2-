using UnityEngine;

public class MissionManager : MonoBehaviour
{
    [Header("Misiones")]
    public bool misionCompletada; // Estado de la misi�n, se puede cambiar desde el juego o desde otro sistema.

    // Aqu� puedes agregar m�s misiones si es necesario en el futuro.
    // public bool mision2Completada;

    void Start()
    {
        // Inicializar el estado de la misi�n, puede ser desde un archivo, base de datos, o como sea necesario.
        misionCompletada = false; // Inicializa a falso, por ejemplo.
    }

    // M�todo para verificar si la misi�n est� completada
    public bool MisionCompletada()
    {
        return misionCompletada;
    }

    // M�todo para completar la misi�n, puede ser llamado desde otros scripts o eventos.
    public void CompletarMision()
    {
        misionCompletada = true;
        Debug.Log("Misi�n completada!");
    }

    // M�todo para resetear la misi�n, en caso de que quieras reiniciarla en alg�n momento.
    public void ResetearMision()
    {
        misionCompletada = false;
        Debug.Log("Misi�n reseteada.");
    }
}
