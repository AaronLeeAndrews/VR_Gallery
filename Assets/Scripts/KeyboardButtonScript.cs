using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class KeyboardButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject textSearchBox;
    public Color highlightColor;
    public char buttonChar;

    public void Start()
    {
        buttonChar = gameObject.GetComponentInChildren<Text>().text[0];
    }

    public void Awake()
    {
        gameObject.GetComponentInChildren<Text>().color = Color.white;
    }

    public void AddCharToTextSearch()
    {
        textSearchBox.GetComponent<TextSearchTextboxScript>().AddChar(buttonChar);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.GetComponentInChildren<Text>().color = highlightColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.GetComponentInChildren<Text>().color = Color.white;
    }
}
