using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class GamePreview : MonoBehaviour {

    public Image GameImagePtr;
    public Texture2D tex2d_Test;
    public Sprite spr_Test;
    public Text descriptionPtr;
    public UnityEngine.Video.VideoPlayer vidPlayer;
    public GameObject vidPlayerPlane;
    public Button previewBackButton;
    public Toggle previewPlayVideoToggle;
    private List<string> imageFileList;
    private int currImageListIdx;
    private string gameFileName;

    public List<GameObject> buttonHighlights;

    [DllImport("__Internal")]
    private static extern void CreateVideoDiv(string str);

    private void Start()
    {
        currImageListIdx = 0;
        vidPlayer.url = "null.mp4";
    }

    private void OnEnable()
    {
        for(int ii=0; ii < buttonHighlights.Count; ++ii)
        {
            buttonHighlights[ii].GetComponent<GamePreview_ButtonFlash>().enabled = true;
        }

        if (VideoDoesExist())
            previewPlayVideoToggle.gameObject.SetActive(true);
        else
            previewPlayVideoToggle.gameObject.SetActive(false);
    }

    public void ResetValues()
    {

        currImageListIdx = 0;
        imageFileList.Clear();
        vidPlayer.url = "null.mp4";
    }

    // Use this for initialization
    public void FillValuesOfNameAndViewType(string gameName, GameObject gamePanelPtr)
    {
        gameFileName = gameName;
        imageFileList = new List<string>();

        previewBackButton.GetComponent<PreviewButtonScript>().SetGenreOrGamePanel(gamePanelPtr);
        gameObject.SetActive(true);


        /////// Begin Desktop App //////////////////////////////////////////////////////////////////
        descriptionPtr.text = System.IO.File.ReadAllText(gameFileName + "/Description.txt");

        vidPlayerPlane.SetActive(false);
        vidPlayer.gameObject.SetActive(false);
        byte[] imgByteArr = null;
        if (System.IO.File.Exists(gameFileName + "/Banner.png"))
            imgByteArr = System.IO.File.ReadAllBytes(gameFileName + "/Banner.png");
        else
            imgByteArr = System.IO.File.ReadAllBytes(gameFileName + "/Banner.jpg");
        tex2d_Test = new Texture2D(100, 100);
        UnityEngine.ImageConversion.LoadImage(tex2d_Test, imgByteArr);
        spr_Test = Sprite.Create(tex2d_Test, new Rect(0, 0, tex2d_Test.width, tex2d_Test.height), new Vector2(0, 0));
        GameImagePtr.sprite = spr_Test;
        GameImagePtr.gameObject.SetActive(true);

        // Get the banner image
        if (System.IO.File.Exists(gameFileName + "/Banner.png"))
            imageFileList.Add(gameFileName + "/Banner.png");
        else if (System.IO.File.Exists(gameFileName + "/Banner.jpg"))
            imageFileList.Add(gameFileName + "/Banner.jpg");
        // Get the background image
        if (System.IO.File.Exists(gameFileName + "/BG.png"))
            imageFileList.Add(gameFileName + "/BG.png");
        else if (System.IO.File.Exists(gameFileName + "/BG.jpg"))
            imageFileList.Add(gameFileName + "/BG.jpg");
        // Get the img1
        if (System.IO.File.Exists(gameFileName + "/Img1.png"))
            imageFileList.Add(gameFileName + "/Img1.png");
        else if (System.IO.File.Exists(gameFileName + "/Img1.jpg"))
            imageFileList.Add(gameFileName + "/Img1.jpg");
        // Get the img2
        if (System.IO.File.Exists(gameFileName + "/Img2.png"))
            imageFileList.Add(gameFileName + "/Img2.png");
        else if (System.IO.File.Exists(gameFileName + "/Img2.jpg"))
            imageFileList.Add(gameFileName + "/Img2.jpg");
        // Get the img3
        if (System.IO.File.Exists(gameFileName + "/Img3.png"))
            imageFileList.Add(gameFileName + "/Img3.png");
        else if (System.IO.File.Exists(gameFileName + "/Img3.jpg"))
            imageFileList.Add(gameFileName + "/Img3.jpg");
        // Get the Controls image
        if (System.IO.File.Exists(gameFileName + "/Controls.png"))
            imageFileList.Add(gameFileName + "/Controls.png");
        else if (System.IO.File.Exists(gameFileName + "/Controls.jpg"))
            imageFileList.Add(gameFileName + "/Controls.jpg");
        /////// End Desktop App //////////////////////////////////////////////////////////////////

        //descriptionPtr.text = Resources.Load<TextAsset>(gameFileName + "/Description").text;

        //vidPlayerPlane.SetActive(false);
        //vidPlayer.gameObject.SetActive(false);
        //tex2d_Test = Resources.Load<Texture2D>(gameFileName + "/Banner");
        //spr_Test = Sprite.Create(tex2d_Test, new Rect(0, 0, tex2d_Test.width, tex2d_Test.height), new Vector2(0, 0));
        //GameImagePtr.sprite = spr_Test;
        //GameImagePtr.gameObject.SetActive(true);

        // Get the banner image
        //imageFileList.Add(gameFileName + "/Banner");
        // Get the background image
        //imageFileList.Add(gameFileName + "/BG");
        // Get the img1
        //imageFileList.Add(gameFileName + "/Img1");
        // Get the img2
        //imageFileList.Add(gameFileName + "/Img2");
        // Get the img3
        //imageFileList.Add(gameFileName + "/Img3");
        // Get the Controls image
        //imageFileList.Add(gameFileName + "/Controls");
    }

    public void GetImageInListEntry()
    {
        tex2d_Test = Resources.Load<Texture2D>(imageFileList[currImageListIdx]);
        spr_Test = Sprite.Create(tex2d_Test, new Rect(0, 0, tex2d_Test.width, tex2d_Test.height), new Vector2(0, 0));
        GameImagePtr.sprite = spr_Test;

        //if (!GameImagePtr.gameObject.active)
        //{
        //    //previewPlayVideoToggle.GetComponent<PlayVideoToggle>().ToggleOff();
        //    GameImagePtr.gameObject.SetActive(true);
        //}
    }

    public void IncrementImageListIdx()
    {
        currImageListIdx = (currImageListIdx + 1) % imageFileList.Count;
    }

    public void DecrementImageListIdx()
    {
        currImageListIdx = (currImageListIdx+(imageFileList.Count-1)) % imageFileList.Count;
    }

    public void PlayVideo()
    {
        TextAsset txt = Resources.Load<TextAsset>(gameFileName + "/Vid");
        //Debug.Log(txt.text);
        CreateVideoDiv(txt.text);

        //Turn off the images plane for the game
        //GameImagePtr.gameObject.SetActive(false);

        // Load up the video file
        //if (System.IO.File.Exists(gameFileName + "/Vid.mp4"))
        //{
        //    vidPlayer.url = gameFileName + "/Vid.mp4";
        //}
        //else if (System.IO.File.Exists(gameFileName + "/Vid.txt"))
        //{
        //    string newUrl = Resources.Load<TextAsset>(gameFileName + "/Vid").text;
        //    newUrl = Resources.Load<TextAsset>(gameFileName + "/Vid").text;
        //Debug.Log(newUrl);
        //    vidPlayer.url = newUrl;
        //}

        // Make sure we found a video
        //if (vidPlayer.url != "null.mp4")
        //{
            // Turn on the video plane for the game
            //vidPlayerPlane.SetActive(true);
            //vidPlayer.gameObject.SetActive(true);
            //vidPlayer.Play();
        //}
        //else
        //{
        //    previewPlayVideoToggle.GetComponent<PlayVideoToggle>().ToggleOff();
        //}
    }

    public void StopVideo()
    {
        ////Turn on the images plane for the game
        //GameImagePtr.gameObject.SetActive(true);

        //// Turn off the video plane for the game
        //vidPlayer.Stop();
        //vidPlayerPlane.SetActive(false);
        //vidPlayer.gameObject.SetActive(false);
    }

    public bool VideoDoesExist()
    {
        if (System.IO.File.Exists(gameFileName + "/Vid.mp4") ||
            System.IO.File.Exists(gameFileName + "/Vid.txt"))
        {
            return true;
        }
        return false;
    }
}
