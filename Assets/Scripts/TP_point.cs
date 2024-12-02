using UnityEngine;

public class TP_point : MonoBehaviour
{
    public Transform posicionDestino; // La posición exacta a la que se teletransportará el jugador
    private TP_manager gestor;   // Referencia al TP_manager

    private bool isPlayerInRange = false; // Verifica si el jugador está dentro del área de activación

    private void Start()
    {
        gestor = FindObjectOfType<TP_manager>();

        if (gestor == null)
        {
            UnityEngine.Debug.LogError("No se ha encontrado el TP_manager en la escena.");
        }
    }

    private void Update()
    {
        // Activamos el teletransporte si el jugador está dentro del área y presiona la tecla de acción
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            TeleportTo();
        }
    }

    public void TeleportTo()
    {
        if (gestor != null)
        {
            // Teletransportamos al jugador a la nueva posición
            gestor.Teletransportar(posicionDestino.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;  // El jugador entra en el área de activación
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;  // El jugador sale del área de activación
        }
    }
}
