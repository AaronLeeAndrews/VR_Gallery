using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePreview : MonoBehaviour {

    public Image GameImagePtr;
    public Texture2D tex2d_Test;
    public Sprite spr_Test;
    public Text descriptionPtr;
    public UnityEngine.Video.VideoPlayer vidPlayer;
    public GameObject vidPlayerPlane;
    public Button previewBackButton;

    // Use this for initialization
    public void FillValuesOfNameAndViewType(string gameName, GameObject gamePanelPtr)
    {
        previewBackButton.GetComponent<PreviewButtonScript>().SetGenreOrGamePanel(gamePanelPtr);
        gameObject.SetActive(true);

        descriptionPtr.text = System.IO.File.ReadAllText(gameName + "/Description.txt");

        vidPlayer.url = gameName + "/Vid.mp4";

        if (System.IO.File.Exists(gameName + "/Vid.mp4"))
        {
            vidPlayerPlane.SetActive(true);
            vidPlayer.gameObject.SetActive(true);
            vidPlayer.Play();
        }
        else
        {
            vidPlayerPlane.SetActive(false);
            vidPlayer.gameObject.SetActive(false);
            tex2d_Test = ((Resources.Load(gameName.Substring(19) + "/Banner", typeof(Texture2D))) as Texture2D);
            spr_Test = Sprite.Create(tex2d_Test, new Rect(0, 0, tex2d_Test.width, tex2d_Test.height), new Vector2(0, 0));
            GameImagePtr.sprite = spr_Test;
            GameImagePtr.gameObject.SetActive(true);
        }

    }
}
