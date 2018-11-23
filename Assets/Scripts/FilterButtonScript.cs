using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class FilterButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Text buttonText;
    public Color onColor, offColor;
    public GameObject backgroundHighlight;
    public Sprite regSprite, highlightSprite;

    public void ToggleTextColor()
    {
        if (gameObject.GetComponent<Toggle>().isOn)
        {
            buttonText.color = onColor;
        }
        else
        {
            buttonText.color = offColor;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        backgroundHighlight.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        backgroundHighlight.gameObject.SetActive(false);
    }
}
