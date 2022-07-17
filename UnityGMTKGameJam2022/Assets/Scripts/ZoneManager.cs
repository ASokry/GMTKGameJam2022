using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZoneManager : MonoBehaviour
{
    private TimeManager timeManager;

    private void Start()
    {
        timeManager = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>();
        if(timeManager == null) { Debug.LogError("Time Manager is not in Persistent Scene!"); }
    }

    private void Update()
    {
        TimesUp();
    }

    private void TimesUp()
    {
        if (timeManager.GetCurrentTime() <= 0 && timeManager.GetActiveTimer() && GetCurrentSceneName() != "Lose")
        {
            GoToLoseScene();
        }
    }

    public static void GoToLoseScene()
    {
        SceneManager.LoadScene("Lose"); // Go to lose scene
    }

    public string GetCurrentSceneName()
    {
        //print(SceneManager.GetActiveScene().name);
        return SceneManager.GetActiveScene().name;
    }
}
