using UnityEngine;

public class TP_point : MonoBehaviour
{
    public Transform posicionDestino; // La posici�n exacta a la que se teletransportar� el jugador
    private TP_manager gestor;   // Referencia al TP_manager

    private bool isPlayerInRange = false; // Verifica si el jugador est� dentro del �rea de activaci�n

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
        // Activamos el teletransporte si el jugador est� dentro del �rea y presiona la tecla de acci�n
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            TeleportTo();
        }
    }

    public void TeleportTo()
    {
        if (gestor != null)
        {
            // Teletransportamos al jugador a la nueva posici�n
            gestor.Teletransportar(posicionDestino.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;  // El jugador entra en el �rea de activaci�n
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;  // El jugador sale del �rea de activaci�n
        }
    }
}
