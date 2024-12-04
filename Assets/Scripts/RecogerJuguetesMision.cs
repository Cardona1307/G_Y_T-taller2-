using System.Collections.Generic;
using UnityEngine;

public class RecogerJuguetesMision : MonoBehaviour, IMision
{
    public string[] objetosRequeridos = { "Juguete1", "Juguete2", "Juguete3", "Juguete4", "Juguete5" };
    private Inventario inventarioJugador;
    private bool misionCompletada = false;

    // Descripción de la misión
    public string ObtenerDescripcion()
    {
        return "Recoge todos los juguetes y entrégalos al NPC.";
    }

    // Verifica si la misión está completada
    public bool EstaCompletada()
    {
        return misionCompletada;
    }

    // Intenta completar la misión si el jugador tiene todos los objetos
    public void IntentarCompletarMision()
    {
        if (inventarioJugador != null && TieneObjetosSuficientes())
        {
            inventarioJugador.LimpiarInventario(); // Limpiar el inventario al completar la misión
            inventarioJugador.AgregarObjeto("piezaRecompensa"); // Dar recompensa
            CompletarMision();
        }
        else
        {
            Debug.Log("No tienes todos los juguetes necesarios.");
        }
    }

    // Completar la misión manualmente
    public void CompletarMision()
    {
        misionCompletada = true;
        Debug.Log("¡Has completado la misión de recoger juguetes!");
    }

    private bool TieneObjetosSuficientes()
    {
        foreach (string objeto in objetosRequeridos)
        {
            if (!inventarioJugador.TieneObjeto(objeto))
            {
                return false;
            }
        }
        return true;
    }

    // Asignación del inventario del jugador cuando entra en el área del NPC
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inventarioJugador = other.GetComponent<Inventario>();
        }
    }

    // Eliminar inventario cuando el jugador sale del área del NPC
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inventarioJugador = null;
        }
    }
}
