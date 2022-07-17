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
    public static event PortalAction OnWin;
    [SerializeField] private bool needsToRoll = false;

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
            if (sceneName == "Win")
            {
                if (OnWin != null) OnWin();
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                SceneManager.LoadScene(sceneName);
                if (OnPortal != null) OnPortal();
            }
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

    public void SetRollToTrue()
    {
        if (needsToRoll)
        {
            PortalManager portalManager = GameObject.FindGameObjectWithTag("PortalManager").GetComponent<PortalManager>();
            portalManager.SetCanRollToTrue();
        }
    }
}
