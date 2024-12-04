using UnityEngine;

public class MissionManager : MonoBehaviour
{
    [Header("Misiones")]
    public bool misionCompletada; // Estado de la misión, se puede cambiar desde el juego o desde otro sistema.

    // Aquí puedes agregar más misiones si es necesario en el futuro.
    // public bool mision2Completada;

    void Start()
    {
        // Inicializar el estado de la misión, puede ser desde un archivo, base de datos, o como sea necesario.
        misionCompletada = false; // Inicializa a falso, por ejemplo.
    }

    // Método para verificar si la misión está completada
    public bool MisionCompletada()
    {
        return misionCompletada;
    }

    // Método para completar la misión, puede ser llamado desde otros scripts o eventos.
    public void CompletarMision()
    {
        misionCompletada = true;
        Debug.Log("Misión completada!");
    }

    // Método para resetear la misión, en caso de que quieras reiniciarla en algún momento.
    public void ResetearMision()
    {
        misionCompletada = false;
        Debug.Log("Misión reseteada.");
    }
}
