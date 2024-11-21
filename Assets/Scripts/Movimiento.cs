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

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        moveDirection = (forward * verticalInput + right * horizontalInput).normalized;

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        if (moveDirection != Vector3.zero)
        {
            rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);

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
