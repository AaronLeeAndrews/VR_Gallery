using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GamePreviewBackButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text buttonText;
    public Color onColor, offColor;
    public GameObject gamePreviewObject;

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = offColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.color = onColor;
    }

    public void OnClickReturnColorToDefault()
    {
        buttonText.color = onColor;
    }
}
