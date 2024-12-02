using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movimiento : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    private float stepTimer = 0f;
    public float stepInterval = 0.5f; // Intervalo entre pasos
    public Transform cameraTransform;
    private Rigidbody rb;
    private Vector3 moveDirection;
    private AudioManager audioManager;

    [Header("Interaction")]
    public KeyCode interactionKey = KeyCode.E;

    private bool isGrounded = false;

    // Referencia al Animator
    private Animator _animator;

    // Referencia a la rata
    
    public Transform rataTransform;
    [SerializeField]
    private Animator rataAnimator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        if (rataTransform != null)
        {
            rataAnimator = rataTransform.GetComponent<Animator>();  // Obtener el Animator de la rata
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        audioManager = AudioManager.instance; // Obtiene la instancia singleton del AudioManager

        // Verificar que el Animator est� asignado
        if (_animator != null)
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
        float speed = moveDirection.magnitude; // Magnitud de la direcci�n de movimiento

        // Actualizar el par�metro "Speed" en el Animator del robot
        _animator.SetFloat("Speed", speed);

        // Actualizar el parámetro "Speed" en el Animator de la rata
        if (rataAnimator != null)
        {
            rataAnimator.SetFloat("Speed", speed);
        }

        // Salto
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        ActionAnimation();
    }

    void ActionAnimation()
    {
        if (Input.GetKeyDown(interactionKey))
            _animator.SetBool("Action", true);
        if (Input.GetKeyUp(interactionKey))
        {
            _animator.SetBool("Action", false);
        }
    }

    void FixedUpdate()
    {
        if (moveDirection != Vector3.zero)
        {
            // Mover el jugador
            rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);

            // No modificamos la rotación del robot, mantenemos el control original que ya tienes:
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation * Quaternion.Euler(0, 0, 0), 0.15f);

            // Temporizador para sonidos de pasos
            stepTimer -= Time.fixedDeltaTime;

            if (stepTimer <= 0f)
            {
                // Reproducir sonido de pasos desde el AudioManager
                audioManager.PlayRobotStepSound();
                stepTimer = stepInterval; // Reinicia el temporizador
            }
        }
        else
        {
            stepTimer = 0f; // Resetea el temporizador si no se está moviendo
        }

        // Mover la rata al lado del robot
        if (rataTransform != null)
        {
            // La rata se mueve al lado del robot, manteniendo una distancia constante
            Vector3 targetPosition = transform.position + transform.right * 2f; // 2 unidades a la derecha del robot
            rataTransform.position = Vector3.Lerp(rataTransform.position, targetPosition, Time.fixedDeltaTime * 5f);

            // Hacer que la rata rote con la misma lógica que el robot
            if (moveDirection != Vector3.zero)
            {
                Quaternion rataRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                rataTransform.rotation = Quaternion.Slerp(rataTransform.rotation, rataRotation, Time.fixedDeltaTime * 5f);
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

    // Actions

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PuzzleTrigger"))
        {
            PuzzleActivator puzzleActivator = other.GetComponent<PuzzleActivator>();

            if (Input.GetKeyDown(interactionKey))
            {
                puzzleActivator.ActivarMinijuego();
            }
        }
        else if (other.CompareTag("Teleport"))
        {
            TP_point teleportActivator = other.GetComponent<TP_point>();

            if (Input.GetKeyDown(interactionKey))
            {
                teleportActivator.TeleportTo();
            }
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
