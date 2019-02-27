using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TextSearchTextboxScript : MonoBehaviour
{
    public Text textObject;
    public GameObject resultsPanel;
    public bool noEnteredText;
    public Button clearTextButton;

    // Start is called before the first frame update
    public void Awake()
    {
        textObject.text = "Enter your search";
        noEnteredText = true;
    }

    public void AddChar(char newChar)
    {
        if (noEnteredText)
        {
            textObject.text = "";
            noEnteredText = false;
            clearTextButton.gameObject.SetActive(true);
        }

        textObject.text += newChar;

        resultsPanel.GetComponent<ResultsPanelScript>().UpdateListOfGamesAfterAddingChar();
    }

    public void DeleteChar()
    {
        if (noEnteredText)
            return;

        textObject.text = textObject.text.Substring(0, textObject.text.Length-1);

        if (textObject.text.Length == 0)
        {
            ClearText();
        }

        resultsPanel.GetComponent<ResultsPanelScript>().UpdateListOfGamesAfterDeletingChar();
    }

    public void ClearText()
    {
        noEnteredText = true;
        textObject.text = "Enter your search";
        clearTextButton.gameObject.SetActive(false);
    }
}
