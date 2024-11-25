using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryGameActivation : MonoBehaviour
{
    public GameObject canvas; // Arrastra el Canvas desde el Inspector
    public KeyCode interactKey = KeyCode.E; // Tecla para interactuar
    private bool isPlayerNearby = false; // Verifica si el jugador está cerca
    private bool isCanvasActive = false; // Verifica si el Canvas está activo

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(interactKey))
        {
            // Alternar visibilidad del Canvas
            isCanvasActive = !canvas.activeSelf;
            canvas.SetActive(isCanvasActive);

            // Alternar visibilidad del cursor
            Cursor.visible = isCanvasActive;
            Cursor.lockState = isCanvasActive ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Asegúrate de que el jugador tenga la etiqueta "Player"
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;

            // Asegúrate de cerrar el Canvas y ocultar el cursor al salir
            if (isCanvasActive)
            {
                isCanvasActive = false;
                canvas.SetActive(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
