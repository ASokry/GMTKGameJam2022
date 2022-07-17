using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaManager : MonoBehaviour
{
    /*
     * Index 0 = Mana 1
     * Index 1 = Mana 5
     * Index 2 = Mana 10
     * Index 3 = Mana 50 (maybe)
    */
    [SerializeField] private List<GameObject> manaPrefabList = new List<GameObject>();
    private int[] manaValues = new int[] { 1, 5, 10, 50 };
    private Dictionary<int, GameObject> manaDictionary = new Dictionary<int, GameObject>();

    [SerializeField] private int totalManaValueToSpawn = 18;
    private List<int> manaValuesToSpawn = new List<int>();
    private int spawnKey = 0;
    private GameObject manaParent;

    private int currentTotalManaValue = 0;
    [SerializeField] private float manaSpawnPadding = 3f;

    private ZoneManager zoneManager;

    private void Awake()
    {
        if (manaPrefabList.Count != manaValues.Length)
        {
            Debug.LogError("Mana Prefab list does not match amount of Mana Values!");
        }
        Portal.OnPortal += OnNewScene;
        zoneManager = GameObject.FindGameObjectWithTag("ZoneManager").GetComponent<ZoneManager>();
        if (zoneManager == null) { Debug.LogError("Zone manager is not in Persistent Scene!"); }
    }

    private void OnNewScene()
    {
        StopCoroutine(ManaSpawnRoutine());
        spawnKey = 0;
        //manaParent = GameObject.FindGameObjectWithTag("ManaParent");
        //if (manaParent == null) { Debug.LogError("Mana Parent is not in other Scene!"); }
        
        manaValuesToSpawn = BreakDownManaValues(totalManaValueToSpawn);
        StartCoroutine(ManaSpawnRoutine());
    }

    private void Start()
    {
        for (int m = 0; m < manaValues.Length; m++)
        {
            manaDictionary.Add(manaValues[m], manaPrefabList[m]);
        }
        OnNewScene();
    }

    private List<int> BreakDownManaValues(int totalManaValue)
    {
        List<int> arr = new List<int>();

        int remainingValue = totalManaValue;
        while (remainingValue > 0)
        {
            int value = 0;
            if (remainingValue >= 50)
            {
                value = manaValues[Random.Range(0, manaValues.Length-1)];
            }
            else if (remainingValue >= 10)
            {
                value = manaValues[Random.Range(0, manaValues.Length-2)];
            }
            else if (remainingValue >= 5)
            {
                value = manaValues[Random.Range(0, manaValues.Length-3)];
            }
            else if (remainingValue >= 1)
            {
                value = manaValues[0];
            }
            
            arr.Add(value);
            remainingValue -= value;
            //print(remainingValue);
        }

        return arr;
    }

    private IEnumerator ManaSpawnRoutine()
    {
        while (spawnKey <= manaValuesToSpawn.Count - 1 && CheckManaRountineScene())
        {
            SpawnMana(manaDictionary[manaValuesToSpawn[spawnKey]], RandomSpawnPointWithinScreen()); // note:change spawn location later
            spawnKey++;
            yield return new WaitForSeconds(1.5f);
        }

        if (!CheckManaRountineScene())
        {
            foreach (GameObject mana in GameObject.FindGameObjectsWithTag("Mana"))
            {
                Destroy(mana);
            }
        }
    }

    private bool CheckManaRountineScene()
    {
        return zoneManager.GetCurrentSceneName() == "wildzone" || zoneManager.GetCurrentSceneName() == "normalzone";
    }

    private Vector2 RandomSpawnPointWithinScreen()
    {
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        Vector2 point = new Vector2(Random.Range(screenBounds.x - manaSpawnPadding, (screenBounds.x- manaSpawnPadding)* -1), Random.Range(screenBounds.y-manaSpawnPadding, (screenBounds.y-manaSpawnPadding) * -1));
        return point;
    }

    private void SpawnMana(GameObject manaPrefab, Vector2 manaSpawnPoint)
    {
        GameObject mana = Instantiate(manaPrefab, manaSpawnPoint, Quaternion.identity);
        //mana.transform.SetParent(manaParent.transform);
    }

    private int CheckNumOfMana()
    {
        GameObject[] allManaInScene = GameObject.FindGameObjectsWithTag("Mana");

        foreach (GameObject manaValue in allManaInScene)
        {
            currentTotalManaValue += manaValue.GetComponent<Mana>().GetManaValue();
        }

        return currentTotalManaValue;
    }
}
