using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class categoryPanelScript : MonoBehaviour {

    public string fileDir;
    public GameObject gameViewPtr;
    public GameObject gamePreviewPtr;
    public RectTransform contentArea;
    public Text textName;
    public GameObject buttonImagePrefab;
    public Scrollbar scrollBar;
    public float xOuterOffset, xInnerOffset, yOffset;

    float xLogoSize = 80f, yLogoSize = 90f;
    List<string> gameDirArr;

    public void Start()
    {
        gameDirArr = new List<string>();
    }

    public void FillValues(string filterName, List<string> stringArr, GameObject gamePreviewObject, GameObject gameViewObject)
    {
        textName.text = filterName;

        gameViewPtr = gameViewObject;
        gamePreviewPtr = gamePreviewObject;

        for(int ii=0; ii< stringArr.Count; ++ii)
        {
            var item = Instantiate(buttonImagePrefab);
            item.transform.SetParent(contentArea);
            yLogoSize = item.GetComponent<RectTransform>().rect.height;
            item.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
            item.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
            item.GetComponent<RectTransform>().sizeDelta = new Vector2(xLogoSize, yLogoSize);
            item.GetComponent<RectTransform>().localPosition = new Vector3(xOuterOffset + (xInnerOffset + xLogoSize) * ii, yOffset, 0);
            item.GetComponent<Button_gameImageClass>().FillValues(stringArr[ii], gamePreviewObject, gameViewObject);
        }
        
        scrollBar.GetComponent<GameCategoryScrollScript>().numberOfItems = stringArr.Count;
        scrollBar.GetComponent<GameCategoryScrollScript>().xOffset = xInnerOffset;
        scrollBar.GetComponent<GameCategoryScrollScript>().xWidth = xLogoSize;
        scrollBar.GetComponent<GameCategoryScrollScript>().SetValues();
    }


}
