using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject interactKey; // Asigna el �cono o imagen en el Inspector

    public void MostrarInteractKey(bool mostrar)
    {
        if (interactKey != null)
        {
            interactKey.SetActive(mostrar); // Activa o desactiva la imagen seg�n el par�metro
        }
        else
        {
            Debug.LogError("InteractKey no est� asignado en el UIManager.");
        }
    }
}
