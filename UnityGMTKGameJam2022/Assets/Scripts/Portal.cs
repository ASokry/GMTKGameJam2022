using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private int sceneIndexNum;
    [SerializeField] private string sceneName;
    public delegate void PortalAction();
    public static event PortalAction OnPortal;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            EnterZone();
        }
    }

    public void EnterZone()
    {
        if(sceneIndexNum <= 0 || sceneIndexNum > SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogError("Not a valid Zone scene. Scene index is out of range!");
        }
        else
        {
            if(OnPortal != null) OnPortal();
            SceneManager.LoadScene(sceneName);
        }
    }

    public void SetPortalSceneIndex(int num)
    {
        sceneIndexNum = num;
    }

    public void SetPortalSceneName(string str)
    {
        sceneName = str;
    }
}
