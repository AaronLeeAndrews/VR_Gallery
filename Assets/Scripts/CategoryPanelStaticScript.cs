using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class CategoryPanelStaticScript : MonoBehaviour {
    
    public GameObject dataManagerGameObject;
    public int currCategoryIdx;
    public int currGameIdx;
    public Text nameText;
    public GameObject leftArrow, rightArrow;
    public List<GameObject> listOfGameImageParents;

    int gameListChangeValue;
    string categoryName;
    List<int> gameDataIndexList;

    // Use this for initialization
    void Start () {
        currCategoryIdx = 0;
        currGameIdx =     0;
        gameListChangeValue = listOfGameImageParents.Count;
    }

    public void UpdateCurrCategoryIdx(int newCategoryIdx)
    {
        currCategoryIdx = newCategoryIdx;
        currGameIdx = dataManagerGameObject.GetComponent<DataManager>().CurrGameIdxOfCategoryIdx(newCategoryIdx);
        categoryName = dataManagerGameObject.GetComponent<DataManager>().NameOfCategoryIdx(newCategoryIdx);
        nameText.text = categoryName;
        gameDataIndexList = dataManagerGameObject.GetComponent<DataManager>().ListOfGameIndecesInCategory(categoryName);
        RefreshGameImages();
        leftArrow.gameObject.SetActive(false);
        if (gameDataIndexList.Count > gameListChangeValue)
            rightArrow.gameObject.SetActive(true);
        else
            rightArrow.gameObject.SetActive(false);
    }

    public void IncGameListIdx()
    {
        if (currGameIdx + gameListChangeValue < gameDataIndexList.Count)
            currGameIdx += gameListChangeValue;
        else
        {
            return;
        }

        RefreshGameImages();
        
        // Reactivate the leftArrow if was deactivated and the list of games exceed the change value
        if (!leftArrow.gameObject.activeSelf && gameDataIndexList.Count > gameListChangeValue)
            leftArrow.gameObject.SetActive(true);

        // Deactivate the rightArrow if all the way at the end of the gameImage list
        if (currGameIdx + gameListChangeValue >= gameDataIndexList.Count)
            rightArrow.gameObject.SetActive(false);
    }

    public void DecGameListIdx()
    {
        if (currGameIdx > 0)
            currGameIdx -= gameListChangeValue;
        else
        {
            return;
        }

        RefreshGameImages();

        // Reactivate the rightArrow if was deactivated and the list of games exceed the change value
        if (!rightArrow.gameObject.activeSelf && gameDataIndexList.Count > gameListChangeValue)
            rightArrow.gameObject.SetActive(true);

        // Deactivate the leftArrow if all the way at the start of the gameImage list
        if(currGameIdx == 0)
            leftArrow.gameObject.SetActive(false);
    }

    // Refreshes the gameImages in a category
    // Deactivates any gameImages that go beyond the count of the gameDataIndexList
    public void RefreshGameImages()
    {
        for (int ii = 0; ii < listOfGameImageParents.Count; ++ii)
        {
            // Fill out the sprites of each GameImage
            if (ii + currGameIdx < gameDataIndexList.Count)
            {
                listOfGameImageParents[ii].gameObject.SetActive(true);
                listOfGameImageParents[ii].GetComponent<GameImageScript>().UpdateImageAndIndexWithGameIndex(gameDataIndexList[ii + currGameIdx]);
            }
            else
                listOfGameImageParents[ii].gameObject.SetActive(false);
        }
    }

    public int GameDataIdxOfGameImageIdx(int gameImageIdx)
    {
        return gameDataIndexList[gameImageIdx + currGameIdx];
    }
}
