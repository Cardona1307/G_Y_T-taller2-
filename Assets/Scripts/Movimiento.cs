using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movimiento : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento
    public Transform cameraTransform; // Referencia a la c�mara
    private Rigidbody rb;
    private Vector3 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Evita que el Rigidbody gire autom�ticamente
    }

    void Update()
    {
        // Obtener input del jugador (teclas W, A, S, D)
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Determinar la direcci�n de movimiento en el plano X-Z en relaci�n con la c�mara
        Vector3 forward = cameraTransform.forward; // Direcci�n hacia donde est� mirando la c�mara
        Vector3 right = cameraTransform.right; // Direcci�n hacia la derecha de la c�mara

        // Asegurarse de que el movimiento est� solo en el plano X-Z (sin afectaci�n en el eje Y)
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        // Direcci�n de movimiento: `W` mueve hacia el fondo (alej�ndose de la c�mara)
        // y `S` mueve hacia la c�mara (acerc�ndose a ella)
        moveDirection = (forward * verticalInput + right * horizontalInput).normalized;
    }

    void FixedUpdate()
    {
        // Movimiento del personaje
        if (moveDirection != Vector3.zero)
        {
            // Mover al personaje en la direcci�n calculada
            rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);

            // Rotaci�n: si el personaje se mueve lateralmente, mirar� en esa direcci�n.
            if (moveDirection.x != 0)
            {
                // Si el personaje se mueve en el eje X (izquierda/derecha)
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);

                // Aqu� corregimos la rotaci�n para que el personaje mire en la direcci�n correcta
                // (invertimos 90 grados en el eje Y debido a la orientaci�n del modelo)
                rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation * Quaternion.Euler(0, 90, 0), 0.15f);
            }
            else if (moveDirection.z != 0)
            {
                // Si se mueve en el eje Z (arriba/abajo), el personaje debe mirar hacia el fondo o la c�mara
                if (moveDirection.z > 0) // Movi�ndose hacia la c�mara
                {
                    // Hacer que el personaje mire hacia la c�mara
                    Quaternion targetRotation = Quaternion.LookRotation(-cameraTransform.forward, Vector3.up);
                    rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation * Quaternion.Euler(0, 90, 0), 0.15f);
                }
                else if (moveDirection.z < 0) // Movi�ndose hacia el fondo
                {
                    // Hacer que el personaje mire hacia el fondo (opuesto a la c�mara)
                    Quaternion targetRotation = Quaternion.LookRotation(cameraTransform.forward, Vector3.up);
                    rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation * Quaternion.Euler(0, 90, 0), 0.15f);
                }
            }
        }
    }
}
