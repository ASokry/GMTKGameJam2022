using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    private PlayerStats playerStats;
    public bool canTeloport = false;
    private PlayerInputActions playerInputActions;
    
    [SerializeField] private GameObject exitPortalPrefab;
    private GameObject exitPortal;
    [SerializeField] private Transform exitPortalSpawnPoint;
    [SerializeField] private float radius = 3.5f;

    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Create.Enable();
        playerInputActions.Player.Create.started += CreateExitPortal;
    }

    private void CreateExitPortal(InputAction.CallbackContext context)
    {
        if ((playerStats.IsManaAtMax() || canTeloport) && exitPortal == null)
        {
            Vector2 spawnPoint = exitPortalSpawnPoint != null ? new Vector2(exitPortalSpawnPoint.position.x, exitPortalSpawnPoint.position.y) : GetRandomPortalPoint();
            playerStats.LoseMana(playerStats.GetPlayerMana());
            exitPortal = Instantiate(exitPortalPrefab, spawnPoint, Quaternion.identity);
        }
    }

    private Vector2 GetRandomPortalPoint()
    {
        Vector2 randomPoint = (Vector2)transform.position + Random.insideUnitCircle * radius;
        while(Vector2.Distance(transform.position, randomPoint) <= 2.5f)
        {
            randomPoint = Random.insideUnitCircle * radius;
        }

        return randomPoint;
    }
}
