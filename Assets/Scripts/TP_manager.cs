using UnityEngine;

public class TP_manager : MonoBehaviour
{
    public Transform personaje; // Referencia al personaje
    public Camera camara; // Referencia a la c�mara del personaje
    private Vector3 puntoOrigen; // Guarda la posici�n de origen del �ltimo TP

    // M�todo para teletransportar al personaje
    public void Teletransportar(Vector3 posicionDestino, bool guardarOrigen = true)
    {
        if (guardarOrigen)
        {
            puntoOrigen = personaje.position; // Guarda el punto de origen
        }

        // Mover personaje y c�mara
        personaje.position = posicionDestino;
        camara.transform.position = posicionDestino; // Si la c�mara sigue al personaje, podr�a omitirse
    }

    // M�todo para regresar al punto de origen
    public void RegresarAlOrigen()
    {
        Teletransportar(puntoOrigen, false); // No vuelve a guardar el origen
    }
}
