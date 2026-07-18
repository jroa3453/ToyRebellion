using UnityEngine;
using TMPro;
using System.Collections;

public class UnitSpawner : MonoBehaviour
{
    [Header("Difficulty")]
    public float enemyDifficultyMultiplier = 1f;
    public int currentWave = 1;
    public bool Level1 = false;
    public bool Level2 = false;
    public bool wave1Spawned = false;
    public bool wave2Spawned = false;
    public bool finalWaveSpawned = false;
    public TMP_Text waveClearedText;

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


    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemyUnit");
        if(enemies.Length == 0 && Level1 == false && wave1Spawned == true)
        {
            Debug.Log("Wave 2 starting!");
            StartCoroutine(SpawnWave2());
            Level1 = true;
        }
        if (enemies.Length == 0 && Level1 == true && Level2 == false && wave2Spawned == true)
        {
            Debug.Log("Final Wave starting!");
            StartCoroutine(SpawnFinalWave());
            Level2 = true;
        }
    }

    void Start()
    {
        StartCoroutine(LevelIntro());
    }

    IEnumerator LevelIntro()
    {
        yield return new WaitForSeconds(2f);
        waveClearedText.gameObject.SetActive(true);
        waveClearedText.text = "Level 1";
        yield return new WaitForSeconds(4f);
        waveClearedText.gameObject.SetActive(false);
        yield return StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(3f);
        waveClearedText.gameObject.SetActive(true);
        waveClearedText.text = "Wave 1";
        yield return new WaitForSeconds(4f);
        waveClearedText.gameObject.SetActive(false);
        SpawnEnemyUnit();
        yield return new WaitForSeconds(7f);
        SpawnEnemyUnit();
        yield return new WaitForSeconds(10f);
        SpawnEnemyUnit();
        SpawnEnemyUnit();
        SpawnEnemyUnit();
        wave1Spawned = true;
    }

    IEnumerator SpawnWave2()
    {
        waveClearedText.gameObject.SetActive(true);
        waveClearedText.text = "Wave 2";
        yield return new WaitForSeconds(4f);
        waveClearedText.gameObject.SetActive(false);
        SpawnEnemyUnit();
        SpawnEnemyUnit();
        SpawnEnemyUnit(); 
        yield return new WaitForSeconds(5f);
        SpawnEnemyUnit();
        SpawnEnemyUnit(); 
        SpawnEnemyUnit();
        SpawnEnemyUnit();
        wave2Spawned = true;
    }

    IEnumerator SpawnFinalWave()
    {
        waveClearedText.gameObject.SetActive(true);
        waveClearedText.text = "Final Wave";
        yield return new WaitForSeconds(4f);
        waveClearedText.gameObject.SetActive(false);
        SpawnEnemyUnit();
        SpawnEnemyUnit();
        SpawnEnemyUnit();
        SpawnEnemyUnit(); 
        yield return new WaitForSeconds(5f);
        SpawnEnemyUnit();
        SpawnEnemyUnit(); 
        SpawnEnemyUnit();
        SpawnEnemyUnit();
        SpawnEnemyUnit();
        SpawnEnemyUnit();
        finalWaveSpawned = true;
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
        {
            if (!isPlayer)
            {
                unitScript.health *= enemyDifficultyMultiplier;
                unitScript.attackDamage *= enemyDifficultyMultiplier;
            }
            else
            {
                if (prefab == plushiePrefab)
                {
                    unitScript.health *= UpgradeManager.GetUpgradeMultiplier(UpgradeManager.PlushieHPLevel);
                    unitScript.attackDamage *= UpgradeManager.GetUpgradeMultiplier(UpgradeManager.PlushieDPSLevel);
                    unitScript.attackRate *= UpgradeManager.GetUpgradeMultiplier(UpgradeManager.PlushieAttackSPDLevel);
                }
                else if (prefab == actionFigPrefab)
                {
                    unitScript.health *= UpgradeManager.GetUpgradeMultiplier(UpgradeManager.ActionFigHPLevel);
                    unitScript.attackDamage *= UpgradeManager.GetUpgradeMultiplier(UpgradeManager.ActionFigDPSLevel);
                    unitScript.attackRate *= UpgradeManager.GetUpgradeMultiplier(UpgradeManager.ActionFigAttackSPDLevel);
                }
                else if (prefab == robotToyPrefab)
                {
                    unitScript.health *= UpgradeManager.GetUpgradeMultiplier(UpgradeManager.RobotToyHPLevel);
                    unitScript.attackDamage *= UpgradeManager.GetUpgradeMultiplier(UpgradeManager.RobotToyDPSLevel); 
                    unitScript.attackRate *= UpgradeManager.GetUpgradeMultiplier(UpgradeManager.RobotToyAttackSPDLevel);
                }   
            }
             unitScript.Init(isPlayer);
        }
        if (!isPlayer)
        {
            SpriteRenderer sr = unit.GetComponent<SpriteRenderer>();
            if (sr != null)
                sr.color = Color.red;
        }
    }
}