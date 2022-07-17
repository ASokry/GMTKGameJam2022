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
    private Animator border;

    private void Awake()
    {
        Portal.OnPortal += OnNewScene;
        currentTimeText = GameObject.FindGameObjectWithTag("TimeText").GetComponent<TextMeshProUGUI>();
        if (currentTimeText == null) { Debug.LogError("Time text is not in Persistent Scene!"); }
        zoneManager = GameObject.FindGameObjectWithTag("ZoneManager").GetComponent<ZoneManager>();
        if (zoneManager == null) { Debug.LogError("Zone manager is not in Persistent Scene!"); }
        border = GameObject.FindGameObjectWithTag("Border").GetComponent<Animator>();
        OnNewScene();
    }

    private void OnNewScene()
    {
        currentTime = startSecondsTime;
        border.SetTrigger("stage1");
    }

    // Update is called once per frame
    void Update()
    {
        if (zoneManager.GetCurrentSceneName() == "wildzone" || zoneManager.GetCurrentSceneName() == "normalzone")
        {
            Timer();
            BorderAnimation();
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

    private void BorderAnimation()
    {
        int percentage = Mathf.FloorToInt(currentTime / startSecondsTime * 100);
        switch (percentage)
        {
            case 100:
                border.SetTrigger("stage1");
                break;
            case 80:
                border.SetTrigger("stage2");
                break;
            case 60:
                border.SetTrigger("stage3");
                break;
            case 40:
                border.SetTrigger("stage4");
                break;
            case 20:
                border.SetTrigger("stage5");
                break;
            default:
                break;
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
