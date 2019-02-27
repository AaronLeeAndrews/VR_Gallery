using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;

public class SceneController : MonoBehaviour
{

    public GameObject dataManagerGameObject;

    string[] difficultyFilters = new string[] { "Easy", "Med.", "Hard" };

    string[] singleMultiFilters = new string[] { "Single", "Multi" };

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
    List<int> listOfFilteredGameIdx;
    bool noFiltersChecked;

    string[] str_arr;
    int gameCount;

    public GameObject genresViewPtr;
    public GameObject gamesViewPtr;
    public Button clearAllButtonPtr;
    public string introTextFile;

    // Variables to track the use of the app and return to the intro page if idle for too long
    bool galleryInUse = false, userIsIdle = false;
    float sessionLength = 0.0f, idleTimeLength = 0.0f, idleTimeLimit = 5.0f;
    Vector2 prevMousePos, currMousePos;
    public GameObject introPanel;
    List<float> listOfAllSessionTimesRecordedToday;
    string currDate;


    // Use this for initialization
    void Start()
    {
        noFiltersChecked = true;
        listOfFilteredGameIdx = new List<int>();

        difficultyFilters = dataManagerGameObject.GetComponent<DataManager>().ReturnStringArrOfDifficulties();
        singleMultiFilters = dataManagerGameObject.GetComponent<DataManager>().ReturnStringArrOfSingleMulti();
        categoryFilters = dataManagerGameObject.GetComponent<DataManager>().ReturnStringArrOfCategories();

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

        str_arr = Directory.GetDirectories(("./Assets/Resources/Game_Files"), "*", SearchOption.TopDirectoryOnly);
        //string gameDirsOnOneLine = Resources.Load<TextAsset>("Text/GameList").text;
        //str_arr = gameDirsOnOneLine.Split(new string[] { "\r\n" }, System.StringSplitOptions.RemoveEmptyEntries);
        gameCount = str_arr.Length;
        //Debug.Log(gameCount);
        //Debug.Log(str_arr.Length);
        //for (int ii=0; ii<str_arr.Length && ii<4; ++ii)
        //{
        //    Debug.Log(str_arr[ii]);
        //}

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

    public void FilterChanged()
    {
        if (noFiltersChecked) // If there were no other filters on, one is now on
        {
            noFiltersChecked = false;
            clearAllButtonPtr.gameObject.SetActive(true);
            gamesViewPtr.gameObject.SetActive(true);
            genresViewPtr.gameObject.SetActive(false);

            CheckGamesAgainstFilters();
            gamesViewPtr.GetComponent<GamePreviewPanelScript>().UpdateGameIdxList(listOfFilteredGameIdx);
            gamesViewPtr.GetComponent<GamePreviewPanelScript>().RefreshPreviewImages();
        }
        else // If the filter was just turned off, it might be the last one off, so check them all
        {
            CheckIfAnyFiltersAreOn();
    
            if (noFiltersChecked)
            {
                gamesViewPtr.SetActive(false);
                genresViewPtr.SetActive(true);
            }
             else
            {
                CheckGamesAgainstFilters();
                gamesViewPtr.GetComponent<GamePreviewPanelScript>().UpdateGameIdxList(listOfFilteredGameIdx);
                gamesViewPtr.GetComponent<GamePreviewPanelScript>().RefreshPreviewImages();
            }   
        }
    }

    public void CheckIfAnyFiltersAreOn()
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
            gamesViewPtr.gameObject.SetActive(false);
            genresViewPtr.gameObject.SetActive(true);
        }
        else
        {
            listOfFilteredGameIdx.Clear();
            
            // Go through each game and see if they match the selected filters
            // If they match they will be added to the list of games to display
            bool skipGame = true;
            for (int ii = 0; ii < dataManagerGameObject.GetComponent<DataManager>().GetGameDataListCount(); ++ii)
            {
                // Check selected difficulty levels (Easy, Med., Hard)
                if (difficultyFilterDict["Easy"] || difficultyFilterDict["Med."] || difficultyFilterDict["Hard"])
                {
                    // Look through each difficulty category, if it is selected
                    for (int jj = 0; jj < difficultyFilters.Length; ++jj)
                    {
                        if (difficultyFilterDict[difficultyFilters[jj]] &&
                            dataManagerGameObject.GetComponent<DataManager>().GameIsInDifficultyCategory(ii, difficultyFilters[jj]))
                        {
                            skipGame = false;
                            break;
                        }
                    }
                    if (skipGame)
                    {
                        continue; // The game isn't of the selected difficulty, so do not add to the list
                    }
                }

                // Check if either selected single/multi
                skipGame = true;
                if (singleMultiFilterDict["Single"] || singleMultiFilterDict["Multi"])
                {
                    // Look through each single-multi category, if it is selected
                    for (int jj = 0; jj < singleMultiFilters.Length; ++jj)
                    {
                        if (singleMultiFilterDict[singleMultiFilters[jj]] &&
                            dataManagerGameObject.GetComponent<DataManager>().GameIsInSingleMultiCategory(ii, singleMultiFilters[jj]))
                        {
                            skipGame = false;
                            break;
                        }
                    }
                    if (skipGame)
                    {
                        continue; // The game isn't of the selected difficulty, so do not add to the list
                    }
                }

                // Check against the category filters
                skipGame = true;
                if (AtLeastOneCategoryIsSelected())
                {
                    for (int jj = 0; jj < categoryFilters.Length; ++jj)
                    {
                        // Make sure the category filter is turned on
                        if (categoryFilterDict[categoryFilters[jj]] &&
                           dataManagerGameObject.GetComponent<DataManager>().GameIsInGenresCategory(ii, categoryFilters[jj]))
                        {
                            skipGame = false;
                            break;
                        }
                    }
                    if (skipGame)
                    {
                        continue; // The game isn't of the selected difficulty, so do not add to the list
                    }
                }

                //Passed all checks, so add the game to the list of games to display
                listOfFilteredGameIdx.Add(ii);
            }
        }
    }

    bool AtLeastOneCategoryIsSelected()
    {
        for (int ii = 0; ii < categoryFilters.Length; ++ii)
        {
            if (categoryFilterDict[categoryFilters[ii]])
                return true;
        }
        return false;
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
