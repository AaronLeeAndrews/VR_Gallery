using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;

public class SceneController : MonoBehaviour
{
    string[] difficultyFilters = new string[] { "Easy", "Med.", "Hard"};

    string[] singleMultiFilters = new string[] { "Single", "Multi"};

    string[] categoryFilters = new string[] { "Shooter", "Action",
                                                "Art", "Horror",
                                                "Sports", "Puzzle",
                                                "ForKids", "StaffFav",
                                                "Exploration", "Tutorial",
                                                "Simulation", "Movie",
                                                "Adventure", "Fantasy"};

    Dictionary<string, bool> difficultyFilterDict = new Dictionary<string, bool>();
    Dictionary<string, bool> singleMultiFilterDict = new Dictionary<string, bool>();
    Dictionary<string, bool> categoryFilterDict = new Dictionary<string, bool>();
    bool noFiltersChecked;

    string[] str_arr;
    int gameCount;
    float xInnerOffset = -100f, xOuterOffset = -150f, yInnerOffset = -45f, yOuterOffset = 390f;
    float xLogoSize = 200f, yLogoSize = 100f;

    public GameObject genresViewPtr;
    public GameObject gamesViewPtr;
    public GameObject gamesViewContentPtr;
    public GameObject gamesPreviewPtr;
    public GameObject prefab;
    public Button clearAllButtonPtr;
    public string introTextFile;
    List<GameObject> gameLogoArr_masterlist;
    List<GameObject> gameLogoArr_ptrlist;

    // Variables to track the use of the app and return to the intro page if idle for too long
    bool galleryInUse = false, userIsIdle = false;
    float sessionLength = 0.0f, idleTimeLength = 0.0f, idleTimeLimit = 5.0f;
    Vector2 prevMousePos, currMousePos;
    public GameObject introPanel;
    List<float> listOfAllSessionTimesRecordedToday;
    string currDate;
    

    // Use this for initialization
    void Start ()
    {
        noFiltersChecked = true;

        for (int ii = 0; ii < difficultyFilters.Length; ++ii)
        {
            difficultyFilterDict.Add(difficultyFilters[ii], false);
        }
        for (int ii = 0; ii < singleMultiFilters.Length; ++ii)
        {
            singleMultiFilterDict.Add(singleMultiFilters[ii], false);
        }
        for (int ii = 0; ii < categoryFilters.Length; ++ii)
        {
            categoryFilterDict.Add(categoryFilters[ii], false);
        }

        //str_arr = Directory.GetDirectories(("./Assets/Resources/Game_Files"), "*", SearchOption.TopDirectoryOnly);
        //str_arr = Directory.GetDirectories(("./Assets/Resources/Game_Files"), "*", SearchOption.TopDirectoryOnly);
        string gameDirsOnOneLine = Resources.Load<TextAsset>("Text/GameList").text;
        str_arr = gameDirsOnOneLine.Split(new string[] { "\r\n" }, System.StringSplitOptions.RemoveEmptyEntries);
        gameCount = str_arr.Length;
        //Debug.Log(gameCount);
        //Debug.Log(str_arr.Length);
        //for (int ii=0; ii<str_arr.Length && ii<4; ++ii)
        //{
        //    Debug.Log(str_arr[ii]);
        //}
        gameLogoArr_masterlist = new List<GameObject>();
        if (prefab != null)
            CreatePreviewImages();
        else
            Debug.Log("prefab is null!!!!");

        // Get the intro text from file
        TextAsset introTextFile = Resources.Load<TextAsset>("Text/IntroText");
        introPanel.gameObject.GetComponentInChildren<Text>().text = introTextFile.text;

        //currDate = System.DateTime.Today.ToString();
        //listOfAllSessionTimesRecordedToday = new List<float>();

        Cursor.visible = false;
    }

    private void Update()
    {
        /*
        currMousePos = Input.mousePosition;

        if(galleryInUse)
        {
            sessionLength += Time.deltaTime;

            if (!userIsIdle)
            {
                if (currMousePos == prevMousePos)
                {
                    BeginTrackingIdle();
                }
            }
            else if(currMousePos != prevMousePos)
            {
                StopAndClearIdle();
            }

            if (userIsIdle)
            {
                idleTimeLength += Time.deltaTime;

                if(idleTimeLength > idleTimeLimit)
                {
                    EndSessionFromTimeOut();
                }
            }
        }
        prevMousePos = currMousePos;
        */
    }

    private void OnDestroy()
    {
        //RecordAndClearListOfSessionTimes();
    }

    void CreatePreviewImages()
    {
        float xOffset, yOffset;
        for (int ii = 0; ii < gameCount; ++ii)
        {
            //Add together the outer offsets, inner offsets, and length of the other logos
            xOffset = xOuterOffset + (ii % 4) * (xLogoSize + xInnerOffset);
            yOffset = yOuterOffset - (int)(ii / 4) * (yLogoSize + yInnerOffset);
            gameLogoArr_masterlist.Add(Instantiate(prefab));
            gameLogoArr_masterlist[ii].transform.SetParent(gamesViewContentPtr.transform);
            gameLogoArr_masterlist[ii].GetComponent<Button_gameImageClass>().FillValues(str_arr[ii], 
                                                                                        gamesPreviewPtr, gamesViewPtr);
            gameLogoArr_masterlist[ii].GetComponent<RectTransform>().localPosition = new Vector3(xOffset, yOffset, 0);
        }
        gameLogoArr_ptrlist = new List<GameObject>(gameLogoArr_masterlist);
    }

    void RefreshPreviewImages()
    {
        float xOffset, yOffset;
        for (int ii = 0; ii < gameLogoArr_ptrlist.Count; ++ii)
        {
            //Add together the outer offsets, inner offsets, and length of the other logos
            xOffset = xOuterOffset + (ii % 4) * (xLogoSize + xInnerOffset);
            yOffset = yOuterOffset - (int)(ii / 4) * (yLogoSize + yInnerOffset);
            gameLogoArr_ptrlist[ii].GetComponent<RectTransform>().localPosition = new Vector3(xOffset, yOffset, 0);
        }
    }

    public void FilterChanged()
    {
        if (noFiltersChecked) // If there were no other filters on, one is now on
        {
            noFiltersChecked = false;
            clearAllButtonPtr.gameObject.SetActive(true);
            gamesViewPtr.gameObject.SetActive(true);
            genresViewPtr.gameObject.SetActive(false);
        }
        else // If the filter was just turned off, it might be the last one off, so check them all
        {
            CheckIfAnyFiltersAreOn();
        }

        CheckGamesAgainstFilters();
        RefreshPreviewImages();
    }

    public void DifficultyValueChanged(string key)
    {
        difficultyFilterDict[key] = !difficultyFilterDict[key];

        FilterChanged();
    }
    public void SingleMultiValueChanged(string key)
    {
        singleMultiFilterDict[key] = !singleMultiFilterDict[key];

        FilterChanged();
    }
    public void CategoryValueChanged(string key)
    {
        categoryFilterDict[key] = !categoryFilterDict[key];

        FilterChanged();
    }

    void CheckIfAnyFiltersAreOn()
    {
        noFiltersChecked = true;
        clearAllButtonPtr.gameObject.SetActive(false);

        // Check if any difficulty filters are on
        for (int ii = 0; ii < difficultyFilters.Length; ++ii) // Look through all of the filters
        {
            if (difficultyFilterDict[difficultyFilters[ii]]) // If any filters are found on, we can stop looking
            {
                noFiltersChecked = false;
                clearAllButtonPtr.gameObject.SetActive(true);
                gamesViewPtr.gameObject.SetActive(true);
                genresViewPtr.gameObject.SetActive(false);
                return;
            }
        }
        // Check if any singl/multi filters are on
        for (int ii = 0; ii < singleMultiFilters.Length; ++ii) // Look through all of the filters
        {
            if (singleMultiFilterDict[singleMultiFilters[ii]]) // If any filters are found on, we can stop looking
            {
                noFiltersChecked = false;
                clearAllButtonPtr.gameObject.SetActive(true);
                gamesViewPtr.gameObject.SetActive(true);
                genresViewPtr.gameObject.SetActive(false);
                return;
            }
        }
        // Check if any category filters are on
        for (int ii = 0; ii < categoryFilters.Length; ++ii) // Look through all of the filters
        {
            if (categoryFilterDict[categoryFilters[ii]]) // If any filters are found on, we can stop looking
            {
                noFiltersChecked = false;
                clearAllButtonPtr.gameObject.SetActive(true);
                gamesViewPtr.gameObject.SetActive(true);
                genresViewPtr.gameObject.SetActive(false);
                return;
            }
        }
    }

    public void CheckGamesAgainstFilters()
    {
        if (noFiltersChecked)
        {
            for (int ii = 0; ii < gameLogoArr_masterlist.Count; ++ii) // Look through all of the games
            {
                gameLogoArr_masterlist[ii].SetActive(true);
                gamesViewPtr.gameObject.SetActive(false);
                genresViewPtr.gameObject.SetActive(true);
            }
            gameLogoArr_ptrlist = gameLogoArr_masterlist;
        }
        else
        {
            gameLogoArr_ptrlist = new List<GameObject>();

            for (int ii = 0; ii < gameLogoArr_masterlist.Count; ++ii)
            {
                gameLogoArr_masterlist[ii].SetActive(true);
            }

            // Go through each game and see if they match the selected filters
            // If they match they will be added to the list of games to display
            for (int ii = 0; ii < gameLogoArr_masterlist.Count; ++ii) // Look through all of the games
            {
                // Check any selected difficulty levels (Easy, Med., Hard)
                if(difficultyFilterDict["Easy"] || difficultyFilterDict["Med."] || difficultyFilterDict["Hard"])
                {
                    // The first index will be the difficulty, check if it is selected
                    if(!difficultyFilterDict[gameLogoArr_masterlist[ii].GetComponent<Button_gameImageClass>().filters[0]])
                    {
                        gameLogoArr_masterlist[ii].SetActive(false);
                        continue; // The game isn't of the selected difficulty, so do not add to the list
                    }
                }
                
                // Check if either selected single/multi
                if (gameLogoArr_masterlist[ii].activeSelf &&
                    (singleMultiFilterDict["Single"] || singleMultiFilterDict["Multi"]) &&
                    gameLogoArr_masterlist[ii].GetComponent<Button_gameImageClass>().filters[1] != "SingleMulti")
                {
                    // The second index will be the single/multi, check if it is selected
                    if (!singleMultiFilterDict[gameLogoArr_masterlist[ii].GetComponent<Button_gameImageClass>().filters[1]])
                    {
                        gameLogoArr_masterlist[ii].SetActive(false);
                        continue; // The game isn't of the selected single/multi, so do not add to the list
                    }
                }

                // Check against the category filters
                foreach (var categoryFilter in categoryFilterDict)
                {
                    if (categoryFilter.Value)
                    {
                        bool categoryFilterIsFoundInGame = false;
                        for (int jj = 2; jj < gameLogoArr_masterlist[ii].GetComponent<Button_gameImageClass>().filters.Length; ++jj) // Look through each game's filters
                        {
                            if (gameLogoArr_masterlist[ii].GetComponent<Button_gameImageClass>().filters[jj] == categoryFilter.Key)
                            {
                                categoryFilterIsFoundInGame = true;
                                break;
                            }
                        }
                        if (!categoryFilterIsFoundInGame)
                        {
                            gameLogoArr_masterlist[ii].SetActive(false);
                            break; // The game isn't of the selected categories, so do not add to the list
                        }
                    }
                }
                if(gameLogoArr_masterlist[ii].activeSelf)
                {
                    gameLogoArr_ptrlist.Add(gameLogoArr_masterlist[ii]);
                }
            }
        }
    }

    public void ClearAllFilters()
    {
        // Turn off all of the filters in our interface
        var childrenArr = gameObject.GetComponentsInChildren<Toggle>();
        for (int ii = 0; ii < childrenArr.Length; ++ii)
        {
            if (childrenArr[ii].isOn) // If any filters are found on, we turn them off
            {
                childrenArr[ii].isOn = false;
            }
        }
    }

    void RecordAndClearListOfSessionTimes()
    {
        string sessionsToRecord = "";
        string formattedDate = "";
        // Format the date to not include the time
        for(int ii=0; ii<currDate.Length; ++ii)
        {
            if (currDate[ii] == ' ')
                break;
            formattedDate += currDate[ii];
        }
        // Switch out all of the slashes with dashes
        formattedDate = formattedDate.Replace('/', '-');
        Debug.Log(formattedDate);

        for(int ii=0; ii<listOfAllSessionTimesRecordedToday.Count; ++ii)
        {
            sessionsToRecord += listOfAllSessionTimesRecordedToday[ii].ToString() + "\r\n";
        }
        System.IO.File.WriteAllText("./Assets/Resources/SessionTimes/" + formattedDate + ".txt", sessionsToRecord);
        listOfAllSessionTimesRecordedToday.Clear();
    }

    public void BeginTrackingUse()
    {
        Debug.Log("BeginTrackingUse");
        //Switch the bool to positive
        galleryInUse = true;
    }

    void BeginTrackingIdle()
    {
        Debug.Log("BeginTrackingIdle");
        userIsIdle = true;
    }

    void StopAndClearIdle()
    {
        Debug.Log("StopAndClearIdle");
        userIsIdle = false;
        idleTimeLength = 0.0f;
    }

    void EndSessionFromTimeOut()
    {
        //Debug.Log("EndSessionFromTimeOut");
        galleryInUse = false;
        userIsIdle = false;

        // Make sure we don't have any sessions left over from yesterday
        if (currDate != System.DateTime.Today.ToString())
        {
            RecordAndClearListOfSessionTimes();
            currDate = System.DateTime.Today.ToString();
        }

        // Save the data to the list of session times
        listOfAllSessionTimesRecordedToday.Add(sessionLength);


        sessionLength = 0.0f;
        idleTimeLength = 0.0f;

        introPanel.SetActive(true);
    }
}
