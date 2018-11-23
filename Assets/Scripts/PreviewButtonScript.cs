using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewButtonScript : MonoBehaviour {

    GameObject currPanel;

    public void SetGenreOrGamePanel(GameObject panel)
    {
        currPanel = panel;
    }

    public void ActivatePanel()
    {
        currPanel.gameObject.SetActive(true);
    }

    public void DeactivatePanel()
    {
        currPanel.gameObject.SetActive(false);
    }
}
