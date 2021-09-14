using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private Text timerText;

    private float seconds = 0;
    private int minutes = 0;
    private bool isPaused = false;

    void Awake()
    {
        timerText = gameObject.GetComponent<Text>();
        timerText.text = "00:00";
    }

    void FixedUpdate()
    {
        if (!isPaused)
        {
            seconds += Time.deltaTime;
            if(seconds > 59)
            {
                minutes++;
                seconds = 0;
            }
            timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        }
    }

    public void Pause()
    {
        isPaused = true;
    }

    public void Play()
    {
        isPaused = false;
    }

    public int GetMinutes()
    {
        return minutes;
    }

    public float GetSeconds()
    {
        return seconds;
    }
}
