using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform jugador; // Asigna el Transform del jugador en el Inspector
    public float distanciaMaxima = 5f; // Distancia m�xima en X antes de ajustar el zoom
    public float zoomSuavizado = 0.1f; // Suavizado para el zoom
    public float zoomMinimo = 60f; // FOV m�nimo (alejado) cuando el jugador est� lejos
    public float zoomMaximo = 70f; // FOV m�ximo (acercado) cuando el jugador est� cerca
    private Camera camara;
    private Vector3 offsetInicial; // Offset inicial entre la c�mara y el jugador en el eje Z

    void Start()
    {
        camara = GetComponent<Camera>();
        offsetInicial = transform.position - jugador.position; // Calcula la diferencia inicial entre el jugador y la c�mara
    }

    void LateUpdate()
    {
        // Mant�n la posici�n de la c�mara centrada en el jugador en el eje Z
        transform.position = new Vector3(transform.position.x, transform.position.y, jugador.position.z + offsetInicial.z);

        // Calcula la distancia en el eje X entre la c�mara y el jugador
        float distanciaEnX = Mathf.Abs(jugador.position.x - transform.position.x);

        // Ajusta el FOV (zoom) en funci�n de la distancia en X, con un suavizado
        float zoomObjetivo;
        if (distanciaEnX > distanciaMaxima)
        {
            // Cuando el jugador est� lejos en X, usa el zoom m�nimo (alejado)
            zoomObjetivo = zoomMinimo;
        }
        else
        {
            // Cuando el jugador est� cerca en X, usa el zoom m�ximo (acercado)
            zoomObjetivo = Mathf.Lerp(zoomMaximo, zoomMinimo, distanciaEnX / distanciaMaxima);
        }

        // Aplica el FOV suavizado
        camara.fieldOfView = Mathf.Lerp(camara.fieldOfView, zoomObjetivo, zoomSuavizado);
    }
}

