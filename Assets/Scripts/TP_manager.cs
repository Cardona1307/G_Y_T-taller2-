using UnityEngine;

public class TP_manager : MonoBehaviour
{
    public Transform personaje; // Referencia al personaje
    public Camera camara; // Referencia a la cámara del personaje
    private Vector3 puntoOrigen; // Guarda la posición de origen del último TP

    // Método para teletransportar al personaje
    public void Teletransportar(Vector3 posicionDestino, bool guardarOrigen = true)
    {
        if (guardarOrigen)
        {
            puntoOrigen = personaje.position; // Guarda el punto de origen
        }

        // Mover personaje y cámara
        personaje.position = posicionDestino;
        camara.transform.position = posicionDestino; // Si la cámara sigue al personaje, podría omitirse
    }

    // Método para regresar al punto de origen
    public void RegresarAlOrigen()
    {
        Teletransportar(puntoOrigen, false); // No vuelve a guardar el origen
    }
}
