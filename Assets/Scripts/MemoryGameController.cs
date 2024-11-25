using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class MemoryGameController : MonoBehaviour
{
    [SerializeField]
    private Sprite bgImage;

    public Sprite[] puzzles;
    public List<Sprite> gamePuzzle = new List<Sprite>();

    public List<Button> btns = new List<Button>();

    private bool firstGuess, secondGuess;

    private int countGuesses;
    private int countCorrectGuesses;
    private int gameGuesses;

    private string firstGuessPuzzle, secondGuessPuzzle;

    private int firstGuessIndex, secondGuessIndex;

    void Awake()
    {
        puzzles = Resources.LoadAll<Sprite>("Sprites/MemoryCards");
    }

    void Start()
    {
        AssignBackgrounds();
        AddListeners();
        AddGamePuzzle();
        Shuffle(gamePuzzle);
        gameGuesses = gamePuzzle.Count / 2;
    }

    public void AddButton(Button button)
    {
        if (button != null && !btns.Contains(button))
        {
            btns.Add(button);
            button.image.sprite = bgImage;
        }
    }

    private void OnEnable()
    {
        AssignBackgrounds();
    }

    void AssignBackgrounds()
    {
        foreach (var btn in btns)
        {
            if (btn != null)
            {
                btn.image.sprite = bgImage;
            }
        }
    }

    void AddGamePuzzle()
    {
        if (puzzles.Length == 0)
        {
            Debug.LogError("No puzzles found in Resources/MemoryCards!");
            return;
        }

        int looper = btns.Count;
        int index = 0;

        for (int i = 0; i < looper; i++)
        {
            if (index >= puzzles.Length)
            {
                index = 0; // Reinicia el índice si supera el número de sprites disponibles
            }

            gamePuzzle.Add(puzzles[index]);
            index++;
        }
    }

    void AddListeners()
    {
        foreach (Button btn in btns)
        {
            btn.onClick.AddListener(() => PickAPuzzle());
        }
    }

    public void PickAPuzzle()
    {
        if (!firstGuess)
        {
            firstGuess = true;
            firstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

            firstGuessPuzzle = gamePuzzle[firstGuessIndex].name;

            btns[firstGuessIndex].image.sprite = gamePuzzle[firstGuessIndex];
        }
        else if (!secondGuess)
        {
            secondGuess = true;
            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

            secondGuessPuzzle = gamePuzzle[secondGuessIndex].name;

            btns[secondGuessIndex].image.sprite = gamePuzzle[secondGuessIndex];

            countGuesses++;

            StartCoroutine(CheckIfThePuzzlesMatch());
        }
    }

    IEnumerator CheckIfThePuzzlesMatch()
    {
        yield return new WaitForSeconds(0.5f);

        if (firstGuessPuzzle == secondGuessPuzzle)
        {
            yield return new WaitForSeconds(0.5f);

            btns[firstGuessIndex].interactable = false;
            btns[secondGuessIndex].interactable = false;

            btns[firstGuessIndex].image.color = new Color(0, 0, 0, 0);
            btns[secondGuessIndex].image.color = new Color(0, 0, 0, 0);

            CheckIfTheGameIsFinished();
        }
        else
        {
            yield return new WaitForSeconds(0.5f);

            btns[firstGuessIndex].image.sprite = bgImage;
            btns[secondGuessIndex].image.sprite = bgImage;
        }

        firstGuess = secondGuess = false;
    }

    void CheckIfTheGameIsFinished()
    {
        countCorrectGuesses++;
        if (countCorrectGuesses >= gameGuesses)
        {
            Debug.Log("Game Finished");
            Debug.Log("It took you " + countGuesses + " guess(es) to finish the game");
        }
    }

    void Shuffle(List<Sprite> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            // Generar un índice aleatorio
            int randomIndex = Random.Range(i, list.Count);

            // Intercambiar los elementos en las posiciones `i` y `randomIndex`
            Sprite temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }


}
