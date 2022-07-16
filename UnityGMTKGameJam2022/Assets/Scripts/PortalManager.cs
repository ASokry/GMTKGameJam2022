using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    public TimeManager timeManager;
    [SerializeField] private int spawnTimeSeconds = 0;
    [SerializeField] private GameObject portalPrefab;
    private GameObject spawnedPortal = null;

    [SerializeField] private float portalSpawnPadding = 5f;
    [SerializeField] private float wildPortalChance = 150f;

    // Update is called once per frame
    void FixedUpdate()
    {
        SpawnPortalWithinTime(spawnTimeSeconds);
        WildPortalChance();
    }

    private void SpawnPortalWithinTime(int time)
    {
        if (time > timeManager.GetStartSecondsTime())
        {
            Debug.LogError("Given spawn time is more than countdown timer!");
        }
        else if (timeManager.GetCurrentTime() <= timeManager.GetStartSecondsTime() - time && spawnedPortal == null)
        {
            print(timeManager.GetCurrentTime() + ", " + timeManager.GetStartSecondsTime());
            SpawnPortal();
        }
    }

    private void WildPortalChance()
    {
        if (spawnedPortal != null)
        {
            float dice = Random.Range(0f, 100f);
            if (dice > (timeManager.GetCurrentTime()/timeManager.GetStartSecondsTime()) * wildPortalChance)
            {
                print("wild");
                spawnedPortal.GetComponent<Portal>().SetPortalSceneIndex(2);
            }
            else
            {
                print("not wild");
                spawnedPortal.GetComponent<Portal>().SetPortalSceneIndex(1);
            }
        }
    }

    private void SpawnPortal()
    {
        spawnedPortal = Instantiate(portalPrefab, RandomSpawnPointWithinScreen(), Quaternion.identity);
    }

    private Vector2 RandomSpawnPointWithinScreen()
    {
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        Vector2 point = new Vector2(Random.Range(screenBounds.x - portalSpawnPadding, (screenBounds.x - portalSpawnPadding) * -1), Random.Range(screenBounds.y - portalSpawnPadding, (screenBounds.y - portalSpawnPadding) * -1));
        return point;
    }
}
