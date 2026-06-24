using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("Stats")]
    public float moveSpeed = 2f;
    public float attackDamage = 10f;
    public float attackRate = 1f;
    public float health = 100f;
    public bool isPlayerUnit;

    [Header("Detection")]
    public float attackRange = 1.2f;

    private float attackCooldown;
    private bool initialized = false;
    private Unit currentTarget;

    public void Init(bool isPlayer)
    {
        isPlayerUnit = isPlayer;
        initialized = true;
    }

    void Update()
    {
        if (!initialized) return;

        currentTarget = FindNearestEnemy();

        if (currentTarget != null)
        {
            if (attackCooldown <= 0)
            {
                currentTarget.TakeDamage(attackDamage);
                attackCooldown = 1f / attackRate;
            }
        }
        else
        {
            float direction = isPlayerUnit ? 1f : -1f;
            transform.position += new Vector3(0, direction * moveSpeed * Time.deltaTime, 0);
        }

        if (attackCooldown > 0)
            attackCooldown -= Time.deltaTime;
    }

    Unit FindNearestEnemy()
    {
        string enemyTag = isPlayerUnit ? "EnemyUnit" : "PlayerUnit";
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float closestDist = attackRange;
        Unit closest = null;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector2.Distance(transform.position, enemy.transform.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                closest = enemy.GetComponent<Unit>();
            }
        }

        return closest;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(gameObject.name + " trigger entered: " + other.gameObject.name);
        string baseTag = isPlayerUnit ? "EnemyBase" : "PlayerBase";
        if (other.CompareTag(baseTag))
        {
            Debug.Log("Base trigger hit!");
            InvokeRepeating("AttackBase", 0f, 1f / attackRate);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        string baseTag = isPlayerUnit ? "EnemyBase" : "PlayerBase";
        if (other.CompareTag(baseTag))
            CancelInvoke("AttackBase");
    }

    void AttackBase()
    {
        string baseTag = isPlayerUnit ? "EnemyBase" : "PlayerBase";
        GameObject baseObj = GameObject.FindGameObjectWithTag(baseTag);

        if (baseObj != null)
        {
            BaseHealth bh = baseObj.GetComponent<BaseHealth>();
            Debug.Log("BaseHealth found: " + (bh != null ? "YES" : "NO"));
            if (bh != null)
                bh.TakeDamage(attackDamage);
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
            Destroy(gameObject);
    }
}