using UnityEngine;

public class TP_point : MonoBehaviour
{
    public Vector3 posicionDestino; // Posición a la que teletransportará
    private TP_manager gestor; // Cambiado de GestorTeletransportes a TP_manager

    private bool isPlayerinRange; //Registra si el jugador está en el collider

    private void Start()
    {
        gestor = FindObjectOfType<TP_manager>(); // Encuentra el gestor en la escena
        isPlayerinRange = false;
    }

    private void Update()
    {
        if (isPlayerinRange && Input.GetKeyDown(KeyCode.E))
            gestor.Teletransportar(posicionDestino);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        isPlayerinRange = true;
        
    }

    private void OnTriggerExit(Collider other)
    {
        isPlayerinRange = false;
    }
}
