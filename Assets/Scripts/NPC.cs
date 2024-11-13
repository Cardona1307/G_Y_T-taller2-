using UnityEngine;

public class NPC : MonoBehaviour
{
    public string[] objetosRequeridos = new string[] { "Juguete1", "Juguete2", "Juguete3", "Juguete4", "Juguete5" }; // Los 5 juguetes requeridos
    private bool enRango = false; // Verifica si el jugador está en rango para interactuar

    private Inventario inventarioJugador;

    // Se ejecuta cuando el jugador entra en el área del trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enRango = true;
            inventarioJugador = other.GetComponent<Inventario>();
        }
    }

    // Se ejecuta cuando el jugador sale del área del trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enRango = false;
            inventarioJugador = null;
        }
    }

    // Se ejecuta cada cuadro
    private void Update()
    {
        // Verifica si el jugador está en rango y presiona la tecla E para interactuar
        if (enRango && Input.GetKeyDown(KeyCode.E))
        {
            // Verifica si el jugador tiene los 5 objetos requeridos en el inventario
            if (inventarioJugador != null && TieneObjetosSuficientes())
            {
                // El jugador ha cumplido con la condición, se puede otorgar un objeto
                OtorgarRecompensa();
            }
            else
            {
                UnityEngine.Debug.Log("No tienes todos los juguetes necesarios.");
            }
        }
    }

    // Verifica si el jugador tiene todos los objetos requeridos en el inventario
    private bool TieneObjetosSuficientes()
    {
        foreach (string objetoRequerido in objetosRequeridos)
        {
            if (!inventarioJugador.TieneObjeto(objetoRequerido))
            {
                return false; // Si falta alguno de los objetos requeridos, no se puede interactuar
            }
        }
        return true; // Si tiene todos los objetos requeridos
    }

    // Otorga la recompensa al jugador
    private void OtorgarRecompensa()
    {
        // Elimina todos los objetos del inventario antes de agregar la recompensa
        inventarioJugador.LimpiarInventario();

        // Lógica para entregar la recompensa al jugador (agregar una pieza o algo similar)
        inventarioJugador.AgregarObjeto("piezaRecompensa");
        UnityEngine.Debug.Log("¡Has recibido una pieza de recompensa!");

        // Aquí podrías hacer que el NPC cambie o se desactive, o mostrar algún mensaje
        gameObject.SetActive(false); // Por ejemplo, desactivamos el NPC después de entregar la recompensa
    }
}
