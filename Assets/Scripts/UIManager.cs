using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject interactImage; // Campo público para asignar el objeto de la "E"

    // Método para mostrar/ocultar la imagen de interacción
    public void MostrarInteraccion(bool mostrar)
    {
        if (interactImage != null)
        {
            interactImage.SetActive(mostrar);
        }
        else
        {
            Debug.LogError("El objeto Interact Image no está asignado en el UIManager.");
        }
    }
}
