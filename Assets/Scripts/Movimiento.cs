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

    [Header("Interaction")] public KeyCode interactionKey = KeyCode.E;
    
    private bool isGrounded = false;

    // Referencia al Animator
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
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

        // Actualizar el par�metro "Speed" en el Animator
        
        _animator.SetFloat("Speed", speed);
        

        // Salto
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        
        ActionAnimation();
    }

    void ActionAnimation()
    {
        if(Input.GetKeyDown(interactionKey))
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

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            isGrounded = true;
        }
    }

    //Actions
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PuzzleTrigger"))
        {
            PuzzleActivator puzzleActivator = other.GetComponent<PuzzleActivator>();
            
            if (Input.GetKeyDown(interactionKey))
            {
                puzzleActivator.ActivarMinijuego();
            }
        }else if (other.CompareTag("Teleport"))
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
