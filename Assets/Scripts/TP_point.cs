using UnityEngine;

public class TP_point : MonoBehaviour
{
    public Vector3 posicionDestino; // Posici�n a la que teletransportar�
    private TP_manager gestor; // Cambiado de GestorTeletransportes a TP_manager

    private void Start()
    {
        gestor = FindObjectOfType<TP_manager>(); // Encuentra el gestor en la escena
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E)) // Verifica que sea el jugador y que presione 'E'
        {
            gestor.Teletransportar(posicionDestino);
        }
    }
}
