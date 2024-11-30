using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform jugador; // Referencia al Transform del jugador
    public float zoomSuavizado = 0.1f; // Suavizado del zoom
    public float zoomCercano = 60f; // FOV máximo (cercano) cuando el jugador se aleja
    public float zoomLejano = 70f; // FOV mínimo (alejado) cuando el jugador está cerca del centro
    public float distanciaZoom = 10f; // Distancia en Z para ajustar el zoom
    public float limiteX = 33.4021454f; // Límite máximo en el eje X para la cámara

    private Camera camara;
    private Vector3 offsetInicial; // Offset inicial entre la cámara y el jugador

    void Start()
    {
        camara = GetComponent<Camera>();
        offsetInicial = transform.position - jugador.position; // Calcula el offset inicial
    }

    void LateUpdate()
    {
        // Calcula la nueva posición basada en el jugador
        Vector3 nuevaPosicion = jugador.position + offsetInicial;

        // Limitar la posición en el eje X
        if (nuevaPosicion.x > limiteX)
        {
            nuevaPosicion.x = limiteX; // No permitir que la cámara pase el límite en X
        }

        // Actualiza la posición de la cámara
        transform.position = nuevaPosicion;

        // Ajusta el zoom en función de la posición del jugador en Z
        float distanciaEnZ = Mathf.Abs(jugador.position.z);
        float zoomObjetivo = Mathf.Lerp(zoomCercano, zoomLejano, distanciaEnZ / distanciaZoom);

        // Suaviza la transición del FOV
        camara.fieldOfView = Mathf.Lerp(camara.fieldOfView, zoomObjetivo, zoomSuavizado);
    }
}
