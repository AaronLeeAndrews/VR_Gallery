using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePreview_ButtonFlash : MonoBehaviour {

    public GameObject flashImage;
    public float buttonFlashDelay, buttonFlashLength;
    public float buttonFlashInLength, buttonFlashOutLength;

    public float buttonFlashDelayCurr, buttonFlashInLengthCurr;
    public float buttonFlashLengthCurr, buttonFlashOutLengthCurr;

    public float fadeLevel;

    private enum FlashState { DELAY, FLASHIN, FLASH, FLASHOUT, END };
    FlashState flashState;

    private void OnEnable()
    {
        flashState = FlashState.DELAY;
        buttonFlashDelayCurr = 0;
        buttonFlashInLengthCurr = 0;
        buttonFlashLengthCurr = 0;
        buttonFlashOutLengthCurr = 0;

        var tempColor = flashImage.GetComponent<Image>().color;
        fadeLevel = (buttonFlashInLengthCurr / buttonFlashInLength);
        tempColor.a = 0;
        flashImage.GetComponent<Image>().color = tempColor;
    }
	
	// Update is called once per frame
	void Update () {
		
        if(flashState == FlashState.END)
        {
            return;
        }
        else if (flashState == FlashState.DELAY)
        {
            // Incr delay time
            buttonFlashDelayCurr += Time.deltaTime;

            // Test if past delay time
            if (buttonFlashDelayCurr > buttonFlashDelay)
            {
                flashState = FlashState.FLASHIN;
                buttonFlashDelayCurr = 0;
                // Make the glow more intense
            }
        }
        else if (flashState == FlashState.FLASHIN)
        {
            // Incr flash-in time
            buttonFlashInLengthCurr += Time.deltaTime;
            var tempColor = flashImage.GetComponent<Image>().color;
            fadeLevel = (buttonFlashInLengthCurr / buttonFlashInLength);
            tempColor.a = fadeLevel;
            flashImage.GetComponent<Image>().color = tempColor;

            // Test if past flash-in time
            if (buttonFlashInLengthCurr > buttonFlashInLength)
            {
                fadeLevel = 1.0f;
                tempColor.a = fadeLevel;
                flashState = FlashState.FLASH;
                buttonFlashInLengthCurr = 0;
            }
        }
        else if(flashState == FlashState.FLASH)
        {
            // Incr flash time
            buttonFlashLengthCurr += Time.deltaTime;

            // Test if past flash time
            if (buttonFlashLengthCurr > buttonFlashLength)
            {
                flashState = FlashState.FLASHOUT;
                buttonFlashLengthCurr = 0;
                fadeLevel = 1;
                var tempColor = flashImage.GetComponent<Image>().color;
                tempColor.a = fadeLevel;
                flashImage.GetComponent<Image>().color = tempColor;
            }
        }
        else// if (flashState == FlashState.FLASHOUT)
        {
            // Incr flash-out time
            buttonFlashOutLengthCurr += Time.deltaTime;
            var tempColor = flashImage.GetComponent<Image>().color;
            fadeLevel = 1 - (buttonFlashOutLengthCurr / buttonFlashOutLength);
            tempColor.a = fadeLevel;
            flashImage.GetComponent<Image>().color = tempColor;

            // Test if past flash-Out time
            if (buttonFlashOutLengthCurr > buttonFlashOutLength)
            {
                flashState = FlashState.DELAY;
                buttonFlashOutLengthCurr = 0;
                tempColor.a = 0;
                flashImage.GetComponent<Image>().color = tempColor;
                gameObject.GetComponent<GamePreview_ButtonFlash>().enabled = false;
            }
        }
    }
}
