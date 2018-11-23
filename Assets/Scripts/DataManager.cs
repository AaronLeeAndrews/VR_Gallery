using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;

public class DataManager : MonoBehaviour {

    // This is the base data we need
    struct GameData
    {
        public string gameNameFileDir;
        public string[] gameCategories;
    }

    public Sprite defualtSprite;
    public GameObject gamePreviewPanel;

    List<int> currGameIdxInEachCategory;
    List<GameData> gameDataList;
    string gameFileTopDir = "./Assets/Resources/Game_Files/";
    string[] gameNameFileDirArr;

    string[] difficultyFilters = new string[] { "Easy", "Med.", "Hard" };

    string[] singleMultiFilters = new string[] { "Single", "Multi" };

    string[] categoryFilters = new string[] { "Shooter", "Action",
                                                "Art", "Horror",
                                                "Sports", "Puzzle",
                                                "ForKids", "StaffFav",
                                                "Exploration", "Tutorial",
                                                "Simulation", "Movie",
                                                "Adventure", "Fantasy"};

    public string[] allCategoryFilters = new string[] {"Easy", "Med.", "Hard",
                                                        "Single", "Multi",
                                                        "Shooter", "Action",
                                                        "Art", "Horror",
                                                        "Sports", "Puzzle",
                                                        "ForKids", "StaffFav",
                                                        "Exploration", "Tutorial",
                                                        "Simulation", "Movie",
                                                        "Adventure", "Fantasy"};

    // Use this for initialization
    void Start () {
        FillGameDataList();
        currGameIdxInEachCategory = new List<int>();
        for (int ii = 0; ii < allCategoryFilters.Length; ++ii)
            currGameIdxInEachCategory.Add(0);

        gamePreviewPanel.SetActive(true);
    }

    public void FillGameDataList()
    {
        gameDataList = new List<GameData>();
        gameNameFileDirArr = Directory.GetDirectories((gameFileTopDir), "*", SearchOption.TopDirectoryOnly);
        currGameIdxInEachCategory = new List<int>();
        for (int ii = 0; ii < allCategoryFilters.Length; ++ii)
            currGameIdxInEachCategory.Add(0);

        for (int ii = 0; ii < gameNameFileDirArr.Length; ++ii)
        {
            GameData newGameDataItem;// = new GameData();

            // Get the name, categories, and logo image for the new GameData item
            newGameDataItem.gameNameFileDir = gameNameFileDirArr[ii];
            newGameDataItem.gameCategories = System.IO.File.ReadAllLines(newGameDataItem.gameNameFileDir + "/Tags.txt");

            // Put the new GameData item into the list
            gameDataList.Add(newGameDataItem);
        }
    }

    public string FileDirOfGameIdx(int gameIdx)
    {
        return gameDataList[gameIdx].gameNameFileDir;
    }

    public Sprite SpriteOfGameIdx(int gameIdx)
    {
        byte[] imgByteArr = null;
        Texture2D tex2dTemp;
        Sprite spriteTemp = defualtSprite;

        // Check the file system for the image file
        if (System.IO.File.Exists(gameDataList[gameIdx].gameNameFileDir + "/Banner.png"))
            imgByteArr = System.IO.File.ReadAllBytes(gameDataList[gameIdx].gameNameFileDir + "/Banner.png");
        else
            imgByteArr = System.IO.File.ReadAllBytes(gameDataList[gameIdx].gameNameFileDir + "/Banner.jpg");

        // Create the Sprite as a Texture2d
        tex2dTemp = new Texture2D(100, 100);
        UnityEngine.ImageConversion.LoadImage(tex2dTemp, imgByteArr);
        spriteTemp = Sprite.Create(tex2dTemp, new Rect(0, 0, tex2dTemp.width, tex2dTemp.height), new Vector2(0, 0));

        // Return the Sprite inside of the image
        return spriteTemp;
    }

    public bool GameIsInCategory(int gameDataIdx, string category)
    {
        for(int ii=0; ii<gameDataList[gameDataIdx].gameCategories.Length; ++ii)
        {
            if(gameDataList[gameDataIdx].gameCategories[ii] == category)
            {
                return true;
            }
        }
        return false;
    }

    public List<int> ListOfGameIndecesInCategory(string categoryName)
    {
        List<int> listOfGameIndeces = new List<int>();

        for (int ii = 0; ii < gameDataList.Count; ++ii)
        {
            if (GameIsInCategory(ii, categoryName))
            {
                listOfGameIndeces.Add(ii);
            }
        }

        return listOfGameIndeces;
    }

    public string NameOfCategoryIdx(int categoryIdx)
    {
        return allCategoryFilters[categoryIdx];
    }

    public int CurrGameIdxOfCategoryIdx(int categoryIdx)
    {
        if (currGameIdxInEachCategory == null)
            return 0;

        return currGameIdxInEachCategory[categoryIdx];
    }

    public int UpdateIdxOfGameCategoryIdx(int newGameIdx, int categoryIdx)
    {
        return currGameIdxInEachCategory[categoryIdx] = newGameIdx;
    }
}
