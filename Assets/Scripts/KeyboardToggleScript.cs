using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class KeyboardToggleScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject letterKeys;
    public GameObject symbolKeys;
    public Color highlightColor;

    void Awake()
    {
        gameObject.GetComponent<Toggle>().isOn = false;
        letterKeys.gameObject.SetActive(true);
        symbolKeys.gameObject.SetActive(false);
    }

    public void ToggleChanged()
    {
        if(gameObject.GetComponent<Toggle>().isOn)
        {
            letterKeys.gameObject.SetActive(false);
            symbolKeys.gameObject.SetActive(true);
        }
        else
        {
            letterKeys.gameObject.SetActive(true);
            symbolKeys.gameObject.SetActive(false);
        }
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
