using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [Header("Spawn Points")]
    public Transform playerSpawnPoint;
    public Transform enemySpawnPoint;

    [Header("Unit Prefabs")]
    public GameObject plushiePrefab;    // cheap, weak
    public GameObject actionFigPrefab;  // balanced
    public GameObject robotToyPrefab;   // expensive, strong

    [Header("Unit Costs")]
    public float plushieCost = 10f;
    public float actionFigCost = 25f;
    public float robotToyCost = 40f;

    [Header("Enemy AI")]
    public float enemySpawnInterval = 3f;
    private float enemySpawnTimer;

    void Update()
    {
        // Simple enemy AI — spawns random units over time
        enemySpawnTimer -= Time.deltaTime;
        if (enemySpawnTimer <= 0)
        {
            SpawnEnemyUnit();
            enemySpawnTimer = enemySpawnInterval;
        }
    }

    // Called by UI buttons
    public void SpawnPlushie()
    {
        if (BatteryManager.Instance.SpendBattery(plushieCost))
            SpawnUnit(plushiePrefab, playerSpawnPoint.position, true);
        else
            Debug.Log("Not enough battery for Plushie!");
    }

    public void SpawnActionFig()
    {
        if (BatteryManager.Instance.SpendBattery(actionFigCost))
            SpawnUnit(actionFigPrefab, playerSpawnPoint.position, true);
        else
            Debug.Log("Not enough battery for Action Figure!");
    }

    public void SpawnRobotToy()
    {
        if (BatteryManager.Instance.SpendBattery(robotToyCost))
            SpawnUnit(robotToyPrefab, playerSpawnPoint.position, true);
        else
            Debug.Log("Not enough battery for Robot Toy!");
    }

    void SpawnEnemyUnit()
    {
        // Enemy randomly picks a unit to spawn
        int rand = Random.Range(0, 3);
        GameObject prefab = rand == 0 ? plushiePrefab :
                           rand == 1 ? actionFigPrefab : robotToyPrefab;
        SpawnUnit(prefab, enemySpawnPoint.position, false);
    }

    void SpawnUnit(GameObject prefab, Vector3 position, bool isPlayer)
    {
        if (prefab == null) return;
        
        GameObject unit = Instantiate(prefab, position, Quaternion.identity);
        
        // Set tag on root AND all children
        string tag = isPlayer ? "PlayerUnit" : "EnemyUnit";
        unit.tag = tag;
        foreach (Transform child in unit.transform)
            child.tag = tag;
        
        Unit unitScript = unit.GetComponent<Unit>();
        if (unitScript != null)
            unitScript.Init(isPlayer);

        if (!isPlayer)
        {
            SpriteRenderer sr = unit.GetComponent<SpriteRenderer>();
            if (sr != null)
                sr.color = Color.red;
        }
    }
}