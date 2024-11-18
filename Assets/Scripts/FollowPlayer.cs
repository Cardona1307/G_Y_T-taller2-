using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform jugador; // Referencia al Transform del jugador
    public float zoomSuavizado = 0.1f; // Suavizado del zoom
    public float zoomCercano = 60f; // FOV m�ximo (cercano) cuando el jugador se aleja
    public float zoomLejano = 70f; // FOV m�nimo (alejado) cuando el jugador est� cerca del centro
    public float distanciaZoom = 10f; // Distancia en Z para ajustar el zoom

    private Camera camara;
    private Vector3 offsetInicial; // Offset inicial entre la c�mara y el jugador

    void Start()
    {
        camara = GetComponent<Camera>();
        offsetInicial = transform.position - jugador.position; // Calcula el offset inicial
    }

    void LateUpdate()
    {
        // Mant�n la c�mara centrada en el jugador
        transform.position = jugador.position + offsetInicial;

        // Ajusta el zoom en funci�n de la posici�n del jugador en Z
        float distanciaEnZ = Mathf.Abs(jugador.position.z);
        float zoomObjetivo = Mathf.Lerp(zoomCercano, zoomLejano, distanciaEnZ / distanciaZoom);

        // Suaviza la transici�n del FOV
        camara.fieldOfView = Mathf.Lerp(camara.fieldOfView, zoomObjetivo, zoomSuavizado);
    }
}
