using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayVideoToggle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text buttonText;
    public Color onColor, offColor;
    //public GameObject backgroundHighlight;
    //public Sprite regSprite, highlightSprite;
    public GameObject gamePreviewObject;

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = offColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.color = onColor;
    }

    public void ToggleAction()
    {
        if(gameObject.GetComponent<Toggle>().isOn)
        {
            gamePreviewObject.GetComponent<GamePreview>().PlayVideo();
            buttonText.text = "Stop";
        }
        else
        {
            //gamePreviewObject.GetComponent<GamePreview>().StopVideo();
            buttonText.text = "Play";
        }
    }

    public void ToggleOff()
    {
        gameObject.GetComponent<Toggle>().isOn = false;
        buttonText.text = "Play";
        gamePreviewObject.GetComponent<GamePreview>().StopVideo();
    }
}
