using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle1 : MonoBehaviour
{
    public string puzzle;
    public GameObject textDisplay;

    public void DisplayPuzzle()
    {
        puzzle = "Hint: the tool box is hidden within an environment where leaves may coexist to form one entity.";
        textDisplay.GetComponent<Text>().text = puzzle;
    }
}
