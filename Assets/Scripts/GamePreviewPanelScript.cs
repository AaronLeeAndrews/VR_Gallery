using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GamePreviewPanelScript : MonoBehaviour {
    
    public GameObject dataManagerGameObject;
    public int currCategoryIdx;
    public int currGameIdx;
    public Text nameText;
    public GameObject upArrow, DownArrow;
    public List<Image> listOfGameImages;

    int gameListChangeValue;
    string categoryName;
    List<int> gameDataIndexList;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
