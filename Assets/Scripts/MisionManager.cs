using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public IMision misionActiva; // Misión activa en el juego

    public bool MisionCompletada()
    {
        return misionActiva != null && misionActiva.EstaCompletada();
    }

    public void IniciarMision(IMision mision)
    {
        misionActiva = mision; // Asignamos la misión activa
        mision.CompletarMision(); // Activamos la misión
        Debug.Log("Misión iniciada: " + mision.ObtenerDescripcion());
    }

    public void IntentarCompletarMision()
    {
        if (misionActiva != null && !misionActiva.EstaCompletada())
        {
            misionActiva.IntentarCompletarMision();
            if (misionActiva.EstaCompletada())
            {
                Debug.Log("¡Misión completada!");
            }
        }
        else
        {
            Debug.Log("No hay misión activa o la misión ya está completada.");
        }
    }
}
