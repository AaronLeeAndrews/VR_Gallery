using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenresPreviewPanelScript : MonoBehaviour {
    
    public GameObject dataManagerGameObject;
    int currCategoryIdx;
    public List<GameObject> listOfGameObjectCategories;
    public GameObject topArrow, bottomArrow;

    int categoryChangeValue = 3;
    
    void Start ()
    {
    }
	
	// Update is called once per frame
	void Update () {

    }

    private void OnEnable()
    {
        currCategoryIdx = 0;

        RefreshCategories();

        topArrow.gameObject.SetActive(false);
    }

    public void IncIdxsOfCategoryList()
    {
        int numberOfCatagories = dataManagerGameObject.GetComponent<DataManager>().allCategoryFilters.Length;

        if (currCategoryIdx + categoryChangeValue < numberOfCatagories)
        {
            currCategoryIdx += categoryChangeValue;
        }

        // Test if we are at the final categories in the list, and we can't inc any more
        if (currCategoryIdx + categoryChangeValue >= numberOfCatagories)
        {
            // Deactivate the bottomArrow if all the way at the end of the category list
            bottomArrow.gameObject.SetActive(false);
        }

        RefreshCategories();

        // Reactivate the leftArrow if was deactivated
        if (!topArrow.gameObject.activeSelf)
            topArrow.gameObject.SetActive(true);
    }

    public void DecIdxsOfCategoryList()
    {
        if (currCategoryIdx > 0)
        {
            currCategoryIdx -= categoryChangeValue;
        }
        else
        {
            return;
        }

        RefreshCategories();

        // Reactivate the leftArrow if was deactivated
        if (!bottomArrow.gameObject.activeSelf)
            bottomArrow.gameObject.SetActive(true);

        // Deactivate the bottomArrow if all the way at the end of the category list
        if(currCategoryIdx == 0)
            topArrow.gameObject.SetActive(false);
    }

    // Refreshes the categories
    // Deactivates any categories that go beyond the count of the categoryList
    public void RefreshCategories()
    {
        // Activate any deactivated categories
        for (int ii = 0; ii < listOfGameObjectCategories.Count; ++ii)
            listOfGameObjectCategories[ii].gameObject.SetActive(true);

        int numberOfCatagories = dataManagerGameObject.GetComponent<DataManager>().allCategoryFilters.Length;

        for (int ii = 0; ii < listOfGameObjectCategories.Count; ++ii)
        {
            if (ii + currCategoryIdx < numberOfCatagories)
            {
                listOfGameObjectCategories[ii].GetComponent<CategoryPanelStaticScript>().UpdateCurrCategoryIdx(ii + currCategoryIdx);
                listOfGameObjectCategories[ii].GetComponent<CategoryPanelStaticScript>().RefreshGameImages();
            }
            else
                listOfGameObjectCategories[ii].gameObject.SetActive(false);
        }
    }
}
