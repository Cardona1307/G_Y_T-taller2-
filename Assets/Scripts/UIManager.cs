using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject interactKey; // Asigna el ícono o imagen en el Inspector

    public void MostrarInteractKey(bool mostrar)
    {
        if (interactKey != null)
        {
            interactKey.SetActive(mostrar); // Activa o desactiva la imagen según el parámetro
        }
        else
        {
            Debug.LogError("InteractKey no está asignado en el UIManager.");
        }
    }
}
