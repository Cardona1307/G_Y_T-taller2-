using UnityEngine;

public class rotarpipe : MonoBehaviour
{
    public float rotationSpeed = 90f;  // 90 grados por clic
    private Camera playerCamera;  // C�mara del jugador

    void Start()
    {
        // Obtener la c�mara principal
        playerCamera = Camera.main;
    }

    void Update()
    {
        // Detectar el clic izquierdo del mouse
        if (Input.GetMouseButtonDown(0))  // Clic izquierdo
        {
            // Crear un rayo desde la c�mara hacia la posici�n del mouse
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Verificar si el rayo golpea algo
            if (Physics.Raycast(ray, out hit))
            {
                // Verificar si el objeto golpeado tiene el collider
                if (hit.transform == transform)
                {
                    // Llamar a la funci�n de rotaci�n
                    RotatePiece();
                }
            }
        }
    }

    // M�todo para rotar la pieza
    private void RotatePiece()
    {
        // Rotar alrededor del eje Z (solo en 2D)
        transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + rotationSpeed);
    }
}
