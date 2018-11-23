using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenresContentAreaScrollScript : MonoBehaviour {

    public Button upButton, downButton;
    public Scrollbar thisScrollbar;
    public RectTransform contentArea;
    public int numberOfItems;
    public float yOffset, yHeight;
    float jumpAmount;
    
    int numberOfItemsOffset = 2, itemsPerJump = 3;

    public void SetValues()
    {
        Rect rect = contentArea.rect;

        int steps = ((numberOfItems + numberOfItemsOffset) / itemsPerJump);

        if (steps > 1)
        {
            thisScrollbar.numberOfSteps = steps;
            rect.height = (steps * itemsPerJump * yHeight) - (steps * itemsPerJump * yOffset);
            jumpAmount = (1.0f / steps) + 0.00001f;
            upButton.GetComponent<ScrollButton>().scrollAddValue = jumpAmount;
            downButton.GetComponent<ScrollButton>().scrollAddValue = -jumpAmount;
            upButton.gameObject.SetActive(false);
        }
        else
        {
            downButton.gameObject.SetActive(false);
        }

        contentArea.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rect.height);
        gameObject.GetComponent<Scrollbar>().value = 1.0f;
    }

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
