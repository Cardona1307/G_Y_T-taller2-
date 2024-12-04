using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject interactImage; // Campo p�blico para asignar el objeto de la "E"

    // M�todo para mostrar/ocultar la imagen de interacci�n
    public void MostrarInteraccion(bool mostrar)
    {
        if (interactImage != null)
        {
            interactImage.SetActive(mostrar);
        }
        else
        {
            Debug.LogError("El objeto Interact Image no est� asignado en el UIManager.");
        }
    }
}
