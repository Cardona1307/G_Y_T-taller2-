using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform jugador; // Asigna el Transform del jugador en el Inspector
    public float distanciaMaxima = 5f; // Distancia máxima en X antes de ajustar el zoom
    public float zoomSuavizado = 0.1f; // Suavizado para el zoom
    public float zoomMinimo = 60f; // FOV mínimo (alejado) cuando el jugador está lejos
    public float zoomMaximo = 70f; // FOV máximo (acercado) cuando el jugador está cerca
    private Camera camara;
    private Vector3 offsetInicial; // Offset inicial entre la cámara y el jugador en el eje Z

    void Start()
    {
        camara = GetComponent<Camera>();
        offsetInicial = transform.position - jugador.position; // Calcula la diferencia inicial entre el jugador y la cámara
    }

    void LateUpdate()
    {
        // Mantén la posición de la cámara centrada en el jugador en el eje Z
        transform.position = new Vector3(transform.position.x, transform.position.y, jugador.position.z + offsetInicial.z);

        // Calcula la distancia en el eje X entre la cámara y el jugador
        float distanciaEnX = Mathf.Abs(jugador.position.x - transform.position.x);

        // Ajusta el FOV (zoom) en función de la distancia en X, con un suavizado
        float zoomObjetivo;
        if (distanciaEnX > distanciaMaxima)
        {
            // Cuando el jugador está lejos en X, usa el zoom mínimo (alejado)
            zoomObjetivo = zoomMinimo;
        }
        else
        {
            // Cuando el jugador está cerca en X, usa el zoom máximo (acercado)
            zoomObjetivo = Mathf.Lerp(zoomMaximo, zoomMinimo, distanciaEnX / distanciaMaxima);
        }

        // Aplica el FOV suavizado
        camara.fieldOfView = Mathf.Lerp(camara.fieldOfView, zoomObjetivo, zoomSuavizado);
    }
}

