using UnityEngine;

public class TP_point : MonoBehaviour
{
    public Vector3 posicionDestino;
    private TP_manager gestor; 

    private bool isPlayerinRange; 

    private void Start()
    {
        gestor = FindObjectOfType<TP_manager>(); 
        isPlayerinRange = false;
    }
    public void TeleportTo()
    {
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
