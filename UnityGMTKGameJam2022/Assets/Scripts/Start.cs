using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("normalzone");
        SceneManager.LoadScene("Persistent_Objects", LoadSceneMode.Additive);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene("Start");
    }
}
