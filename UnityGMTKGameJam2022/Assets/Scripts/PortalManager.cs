using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    private TimeManager timeManager;
    [SerializeField] private int spawnTimeSeconds = 0;
    [SerializeField] private GameObject portalPrefab;
    private GameObject spawnedPortal = null;

    [SerializeField] private float portalSpawnPadding = 5f;
    [SerializeField] private float wildPortalChance = 150f;

    private Transform player;
    private ZoneManager zoneManager;

    private void Start()
    {
        timeManager = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>();
        if (timeManager == null) { Debug.LogError("Time Manager is not in Persistent Scene!"); }
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if (player == null) { Debug.LogError("Player is not in Persistent Scene!"); }
        zoneManager = GameObject.FindGameObjectWithTag("ZoneManager").GetComponent<ZoneManager>();
        if (zoneManager == null) { Debug.LogError("Zone manager is not in Persistent Scene!"); }
    }

    void FixedUpdate()
    {
        if (zoneManager.GetCurrentSceneName() == "wildzone" || zoneManager.GetCurrentSceneName() == "normalzone")
        {
            SpawnPortalWithinTime(spawnTimeSeconds);
            WildPortalChance();
        }
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
                spawnedPortal.GetComponent<Portal>().SetPortalSceneName("wildzone");
            }
            else
            {
                print("not wild");
                spawnedPortal.GetComponent<Portal>().SetPortalSceneName("normalzone");
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
        float xRangeMax = player.position.x > 0 ? player.position.x : screenBounds.x * -1;
        float xRangeMin = player.position.x > 0 ? screenBounds.x : player.position.x;
        float yRangeMax = player.position.y > 0 ? player.position.y : screenBounds.y * -1;
        float yRangeMin = player.position.y > 0 ? screenBounds.y : player.position.y;

        Vector2 point = new Vector2(Random.Range(xRangeMin - portalSpawnPadding, xRangeMax + portalSpawnPadding), Random.Range(yRangeMin - portalSpawnPadding, yRangeMax+portalSpawnPadding));
        return point;
    }
}
