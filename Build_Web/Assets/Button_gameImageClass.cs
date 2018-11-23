using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Button_gameImageClass : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string fileDir;
    public GameObject gameViewPtr;
    public GameObject gamePreviewPtr;
    public string[] filters;
    string gameIndex;
    public Image imgPtr, img_Test;
    public Sprite spr_Test;
    int normalWidth = 100, normalHeight = 55;
    int hoverWidth = 120, hoverHeight = 65;
    public Texture2D tex2d_Test;
    public UnityEngine.Video.VideoClip vid;

    // Use this for initialization
    void Start()
    {
        imgPtr = GetComponent<Image>();
    }

    public void OpenPreview()
    {
        gamePreviewPtr.GetComponent<GamePreview>().FillValuesOfNameAndViewType(fileDir, gameViewPtr);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, hoverWidth);
        gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, hoverHeight);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, normalWidth);
        gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, normalHeight);
    }

    public void FillValues(string gameName, GameObject gamePreviewObject, GameObject gameViewObject)
    {
        gameViewPtr = gameViewObject;
        gamePreviewPtr = gamePreviewObject;
        fileDir = gameName;
        filters = System.IO.File.ReadAllLines(gameName + "/Tags.txt");
        tex2d_Test = ((Resources.Load(gameName.Substring(19) + "/Banner", typeof(Texture2D))) as Texture2D);
        spr_Test = Sprite.Create(tex2d_Test, new Rect(0, 0, tex2d_Test.width, tex2d_Test.height), new Vector2(0, 0));

        imgPtr.sprite = spr_Test;
    }
}
