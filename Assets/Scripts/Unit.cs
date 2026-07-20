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
    public float attackRange = 1f;

    private float attackCooldown;
    private bool initialized = false;
    private Unit currentTarget;
    private BaseHealth currentBase;

    private UnitHealthBar unitHealthBar;
    private UnitAnimationController unitAnimation;

    public void Init(bool isPlayer)
    {
        isPlayerUnit = isPlayer;
        initialized = true;

        unitHealthBar = GetComponent<UnitHealthBar>(); 
        unitAnimation = GetComponent<UnitAnimationController>();
        if (unitHealthBar != null)
        {
            unitHealthBar.SetMaxHealth(health);
        }
    }

    void Update()
    {
        if (!initialized) return;

        currentTarget = FindNearestEnemy();
        currentBase = FindNearestBase();

        if (currentTarget != null)
        {
            // Fight enemy unit
            if (attackCooldown <= 0)
            {
                if(unitAnimation != null)
                {
                     unitAnimation.Attack();
                }
                currentTarget.TakeDamage(attackDamage);
                attackCooldown = 1f / attackRate;
            }
        }
        else if (currentBase != null)
        {
            // Attack base
            if (attackCooldown <= 0)
            {
                Debug.Log("Unit reached base attack block");

                if(unitAnimation != null)
                {
                     unitAnimation.Attack();
                }

                currentBase.TakeDamage(attackDamage);
                attackCooldown = 1f / attackRate;
                
                Debug.Log("Attacking base!");
            }
        }
        else
        {
            // Move forward
            float direction = isPlayerUnit ? 1f : -1f;
            transform.position += new Vector3(direction * moveSpeed * Time.deltaTime, 0, 0);
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

    BaseHealth FindNearestBase()
    {
        string baseTag = isPlayerUnit ? "EnemyBase" : "PlayerBase";
        GameObject baseObj = GameObject.FindGameObjectWithTag(baseTag);

        if (baseObj == null) return null;

        float dist = Vector2.Distance(transform.position, baseObj.transform.position);
        if (dist < attackRange * 1.2f)
            return baseObj.GetComponent<BaseHealth>();

        return null;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (unitHealthBar != null)
            unitHealthBar.SetHealth(health);

        if (!isPlayerUnit)
        {
            UpgradeManager.Stars += 1;
        }

        if(health <= 0 && !isPlayerUnit)
        {
            Debug.Log("1 Stars added!");
            UpgradeManager.Stars +=1;
        }
        
        if (health <= 0)
            Destroy(gameObject);
    }
}