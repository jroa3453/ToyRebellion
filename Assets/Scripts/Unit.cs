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

    [Header("Crowd Movement")]
    [Tooltip("How far below the normal ground line units may move.")]
    [SerializeField] private float laneBottomOffset = -0.5f;

    [Tooltip("Keep this at 0 so units never move above their starting Y.")]
    [SerializeField] private float laneTopOffset = 0f;

    [SerializeField] private float verticalSpeed = 0.75f;
    [SerializeField] private float avoidanceDistance = 0.45f;
    [SerializeField] private float avoidanceRadius = 0.2f;

    private float attackCooldown;
    private bool initialized;
    private bool wantsToMove;

    private Unit currentTarget;
    private BaseHealth currentBase;

    private UnitHealthBar unitHealthBar;
    private UnitAnimationController unitAnimation;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    private float originalLaneY;
    private float preferredY;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        unitHealthBar = GetComponent<UnitHealthBar>();
        unitAnimation = GetComponent<UnitAnimationController>();
    }

    public void Init(bool isPlayer)
    {
        Debug.Log("Unit initialized. Player unit: " + isPlayer);

        isPlayerUnit = isPlayer;
        initialized = true;

        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = !isPlayer;
        }

        if (unitHealthBar != null)
        {
            unitHealthBar.SetMaxHealth(health);
        }

        originalLaneY = transform.position.y;
        preferredY = originalLaneY;
    }

    private void Update()
    {
        if (!initialized)
        {
            return;
        }

        if (attackCooldown > 0f)
        {
            attackCooldown -= Time.deltaTime;
        }

        currentTarget = FindNearestEnemy();
        currentBase = FindNearestBase();

        float distanceToUnit = Mathf.Infinity;
        if (currentTarget != null)
        {
            distanceToUnit = Vector2.Distance(
                transform.position,
                currentTarget.transform.position
            );
        }

        float distanceToBase = Mathf.Infinity;
        if (currentBase != null)
        {
            distanceToBase = GetDistanceToBase();
        }

        wantsToMove = false;

        if (distanceToUnit <= attackRange)
        {
            // An enemy unit is in range: fight it first.
            StopMoving();
            AttackUnit();
        }
        else if (distanceToBase <= attackRange)
        {
            // No unit in range but the base is: attack the base.
            StopMoving();
            AttackBase();
        }
        else
        {
            // Nothing in range: keep walking.
            wantsToMove = true;
        }
    }

    // Distance to the nearest edge of the base (falls back to its center
    // if the base has no collider).
    private float GetDistanceToBase()
    {
        Collider2D baseCollider = currentBase.GetComponent<Collider2D>();

        if (baseCollider != null)
        {
            Vector2 closestPoint = baseCollider.ClosestPoint(transform.position);
            return Vector2.Distance(transform.position, closestPoint);
        }

        return Vector2.Distance(
            transform.position,
            currentBase.transform.position
        );
    }

    private void FixedUpdate()
    {
        if (!initialized || rb == null)
        {
            return;
        }

        if (wantsToMove)
        {
            MoveForward();
        }
        else
        {
            StopMoving();
        }
    }

    private void MoveForward()
    {
        float forwardDirection = isPlayerUnit ? 1f : -1f;

        float minimumY = originalLaneY + laneBottomOffset;
        float maximumY = originalLaneY + laneTopOffset;

        float targetY = Mathf.Clamp(
            preferredY,
            minimumY,
            maximumY
        );

        // Check for a friendly unit immediately ahead.
        Vector2 checkPosition = rb.position + new Vector2(
            forwardDirection * avoidanceDistance,
            0f
        );

        Collider2D[] nearbyColliders = Physics2D.OverlapCircleAll(
            checkPosition,
            avoidanceRadius
        );

        foreach (Collider2D nearbyCollider in nearbyColliders)
        {
            Unit nearbyUnit = nearbyCollider.GetComponent<Unit>();

            if (nearbyUnit == null || nearbyUnit == this)
            {
                continue;
            }

            if (nearbyUnit.isPlayerUnit == isPlayerUnit)
            {
                preferredY = minimumY;
                targetY = preferredY;
                break;
            }
        }

        float verticalVelocity = 0f;
        float verticalDifference = targetY - rb.position.y;

        if (Mathf.Abs(verticalDifference) > 0.03f)
        {
            verticalVelocity =
                Mathf.Sign(verticalDifference) * verticalSpeed;
        }

        rb.linearVelocity = new Vector2(
            forwardDirection * moveSpeed,
            verticalVelocity
        );

        // Prevent the unit from ever moving above its original Y.
        Vector2 clampedPosition = rb.position;

        clampedPosition.y = Mathf.Clamp(
            clampedPosition.y,
            minimumY,
            maximumY
        );

        rb.position = clampedPosition;
    }

    private void StopMoving()
    {
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void AttackUnit()
    {
        if (attackCooldown > 0f || currentTarget == null)
        {
            return;
        }

        if (unitAnimation != null)
        {
            unitAnimation.PlayAttack();
        }

        currentTarget.TakeDamage(attackDamage);
        attackCooldown = 1f / attackRate;
    }

    private void AttackBase()
    {
        if (attackCooldown > 0f || currentBase == null)
        {
            return;
        }

        if (unitAnimation != null)
        {
            unitAnimation.PlayAttack();
        }

        currentBase.TakeDamage(attackDamage);
        attackCooldown = 1f / attackRate;
    }

    private Unit FindNearestEnemy()
    {
        string enemyTag =
            isPlayerUnit ? "EnemyUnit" : "PlayerUnit";

        GameObject[] enemies =
            GameObject.FindGameObjectsWithTag(enemyTag);

        float closestDistance = Mathf.Infinity;
        Unit closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            Unit enemyUnit = enemy.GetComponent<Unit>();

            if (enemyUnit == null)
            {
                continue;
            }

            float distance = Vector2.Distance(
                transform.position,
                enemy.transform.position
            );

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemyUnit;
            }
        }

        return closestEnemy;
    }

    private BaseHealth FindNearestBase()
    {
        string baseTag =
            isPlayerUnit ? "EnemyBase" : "PlayerBase";

        GameObject baseObject =
            GameObject.FindGameObjectWithTag(baseTag);

        if (baseObject == null)
        {
            return null;
        }

        return baseObject.GetComponent<BaseHealth>();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (unitHealthBar != null)
        {
            unitHealthBar.SetHealth(health);
        }

        // Give one star every time an enemy unit is hit.
        if (!isPlayerUnit)
        {
            UpgradeManager.Stars += 1;
        }

        if (health <= 0f)
        {
            Destroy(gameObject);
        }
    }
}