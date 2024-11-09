using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movimiento : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento
    public Transform cameraTransform; // Transform de la c�mara para orientar el movimiento
    private Rigidbody rb;
    private Vector3 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Evita que el Rigidbody gire al moverse

        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform; // Asigna la c�mara principal si no se ha configurado
        }
    }

    void Update()
    {
        // Obtener input del jugador
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calcular direcci�n en funci�n de la c�mara
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Ignorar la inclinaci�n de la c�mara en el eje Y
        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        // Determinar la direcci�n de movimiento
        moveDirection = (forward * verticalInput + right * horizontalInput).normalized;
    }

    void FixedUpdate()
    {
        // Movimiento del robot
        if (moveDirection != Vector3.zero)
        {
            rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
            // Rotar el robot hacia la direcci�n del movimiento
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, 0.15f);
        }
    }
}