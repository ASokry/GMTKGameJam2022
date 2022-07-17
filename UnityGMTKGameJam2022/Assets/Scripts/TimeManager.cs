using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private bool timerActive = false;
    [SerializeField] private int startSecondsTime;

    [Header("Debug")]
    public bool SeeTimer = false;
    [SerializeField] private float timeLeft = 0f;
    [SerializeField] private float zoneStability = 1;
    [SerializeField] private int stage = 0;

    private TextMeshProUGUI currentTimeText;
    private ZoneManager zoneManager;
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
        timeLeft = startSecondsTime;
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
        if (timerActive && timeLeft > 0 && currentTimeText != null)
        {
            currentTimeText.gameObject.SetActive(true);
            timeLeft -= Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(timeLeft);
            currentTimeText.text = time.Minutes.ToString() + ":" + time.Seconds.ToString();
        }
        
        if(currentTimeText != null && SeeTimer)
        {
            currentTimeText.gameObject.SetActive(false);
        }
    }

    private void BorderAnimation()
    {
        if(zoneStability > 0)
        {
            //print("timeLeft " + timeLeft + " <= " + Mathf.Pow(startSecondsTime, zoneStability) + " = " + (timeLeft <= Mathf.Pow(startSecondsTime, zoneStability)));
            if (timeLeft <= Mathf.Pow(startSecondsTime, zoneStability))
            {
                zoneStability -= .2f;
                border.SetTrigger("stage" + ++stage);
            }
        }
        
    }

    public float GetCurrentTime()
    {
        return timeLeft;
    }

    public int GetStartSecondsTime()
    {
        return startSecondsTime;
    }

    public void SetTimer(int time)
    {
        startSecondsTime = time;
        timeLeft = startSecondsTime;
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
