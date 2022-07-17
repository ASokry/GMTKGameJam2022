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

    [SerializeField] private bool canRoll = true;

    private void Awake()
    {
        Portal.OnPortal += OnNewScene;
    }

    private void OnNewScene()
    {
        canRoll = true;
    }

    private void Start()
    {
        timeManager = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>();
        if (timeManager == null) { Debug.LogError("Time Manager is not in Persistent Scene!"); }
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if (player == null) { Debug.LogError("Player is not in Persistent Scene!"); }
        zoneManager = GameObject.FindGameObjectWithTag("ZoneManager").GetComponent<ZoneManager>();
        if (zoneManager == null) { Debug.LogError("Zone manager is not in Persistent Scene!"); }
    }

    void Update()
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
            //print(timeManager.GetCurrentTime() + ", " + timeManager.GetStartSecondsTime());
            SpawnPortal();
        }
    }

    public void SetCanRollToTrue()
    {
        //print(true);
        canRoll = true;
    }

    private void WildPortalChance()
    {
        if (spawnedPortal != null && canRoll)
        {
            int dice = Mathf.FloorToInt(Random.Range(0f, 100f));
            if (zoneManager.GetCurrentSceneName() == "wildzone")
            {
                spawnedPortal.GetComponent<Animator>().SetBool("isWild", false);
                spawnedPortal.GetComponent<Portal>().SetPortalSceneName("normalzone");
            }
            else if (dice >= Mathf.FloorToInt(timeManager.GetCurrentTime() / timeManager.GetStartSecondsTime() * wildPortalChance))
            {
                //print("wild");
                canRoll = false;
                spawnedPortal.GetComponent<Animator>().SetBool("isWild", true);
                spawnedPortal.GetComponent<Portal>().SetPortalSceneName("wildzone");
            }
            else
            {
                spawnedPortal.GetComponent<Animator>().SetBool("isWild", false);
                spawnedPortal.GetComponent<Portal>().SetPortalSceneName("normalzone");
            }
            print(dice> Mathf.FloorToInt(timeManager.GetCurrentTime() / timeManager.GetStartSecondsTime() * wildPortalChance));
        }
    }

    private void SpawnPortal()
    {
        spawnedPortal = Instantiate(portalPrefab, RandomSpawnPointWithinScreen(), Quaternion.identity);
    }

    private Vector2 RandomSpawnPointWithinScreen()
    {
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        print(screenBounds);
        float xRangeMax = player.position.x > 0 ? player.position.x : screenBounds.x;
        float xRangeMin = player.position.x > 0 ? screenBounds.x*-1 : player.position.x;
        float yRangeMax = player.position.y > 0 ? player.position.y : screenBounds.y;
        float yRangeMin = player.position.y > 0 ? screenBounds.y*-1 : player.position.y;

        Vector2 point = new Vector2(Random.Range(xRangeMin + portalSpawnPadding, xRangeMax - portalSpawnPadding), Random.Range(yRangeMin + portalSpawnPadding, yRangeMax-portalSpawnPadding));
        return point;
    }
}
