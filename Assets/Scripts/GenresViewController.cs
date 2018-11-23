using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GenresViewController : MonoBehaviour
{
    string[] genresFilters = new string[] { "Easy", "Med.", "Hard",
                                                "Shooter", "Action",
                                                "Art", "Horror",
                                                "Sports", "Puzzle",
                                                "ForKids", "StaffFav",
                                                "Exploration", "Tutorial",
                                                "Simulation", "Movie",
                                                "Adventure", "Fantasy"};

    string[] str_arr;
    int gameCount;
    float xInnerOffset = -100f, xOuterOffset = -150f, yInnerOffset = -45f, yOuterOffset = 390f;
    float xLogoSize = 200f, yLogoSize = 100f;

    public GameObject genresViewPtr;
    public GameObject genresViewContentPtr;
    public GameObject gamesPreviewPtr;
    public GameObject buttonImagePrefab, categoryPanelPrefab;
    public List<GameObject> gameLogoArr_masterlist;
    public Scrollbar contentScrollbar;

    List<GameObject> panelArr_masterlist;
    List<List<string>> gameLogo2dDirectorylist;

    // Use this for initialization
    void Start()
    {
        //str_arr = Directory.GetDirectories(("./Assets/Resources/Game_Files"), "*", SearchOption.TopDirectoryOnly);
        string gameNamesOnOneLine = Resources.Load<TextAsset>("Text/GameList").text;
        str_arr = gameNamesOnOneLine.Split(new string[] { "\r\n" }, System.StringSplitOptions.RemoveEmptyEntries);

        gameCount = str_arr.Length;

        gameLogoArr_masterlist = new List<GameObject>();

        gameLogo2dDirectorylist = new List<List<string>>();
        for (int ii = 0; ii < genresFilters.Length; ++ii)
        {
            gameLogo2dDirectorylist.Add(new List<string>());
        }

        CreatePreviewImages();
        CheckGamesAgainstFilters();
        CreateEachCategoryObject();
        //Cursor.visible = false;
    }

    void CreatePreviewImages()
    {
        for (int ii = 0; ii < gameCount; ++ii)
        {
            gameLogoArr_masterlist.Add(Instantiate(buttonImagePrefab));
            //gameLogoArr_masterlist[ii].transform.SetParent(genresViewContentPtr.transform);
            gameLogoArr_masterlist[ii].GetComponent<Button_gameImageClass>().FillValues(str_arr[ii],
                                                                                        gamesPreviewPtr, genresViewPtr);
        }
    }

    public void CheckGamesAgainstFilters()
    {
        // Look through all of the categories
        for (int ii = 0; ii < genresFilters.Length; ++ii)
        {   // Look through each entry of the master list
            for (int jj = 0; jj < gameLogoArr_masterlist.Count; ++jj)
            {   // look through the filter list of each entry
                for (int kk = 0; kk < gameLogoArr_masterlist[jj].GetComponent<Button_gameImageClass>().filters.Length; ++kk)
                {   // Check each filter against the current category
                    if (gameLogoArr_masterlist[jj].GetComponent<Button_gameImageClass>().filters[kk] == genresFilters[ii])
                    {   // If the filter and category match, add that index of the master list into the 2d array
                        gameLogo2dDirectorylist[ii].Add(gameLogoArr_masterlist[jj].GetComponent<Button_gameImageClass>().fileDir);
                        break;
                    }
                }
            }
        }
    }
    
    public void CreateEachCategoryObject()
    {
        panelArr_masterlist = new List<GameObject>();
        for (int ii = 0; ii < gameLogo2dDirectorylist.Count; ++ii)
        {
            if (gameLogo2dDirectorylist[ii].Count == 0)
                continue;

            var categoryPrefab = Instantiate(categoryPanelPrefab);
            categoryPrefab.transform.SetParent(genresViewContentPtr.transform);
            categoryPrefab.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            categoryPrefab.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            categoryPrefab.GetComponent<RectTransform>().offsetMax = new Vector2(0, 71);
            categoryPrefab.GetComponent<RectTransform>().localPosition = new Vector3(0, 385 - 65* panelArr_masterlist.Count, 0);
            categoryPrefab.GetComponent<categoryPanelScript>().FillValues(genresFilters[ii], gameLogo2dDirectorylist[ii],
                                                                                       gamesPreviewPtr, genresViewPtr);

            categoryPrefab.GetComponent<categoryPanelScript>().textName.GetComponent<RectTransform>().rect.Set(0, 0, 1, 1);

            panelArr_masterlist.Add(categoryPrefab);
        }
        if(panelArr_masterlist.Count > 0)
            contentScrollbar.GetComponent<GenresContentAreaScrollScript>().yHeight = panelArr_masterlist[0].GetComponent<RectTransform>().rect.height;
        contentScrollbar.GetComponent<GenresContentAreaScrollScript>().numberOfItems = panelArr_masterlist.Count;
        contentScrollbar.GetComponent<GenresContentAreaScrollScript>().SetValues();
    }
}
