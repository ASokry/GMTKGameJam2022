using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [Range(1,2)]
    [SerializeField] private int sceneIndexNum;

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
            SceneManager.LoadScene(sceneIndexNum);
        }
    }
}
