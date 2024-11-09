using UnityEngine;

public class ObjetoRecogible : MonoBehaviour
{
    public string nombreObjeto; // Nombre del objeto para identificarlo en el inventario
    private bool enRango = false; // Verifica si el jugador está en rango
    private Inventario inventarioJugador;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enRango = true;
            inventarioJugador = other.GetComponent<Inventario>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enRango = false;
            inventarioJugador = null;
        }
    }

    private void Update()
    {
        if (enRango && Input.GetKeyDown(KeyCode.E))
        {
            if (inventarioJugador != null)
            {
                inventarioJugador.AgregarObjeto(nombreObjeto);
                Destroy(gameObject);
            }
        }
    }
}