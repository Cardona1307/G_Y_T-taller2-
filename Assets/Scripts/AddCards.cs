using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCards : MonoBehaviour
{
    [SerializeField]
    private Transform puzzleField;

    [SerializeField]
    private GameObject btn;

    private MemoryGameController memoryGameController;

    void Awake()
    {
        memoryGameController = FindObjectOfType<MemoryGameController>(); // Encuentra el controlador de memoria

        for (int i = 0; i < 12; i++)
        {
            GameObject button = Instantiate(btn);
            button.name = "" + i;
            button.transform.SetParent(puzzleField, false);

            // Notifica al controlador de memoria
            if (memoryGameController != null)
            {
                memoryGameController.AddButton(button.GetComponent<UnityEngine.UI.Button>());
            }
        }
    }
}
