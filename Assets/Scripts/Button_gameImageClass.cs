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
    public GameObject backgroundHighlight;
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
        //imgPtr = GetComponent<Image>();
    }

    public void OpenPreview()
    {
        gamePreviewPtr.GetComponent<GamePreview>().FillValuesOfNameAndViewType(fileDir, gameViewPtr);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        imgPtr.gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, hoverWidth);
        imgPtr.gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, hoverHeight);
        transform.SetAsLastSibling();

        backgroundHighlight.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        imgPtr.gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, normalWidth);
        imgPtr.gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, normalHeight);
        //Vector3 pos = gameObject.GetComponent<RectTransform>().localPosition;
        //gameObject.GetComponent<RectTransform>().localPosition = new Vector3(pos.x, pos.y, 0);

        backgroundHighlight.gameObject.SetActive(false);
    }

    public void FillValues(string gameDirectory, GameObject gamePreviewObject, GameObject gameViewObject)
    {
        imgPtr.sprite = null;
        gameViewPtr = gameViewObject;
        gamePreviewPtr = gamePreviewObject;
        fileDir = gameDirectory;

        tex2d_Test = Resources.Load<Texture2D>(gameDirectory + "/Banner");
        //imgPtr.sprite = Resources.Load(gameDirectory + "/Banner", typeof(Sprite)) as Sprite;
        spr_Test = Sprite.Create(tex2d_Test, new Rect(0, 0, tex2d_Test.width, tex2d_Test.height), new Vector2(0, 0));
        imgPtr.sprite = spr_Test;

        string filtersOnOneLine = Resources.Load<TextAsset>(gameDirectory + "/Tags").text;
        filters = filtersOnOneLine.Split(new string[] { "\r\n" }, System.StringSplitOptions.RemoveEmptyEntries);
    }
}
