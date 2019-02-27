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

    public void OnEnable()
    {
        GetComponent<Image>().color = new Color(1, 1, 1, 0);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().color = new Color(1, 1, 1, 1);
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
