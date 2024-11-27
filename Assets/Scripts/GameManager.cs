using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject menuPausaCanvas; 
    private bool juegoEnPausa = false; 

    public bool isInPuzzle = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; 
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoEnPausa)
            {
                AudioManager.instance.PlaySoundEffect(2);
                ReanudarJuego();
            }
            else
            {
                AudioManager.instance.PlaySoundEffect(2);
                PausarJuego();
            }
        }
    }

    public void PausarJuego()
    {
        juegoEnPausa = true;
        Time.timeScale = 0f; 
        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true; 
        menuPausaCanvas.SetActive(true); 
    }

    public void ReanudarJuego()
    {
        if (isInPuzzle)
        {
            juegoEnPausa = false;
            Time.timeScale = 1f; 
            Cursor.lockState = CursorLockMode.None; 
            Cursor.visible = false; 
            menuPausaCanvas.SetActive(false); 
        }
        else
        {
            juegoEnPausa = false;
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked; 
            Cursor.visible = false; 
            menuPausaCanvas.SetActive(false); 
        }



        
    }
}

