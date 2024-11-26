using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movimiento : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public Transform cameraTransform;
    private Rigidbody rb;
    private Vector3 moveDirection;

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
