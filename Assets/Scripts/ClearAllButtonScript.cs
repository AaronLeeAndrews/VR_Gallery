using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClearAllButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text buttonText, theX;
    public Color regularColor;
    public Color highlightColor;

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = highlightColor;
        theX.color = highlightColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.color = regularColor;
        theX.color = regularColor;
    }
}
