using UnityEngine;

public class rotarpipe : MonoBehaviour
{
    public float rotationSpeed = 90f;  // 90 grados por clic
    private Camera playerCamera;  // Cámara del jugador

    void Start()
    {
        // Obtener la cámara principal
        playerCamera = Camera.main;
    }

    void Update()
    {
        // Detectar el clic izquierdo del mouse
        if (Input.GetMouseButtonDown(0))  // Clic izquierdo
        {
            // Crear un rayo desde la cámara hacia la posición del mouse
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Verificar si el rayo golpea algo
            if (Physics.Raycast(ray, out hit))
            {
                // Verificar si el objeto golpeado tiene el collider
                if (hit.transform == transform)
                {
                    // Llamar a la función de rotación
                    RotatePiece();
                }
            }
        }
    }

    // Método para rotar la pieza
    private void RotatePiece()
    {
        // Rotar alrededor del eje Z (solo en 2D)
        transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + rotationSpeed);
    }
}
