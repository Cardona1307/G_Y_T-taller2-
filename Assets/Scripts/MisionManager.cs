using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public IMision misionActiva; // Misi�n activa en el juego

    public bool MisionCompletada()
    {
        return misionActiva != null && misionActiva.EstaCompletada();
    }

    public void IniciarMision(IMision mision)
    {
        misionActiva = mision; // Asignamos la misi�n activa
        mision.CompletarMision(); // Activamos la misi�n
        Debug.Log("Misi�n iniciada: " + mision.ObtenerDescripcion());
    }

    public void IntentarCompletarMision()
    {
        if (misionActiva != null && !misionActiva.EstaCompletada())
        {
            misionActiva.IntentarCompletarMision();
            if (misionActiva.EstaCompletada())
            {
                Debug.Log("�Misi�n completada!");
            }
        }
        else
        {
            Debug.Log("No hay misi�n activa o la misi�n ya est� completada.");
        }
    }
}
