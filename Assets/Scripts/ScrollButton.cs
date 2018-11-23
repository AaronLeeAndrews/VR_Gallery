using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HorizScrollButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public float scrollAddValue;
    public Scrollbar scrollbar;
    public Sprite baseImage, highlightImage;

    public void AddValueToScrollbar()
    {
        scrollbar.value += scrollAddValue;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().sprite = highlightImage;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().sprite = baseImage;
    }
}
