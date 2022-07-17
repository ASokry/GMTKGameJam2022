using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private void OnEnable()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Awake()
    {
        ZoneManager.OnLose += SelfDestroy;
        Portal.OnWin += SelfDestroy;
    }

    private void SelfDestroy()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        ZoneManager.OnLose -= SelfDestroy;
        Portal.OnWin -= SelfDestroy;
    }
}
