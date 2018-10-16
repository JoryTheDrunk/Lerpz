//Jake Poshepny

// It calculates frames/second over each updateInterval,
// so the display does not keep changing wildly.
// It is also fairly accurate at very low FPS counts (<10).
// We do this not by simply counting frames per interval, but
// by accumulating FPS for each frame. This way we end up with
// correct overall FPS even if the interval renders something like
// 5.5 frames.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{
    public Text FPSText;
    public float updateInterval = 0.5f;

    private float accum = 0f; // FPS accumulated over the interval
    private int frames = 0; // Frames drawn over the interval
    private float timeLeft; // Left time for current interval

    // Use this for initialization
    void Start ()
    {
        FPSText = GetComponent<Text>();

	    if (FPSText == null)
        {
            Debug.LogWarning("FramesPerSecond Needs a Text Component");
            enabled = false;
            return;
        }

        timeLeft = updateInterval;
	}
	
	// Update is called once per frame
	void Update ()
    {
        timeLeft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        ++frames;

        // Interval ended - update GUI text and start new interval
        if (timeLeft <= 0.0)
        {
            // display two fractional digits (f2 format)
            FPSText.text = "" + (accum / frames).ToString("f2");
            timeLeft = updateInterval;
            accum = 0.0f;
            frames = 0;
        }
    }
}
