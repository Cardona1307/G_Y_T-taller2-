using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform jugador; // Asigna el jugador en el inspector
    public float distanciaMaxima = 5f; // Distancia m�xima antes de aplicar zoom
    public float zoomSuavizado = 0.1f; // Suavizado para el zoom
    public Vector3 offset = new Vector3(0, 5, -10); // Ajuste en el eje Y y Z para la posici�n lateral
    private Camera camara;

    void Start()
    {
        camara = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        // Actualiza solo la posici�n X de la c�mara
        Vector3 nuevaPosicion = new Vector3(jugador.position.x, transform.position.y, transform.position.z);
        transform.position = nuevaPosicion;

        // Calcula la distancia entre la c�mara y el jugador en el eje horizontal
        float distanciaJugador = Mathf.Abs(jugador.position.x - transform.position.x);

        // Ajusta el tama�o del zoom si el jugador se aleja del rango
        if (distanciaJugador > distanciaMaxima)
        {
            float zoomObjetivo = Mathf.Lerp(camara.orthographicSize, 7f, zoomSuavizado); // Ajusta 7f seg�n la cantidad de zoom deseado
            camara.orthographicSize = Mathf.Clamp(zoomObjetivo, 5f, 10f); // Limita el zoom entre un m�nimo y un m�ximo
        }
        else
        {
            // Retorna al tama�o original si el jugador est� en el rango permitido
            camara.orthographicSize = Mathf.Lerp(camara.orthographicSize, 5f, zoomSuavizado); // 5f es el tama�o normal de la c�mara
        }
    }
}
