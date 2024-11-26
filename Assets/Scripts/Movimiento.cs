using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movimiento : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public Transform cameraTransform;
    private Rigidbody rb;
    private Vector3 moveDirection;

    private bool isGrounded = false;

    // Referencia al Animator
    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        // Verificar que el Animator está asignado
        if (animator == null)
        {
            UnityEngine.Debug.Log("Animator no asignado en el Inspector.");
        }
    }

    void Update()
    {
        // Capturar el input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calcular direcciones
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        moveDirection = (forward * verticalInput + right * horizontalInput).normalized;

        // Calcular la magnitud del movimiento para el Animator
        float speed = moveDirection.magnitude; // Magnitud de la dirección de movimiento

        // Actualizar el parámetro "Speed" en el Animator
        if (animator != null)
        {
            animator.SetFloat("Speed", speed);
        }

        // Salto
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        if (moveDirection != Vector3.zero)
        {
            // Mover el jugador
            rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);

            // Rotar dependiendo de la dirección en el eje X o Z
            if (moveDirection.x != 0)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation * Quaternion.Euler(0, 90, 0), 0.15f);
            }
            else if (moveDirection.z != 0)
            {
                if (moveDirection.z > 0)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(-cameraTransform.forward, Vector3.up);
                    rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation * Quaternion.Euler(0, 90, 0), 0.15f);
                }
                else if (moveDirection.z < 0)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(cameraTransform.forward, Vector3.up);
                    rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation * Quaternion.Euler(0, 90, 0), 0.15f);
                }
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            isGrounded = false;
        }
    }
}
