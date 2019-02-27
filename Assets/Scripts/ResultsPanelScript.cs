using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultsPanelScript : MonoBehaviour
{

    public GameObject printedStringResult;
    public GameObject dataManagerGameObject;
    public GameObject upArrow, downArrow;
    public List<GameObject> listOfGameImages;
    public int currentStepInIdxList;
    public List<int> resultsList;
    public Image magImage, magImage_noResults;
    public Button backToGameListButton;
    
    void Awake()
    {
        resultsList.Clear();
        for (int ii = 0; ii<listOfGameImages.Count; ++ii)
            listOfGameImages[ii].SetActive(false);
        magImage.gameObject.SetActive(true);
        magImage_noResults.gameObject.SetActive(false);
        backToGameListButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void UpdateListOfGamesAfterAddingChar()
    {
        currentStepInIdxList = 0;

        if (printedStringResult.GetComponentInChildren<Text>().text.Length == 1)
        {
            //Debug.Log(dataManagerGameObject.GetComponent<DataManager>().GetGameDataListCount());
            // Check every game in library
            for (int ii = 0; ii < dataManagerGameObject.GetComponent<DataManager>().GetGameDataListCount(); ++ii)
            {
                if (dataManagerGameObject.GetComponent<DataManager>().NameOfGameIdx(ii).ToUpper().Contains(printedStringResult.GetComponentInChildren<Text>().text))
                {
                    //Debug.Log(dataManagerGameObject.GetComponent<DataManager>().NameOfGameIdx(ii));
                    resultsList.Add(ii);
                }
            }
        }
        else
        {
            int currResultListCount = resultsList.Count;
            // Check every game in list and filter out ones that don't match anymore
            for (int ii = 0; ii < resultsList.Count;)
            {
                string nameOfGame = dataManagerGameObject.GetComponent<DataManager>().NameOfGameIdx(resultsList[ii]);
                string searchStr = printedStringResult.GetComponentInChildren<Text>().text;

                if (!nameOfGame.ToUpper().Contains(searchStr))
                {
                    resultsList.RemoveAt(ii);
                }
                else
                {
                    ++ii;
                }
            }
        }
        RefreshPreviewImages();
    }

    // Update is called once per frame
    public void UpdateListOfGamesAfterDeletingChar()
    {
        currentStepInIdxList = 0;

        if (printedStringResult.GetComponent<TextSearchTextboxScript>().noEnteredText)
        {
            resultsList.Clear();
        }
        else
        {
            List<int> gamesToAdd = new List<int>();

            // Check every game in library
            for (int ii = 0; ii < dataManagerGameObject.GetComponent<DataManager>().GetGameDataListCount(); ++ii)
            {
                // Use binary search to see if we already have that game
                if (!GameIdxIsInList(ii))
                {
                    string nameOfGame = dataManagerGameObject.GetComponent<DataManager>().NameOfGameIdx(ii);
                    string searchStr = printedStringResult.GetComponentInChildren<Text>().text;

                    if (nameOfGame.ToUpper().Contains(searchStr))
                        gamesToAdd.Add(ii);
                }
            }

            if (gamesToAdd.Count > 0)
            {
                resultsList.AddRange(gamesToAdd);
                resultsList.Sort();
            }
        }
        RefreshPreviewImages();
    }

    private bool GameIdxIsInList(int idx)
    {
        int topVal = resultsList.Count-1;
        int middleVal = resultsList.Count / 2;
        int botVal = 0;

        // Check every game in library
        for (int ii = 0; ii < resultsList.Count; ++ii)
        {
            if (middleVal < resultsList.Count && idx == resultsList[middleVal])
                return true;
            else if (botVal > topVal)
                return false;

            if (idx > resultsList[middleVal])
            {
                botVal = middleVal+1;
            }
            else
            {
                topVal = middleVal-1;
            }
            
            middleVal = ((topVal - botVal) / 2) + botVal;
            //Debug.Log("middleVal: " + middleVal + ", idx: " + idx);
        }
        return false;
    }
    
    public void RefreshPreviewImages()
    {
        for (int ii = 0; ii < listOfGameImages.Count; ++ii)
        {
            if (ii + currentStepInIdxList < resultsList.Count)
            {
                listOfGameImages[ii].SetActive(true);
                listOfGameImages[ii].GetComponent<GameImageScript>().UpdateImageAndIndexWithGameIndex(resultsList[ii + currentStepInIdxList]);
            }
            else
                listOfGameImages[ii].SetActive(false);
        }

        if(resultsList.Count == 0 && !printedStringResult.GetComponent<TextSearchTextboxScript>().noEnteredText)
        {
            magImage_noResults.gameObject.SetActive(true);
            backToGameListButton.gameObject.SetActive(true);
            magImage.gameObject.SetActive(false);
        }
        else
        {
            magImage_noResults.gameObject.SetActive(false);
            backToGameListButton.gameObject.SetActive(false);
            magImage.gameObject.SetActive(true);
        }
    }

    public void IncCurrStepInIdxList()
    {
        if (currentStepInIdxList + listOfGameImages.Count < resultsList.Count)
            currentStepInIdxList += listOfGameImages.Count;
        else
            return;

        if (!upArrow.gameObject.activeSelf && resultsList.Count >= listOfGameImages.Count)
            upArrow.SetActive(true);

        if (currentStepInIdxList + listOfGameImages.Count >= resultsList.Count)
            downArrow.SetActive(false);

        RefreshPreviewImages();
    }

    public void DecCurrStepInIdxList()
    {
        if (currentStepInIdxList > 0)
            currentStepInIdxList -= listOfGameImages.Count;
        else
            return;

        if (!downArrow.gameObject.activeSelf && currentStepInIdxList + listOfGameImages.Count < resultsList.Count)
            downArrow.SetActive(true);

        if (currentStepInIdxList == 0)
            upArrow.SetActive(false);

        RefreshPreviewImages();
    }
}
