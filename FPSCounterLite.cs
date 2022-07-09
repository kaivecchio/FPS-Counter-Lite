using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class FPSCounterLite : MonoBehaviour
{
    const float fpsMeasurePeriod = 0.5f;
    private int fpsAccumulator = 0;
    private float fpsNextPeriod = 0;
    private int currentFps;
    const string display = "FPS: ";
    private Text text;

    public bool showTotalFrames;
    private int totalFramesElapsed;
    const string framesElapsedDisplay = "Frames Elapsed: ";

    public bool showTimeElapsed;
    const string timeElapsedDisplay = "Real Time Elapsed: ";


    private void Start()
    {
        fpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;
        text = GetComponent<Text>();

        //Ensures we don't have strange wrapping
        text.horizontalOverflow = HorizontalWrapMode.Overflow;
    }


    private void Update()
    {
        fpsAccumulator++;
        //Check if we're ready for another calculation and then proceed
        if (Time.realtimeSinceStartup > fpsNextPeriod)
        {
            currentFps = (int)(fpsAccumulator / fpsMeasurePeriod);
            totalFramesElapsed += fpsAccumulator;
            fpsAccumulator = 0;
            fpsNextPeriod += fpsMeasurePeriod;
            text.text = display + currentFps;

            if(showTotalFrames) 
                text.text += "\n" + framesElapsedDisplay + totalFramesElapsed;

            if (showTimeElapsed)
            {
                int timeElapsed = (int)Time.realtimeSinceStartup;
                text.text += "\n" + timeElapsedDisplay + timeElapsed.ToString(CultureInfo.CurrentCulture) + "s";
            }

            UpdateTextColor(currentFps);           
        }
    }

    private void UpdateTextColor(int fps)
    {
        if (fps > 60)
            text.color = Color.green;
        else if (fps > 20)
            text.color = Color.yellow;
        else
            text.color = Color.red;
    }
}
