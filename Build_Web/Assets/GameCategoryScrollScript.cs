using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCategoryScrollScript : MonoBehaviour {

    public Button leftButton, rightButton;
    public Scrollbar thisScrollbar;
    public RectTransform contentArea;
    public int numberOfItems;
    public float xOffset, xWidth;

    float jumpAmount;
    int numberOfItemsOffset = 3, itemsPerJump = 4;

    public void SetValues()
    {
        Rect rect = contentArea.rect;
        RectTransform rT = contentArea;

        int steps = ((numberOfItems+numberOfItemsOffset) / itemsPerJump);

        if (steps > 1) // Make sure that there is are enough objects to require more than one page
        {
            thisScrollbar.numberOfSteps = steps;
            rect.width = (steps * itemsPerJump * xWidth) + (((steps * itemsPerJump)) * xOffset) + xOffset/2.0f;
            jumpAmount = (1.0f / steps) + 0.00001f;
            leftButton.GetComponent<ScrollButton>().scrollAddValue = -jumpAmount;
            rightButton.GetComponent<ScrollButton>().scrollAddValue = jumpAmount;
        }
        else
        {
            rightButton.gameObject.SetActive(false);
        }

        contentArea.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rect.width);
    }

    public void HideButtonIfAtThatEnd()
    {
        if(leftButton.IsActive() && thisScrollbar.value == 0)
        {
            leftButton.gameObject.SetActive(false);
        }
        else if(!leftButton.IsActive() && thisScrollbar.value > 0)
        {
            leftButton.gameObject.SetActive(true);
        }

        if (rightButton.IsActive() && thisScrollbar.value == 1)
        {
            rightButton.gameObject.SetActive(false);
        }
        else if (!rightButton.IsActive() && thisScrollbar.value < 1)
        {
            rightButton.gameObject.SetActive(true);
        }
    }
}
