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
    private TextMeshProUGUI currentTimeText;
    private ZoneManager zoneManager;
    public bool SeeTimer = false;

    private void Awake()
    {
        Portal.OnPortal += OnNewScene;
        currentTimeText = GameObject.FindGameObjectWithTag("TimeText").GetComponent<TextMeshProUGUI>();
        if (currentTimeText == null) { Debug.LogError("Time text is not in Persistent Scene!"); }
        zoneManager = GameObject.FindGameObjectWithTag("ZoneManager").GetComponent<ZoneManager>();
        if (zoneManager == null) { Debug.LogError("Zone manager is not in Persistent Scene!"); }
        OnNewScene();
    }

    private void OnNewScene()
    {
        currentTime = startSecondsTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (zoneManager.GetCurrentSceneName() == "wildzone" || zoneManager.GetCurrentSceneName() == "normalzone")
        {
            Timer();
        }
    }

    private void Timer()
    {
        if (timerActive && currentTime > 0 && currentTimeText != null)
        {
            currentTimeText.gameObject.SetActive(true);
            currentTime -= Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(currentTime);
            currentTimeText.text = time.Minutes.ToString() + ":" + time.Seconds.ToString();
        }
        
        if(currentTimeText != null && SeeTimer)
        {
            currentTimeText.gameObject.SetActive(false);
        }
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
