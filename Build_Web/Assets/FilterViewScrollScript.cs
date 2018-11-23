using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilterViewScrollScript : MonoBehaviour {

    public Button upButton, downButton;
    public Scrollbar thisScrollbar;

    public void HideButtonIfAtThatEnd()
    {
        if (upButton.IsActive() && thisScrollbar.value == 1)
        {
            upButton.gameObject.SetActive(false);
        }
        else if (!upButton.IsActive() && thisScrollbar.value < 1)
        {
            upButton.gameObject.SetActive(true);
        }

        if (downButton.IsActive() && thisScrollbar.value == 0)
        {
            downButton.gameObject.SetActive(false);
        }
        else if (!downButton.IsActive() && thisScrollbar.value > 0)
        {
            downButton.gameObject.SetActive(true);
        }
    }
}
