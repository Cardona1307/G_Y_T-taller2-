using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movimiento : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento
    public Transform cameraTransform; // Referencia a la cámara
    private Rigidbody rb;
    private Vector3 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Evita que el Rigidbody gire automáticamente
    }

    void Update()
    {
        // Obtener input del jugador (teclas W, A, S, D)
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Determinar la dirección de movimiento en el plano X-Z en relación con la cámara
        Vector3 forward = cameraTransform.forward; // Dirección hacia donde está mirando la cámara
        Vector3 right = cameraTransform.right; // Dirección hacia la derecha de la cámara

        // Asegurarse de que el movimiento esté solo en el plano X-Z (sin afectación en el eje Y)
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        // Dirección de movimiento: `W` mueve hacia el fondo (alejándose de la cámara)
        // y `S` mueve hacia la cámara (acercándose a ella)
        moveDirection = (forward * verticalInput + right * horizontalInput).normalized;
    }

    void FixedUpdate()
    {
        // Movimiento del personaje
        if (moveDirection != Vector3.zero)
        {
            // Mover al personaje en la dirección calculada
            rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);

            // Rotación: si el personaje se mueve lateralmente, mirará en esa dirección.
            if (moveDirection.x != 0)
            {
                // Si el personaje se mueve en el eje X (izquierda/derecha)
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);

                // Aquí corregimos la rotación para que el personaje mire en la dirección correcta
                // (invertimos 90 grados en el eje Y debido a la orientación del modelo)
                rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation * Quaternion.Euler(0, 90, 0), 0.15f);
            }
            else if (moveDirection.z != 0)
            {
                // Si se mueve en el eje Z (arriba/abajo), el personaje debe mirar hacia el fondo o la cámara
                if (moveDirection.z > 0) // Moviéndose hacia la cámara
                {
                    // Hacer que el personaje mire hacia la cámara
                    Quaternion targetRotation = Quaternion.LookRotation(-cameraTransform.forward, Vector3.up);
                    rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation * Quaternion.Euler(0, 90, 0), 0.15f);
                }
                else if (moveDirection.z < 0) // Moviéndose hacia el fondo
                {
                    // Hacer que el personaje mire hacia el fondo (opuesto a la cámara)
                    Quaternion targetRotation = Quaternion.LookRotation(cameraTransform.forward, Vector3.up);
                    rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation * Quaternion.Euler(0, 90, 0), 0.15f);
                }
            }
        }
    }
}
