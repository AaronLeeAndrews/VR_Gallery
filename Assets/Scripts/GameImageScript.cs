using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameImageScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{

    float normalWidth = 195f, normalHeight = 108f;
    float hoverWidth = 215f, hoverHeight = 128f;
    
    public int gameDataIdx;
    public Image imgPtr;
    public GameObject gamePreviewPtr;
    public GameObject backgroundHighlight;
    public GameObject dataManagerGameObject;
    public GameObject parentGameViewPtr;

    //OnPointerDown is also required to receive OnPointerUp callbacks
    public void OnPointerDown(PointerEventData eventData)
    {
        // Is empty, but does need to be here for OnPointerUp to work!
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Point up");
        string fileDir = dataManagerGameObject.GetComponent<DataManager>().FileDirOfGameIdx(gameDataIdx);
        gamePreviewPtr.GetComponent<GamePreview>().FillValuesOfNameAndViewType(fileDir, parentGameViewPtr);
        parentGameViewPtr.gameObject.SetActive(false);

        imgPtr.gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, normalWidth);
        imgPtr.gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, normalHeight);

        backgroundHighlight.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        imgPtr.gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, hoverWidth);
        imgPtr.gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, hoverHeight);

        backgroundHighlight.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        imgPtr.gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, normalWidth);
        imgPtr.gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, normalHeight);

        backgroundHighlight.gameObject.SetActive(false);
    }

    public void UpdateImageAndIndexWithGameIndex(int gameIdx)
    {
        gameDataIdx = gameIdx;
        imgPtr.sprite = dataManagerGameObject.GetComponent<DataManager>().SpriteOfGameIdx(gameIdx);
    }
}
