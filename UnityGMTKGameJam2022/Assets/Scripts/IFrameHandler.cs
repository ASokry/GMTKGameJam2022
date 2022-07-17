using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IFrameHandler : MonoBehaviour
{
    private PlayerStats playerStats;
    
    // Start is called before the first frame update
    void Start()
    {
        playerStats.GetComponentsInParent<PlayerStats>();
    }

    public void SetIframesToFalse()
    {
        playerStats.ResetIFrames();
    }
}
