using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZoneManager : MonoBehaviour
{
    public TimeManager timeManager;

    private void Update()
    {
        TimesUp();
    }

    private void TimesUp()
    {
        if (timeManager.GetCurrentTime() <= 0 && timeManager.GetActiveTimer())
        {
            GoToLoseScene();
        }
    }

    private void GoToLoseScene()
    {
        SceneManager.LoadScene(3); // Go to lose scene
    }
}
