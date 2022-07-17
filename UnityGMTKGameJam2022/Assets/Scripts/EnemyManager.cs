using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int numOfEnemies = 1;
    [SerializeField] private float spawnBoxSize = 10f;
    [SerializeField] private float padding = 5f;
    private int zoneCount = 0;
    private List<int> enemyProgressChart = new List<int>()
    {
        2,2,3,3,3,3,4,4,4,5
    };

    private void Awake()
    {
        Portal.OnPortal += OnNewScene;
        numOfEnemies = enemyProgressChart[zoneCount];
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private void OnNewScene()
    {
        zoneCount++;
        zoneCount = Mathf.Clamp(zoneCount, 0, enemyProgressChart.Count-1);
        numOfEnemies = enemyProgressChart[zoneCount];
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(1.5f);
        for (int i=0; i<numOfEnemies; i++)
        {
            SpawnEnemy(enemyPrefab, RandomSpawnPointOutOfScreen());
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void SpawnEnemy(GameObject enemy, Vector2 spawnPoint)
    {
        Instantiate(enemy, spawnPoint, Quaternion.identity);
    }

    private Vector2 RandomSpawnPointOutOfScreen()
    {
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        List<Vector2> points = new List<Vector2>()
        {
            new Vector2(Random.Range(screenBounds.x * spawnBoxSize, screenBounds.x) + padding, Random.Range(screenBounds.y , screenBounds.y )),
            new Vector2(Random.Range(screenBounds.x * -1, screenBounds.x * -1 * spawnBoxSize) - padding, Random.Range(screenBounds.y, screenBounds.y)),
            new Vector2(Random.Range(screenBounds.x, screenBounds.x * -1), Random.Range(screenBounds.y * -1, screenBounds.y*-1*spawnBoxSize) - padding),
            new Vector2(Random.Range(screenBounds.x, screenBounds.x * -1), Random.Range(screenBounds.y * spawnBoxSize, screenBounds.y) + padding)
        };

        Vector2 point = points[Random.Range(0, points.Count - 1)];

        return point;
    }
}
