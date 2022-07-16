using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private bool timerActive = false;
    private float currentTime = 0f;
    [SerializeField] private int startSecondsTime;
    public TextMeshProUGUI currentTimeText;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startSecondsTime;
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
    }

    private void Timer()
    {
        if (timerActive && currentTime > 0)
        {
            currentTimeText.gameObject.SetActive(true);
            currentTime -= Time.deltaTime;
        }
        else
        {
            currentTimeText.gameObject.SetActive(false);
        }

        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = time.Minutes.ToString() + ":" + time.Seconds.ToString();
    }

    public float GetCurrentTime()
    {
        return currentTime;
    }

    public int GetStartSecondsTime()
    {
        return startSecondsTime;
    }

    public void SetTimer(int time)
    {
        startSecondsTime = time;
        currentTime = startSecondsTime;
    }

    public void SetActiveTimer(bool b)
    {
        timerActive = b;
    }

    public bool GetActiveTimer()
    {
        return timerActive;
    }
}
