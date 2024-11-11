using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform jugador; // Asigna el jugador en el inspector
    public float distanciaMaxima = 5f; // Distancia máxima antes de aplicar zoom
    public float zoomSuavizado = 0.1f; // Suavizado para el zoom
    public Vector3 offset = new Vector3(0, 5, -10); // Ajuste en el eje Y y Z para la posición lateral
    private Camera camara;

    void Start()
    {
        camara = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        // Actualiza solo la posición X de la cámara
        Vector3 nuevaPosicion = new Vector3(jugador.position.x, transform.position.y, transform.position.z);
        transform.position = nuevaPosicion;

        // Calcula la distancia entre la cámara y el jugador en el eje horizontal
        float distanciaJugador = Mathf.Abs(jugador.position.x - transform.position.x);

        // Ajusta el tamaño del zoom si el jugador se aleja del rango
        if (distanciaJugador > distanciaMaxima)
        {
            float zoomObjetivo = Mathf.Lerp(camara.orthographicSize, 7f, zoomSuavizado); // Ajusta 7f según la cantidad de zoom deseado
            camara.orthographicSize = Mathf.Clamp(zoomObjetivo, 5f, 10f); // Limita el zoom entre un mínimo y un máximo
        }
        else
        {
            // Retorna al tamaño original si el jugador está en el rango permitido
            camara.orthographicSize = Mathf.Lerp(camara.orthographicSize, 5f, zoomSuavizado); // 5f es el tamaño normal de la cámara
        }
    }
}
