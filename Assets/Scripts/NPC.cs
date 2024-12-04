using UnityEngine;

public class RecogerJuguetesMision : MonoBehaviour, IMision
{
    public string[] objetosRequeridos = { "Juguete1", "Juguete2", "Juguete3", "Juguete4", "Juguete5" };
    private Inventario inventarioJugador;
    private bool misionCompletada = false;

    public string ObtenerDescripcion()
    {
        return "Recoge todos los juguetes y entrégalos al NPC.";
    }

    public bool EstaCompletada()
    {
        return misionCompletada;
    }

    public void CompletarMision()
    {
        misionCompletada = true;
        Debug.Log("¡Has completado la misión de recoger juguetes!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inventarioJugador = other.GetComponent<Inventario>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inventarioJugador = null;
        }
    }

    public void IntentarCompletarMision()
    {
        if (inventarioJugador != null && TieneObjetosSuficientes())
        {
            inventarioJugador.LimpiarInventario();
            inventarioJugador.AgregarObjeto("piezaRecompensa");
            CompletarMision();
        }
        else
        {
            Debug.Log("No tienes todos los juguetes necesarios.");
        }
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
}
