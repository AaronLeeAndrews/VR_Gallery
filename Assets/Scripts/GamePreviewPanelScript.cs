using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GamePreviewPanelScript : MonoBehaviour {
    
    public GameObject dataManagerGameObject;
    public GameObject upArrow, downArrow;
    public List<GameObject> listOfGameImages;

    public List<int> listOfGameIndices;
    private int currentStepInIdxList = 0;

    public void UpdateGameIdxList(List<int> newGameIdxList)
    {
        listOfGameIndices = new List<int>(newGameIdxList);

        // Set the curr step and the arrows back to the starting state
        currentStepInIdxList = 0;
        upArrow.SetActive(false);
        if (listOfGameIndices.Count <= listOfGameImages.Count)
            downArrow.SetActive(false);
        else
            downArrow.SetActive(true);

        RefreshPreviewImages();
    }

    public void RefreshPreviewImages()
    {
        for(int ii=0; ii<listOfGameImages.Count; ++ii)
        {
            if(ii + currentStepInIdxList < listOfGameIndices.Count)
            {
                listOfGameImages[ii].SetActive(true);
                listOfGameImages[ii].GetComponent<GameImageScript>().UpdateImageAndIndexWithGameIndex(listOfGameIndices[ii+currentStepInIdxList]);
            }
            else
                listOfGameImages[ii].SetActive(false);
        }
    }

    public void IncCurrStepInIdxList()
    {
        if (currentStepInIdxList + listOfGameImages.Count < listOfGameIndices.Count)
            currentStepInIdxList += listOfGameImages.Count;
        else
            return;

        if (!upArrow.gameObject.activeSelf && listOfGameIndices.Count >= listOfGameImages.Count)
            upArrow.SetActive(true);

        if (currentStepInIdxList + listOfGameImages.Count >= listOfGameIndices.Count)
            downArrow.SetActive(false);

        RefreshPreviewImages();
    }

    public void DecCurrStepInIdxList()
    {
        if (currentStepInIdxList > 0)
            currentStepInIdxList -= listOfGameImages.Count;
        else
            return;

        if (!downArrow.gameObject.activeSelf && currentStepInIdxList + listOfGameImages.Count < listOfGameIndices.Count)
            downArrow.SetActive(true);

        if (currentStepInIdxList == 0)
            upArrow.SetActive(false);

        RefreshPreviewImages();
    }
}
