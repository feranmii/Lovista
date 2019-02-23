using System.Collections;
using System.Collections.Generic;
using EventCallbacks;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : ObjectPhysics, IDammageable
{

    [SerializeField]
    enum EnemyType
    {
        Slow,
        Fast
    }

    [SerializeField] private EnemyType enemyType;

    [Header("Enemy Stats")] public EnemyStats stats;
    public int health;
    private float launch = 1;
    public float launchPower = 10;
    public float direction = 1;
    public float changeDirectionEase = 1;
    private float directionSmooth = 1;
    private Vector2 originalLocalScale;
   
    public bool isDead;
    public GameObject deathParticles;
    public bool jump;
    private float playerDifference;
    public float recoveryCounter;
    public bool recovering;
    public float recoveryTime;
    private bool followPlayer;
    public float followRange;
    [FormerlySerializedAs("attackRange")] public float attackRate;

    public GameObject hitBox;
    [SerializeField]
    private LayerMask layerMask;
    public GameObject graphic;
    public Animator animator;
    public bool jumping;
    
    [SerializeField] private Vector2 rayCastOffset;
    private RaycastHit2D rightWall;
    private RaycastHit2D leftWall;

    private float t;

    private void Start()
    {
        originalLocalScale = transform.localScale;
        health = stats.maxHealth;
    }
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, followRange);
    }


    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;
        playerDifference = PlayerManager.Instance.gameObject.transform.position.x - transform.position.x;
        directionSmooth += (direction - directionSmooth) * Time.deltaTime * changeDirectionEase;

        if (!isDead && !recovering)
        {
            move.x = 1 * directionSmooth;
            if (move.x > 0.01f) {
                if (graphic.transform.localScale.x == -originalLocalScale.x) {
                    graphic.transform.localScale = new Vector3 (originalLocalScale.x, transform.localScale.y, transform.localScale.z);
                }
            } else if (move.x < -0.01f) {
                if (graphic.transform.localScale.x >= originalLocalScale.x) {
                    graphic.transform.localScale = new Vector3 (-originalLocalScale.x, transform.localScale.y, transform.localScale.z);                }
            }
            //Check if the player is within range to follow

            if (enemyType == EnemyType.Slow)
            {
                if (Mathf.Abs(playerDifference) < followRange)
                {
                    followPlayer = true;
                }
                else
                {
                    followPlayer = false;
                }
                
                
                if (Mathf.Abs(playerDifference) < attackRate)
                {
                    t += Time.deltaTime;
                    if (t > attackRate)
                    {
                        t = 0;
                        StopCoroutine(Attack());
                        StartCoroutine(Attack());
                    }
                }
                
            }

            if (followPlayer)
            {
                if (playerDifference < 0)
                {
                    direction = -1;
                }
                else
                {
                    direction = 1;
                }
            }
            else
            {
                //Allow enemy to instantly change direction when not following
                directionSmooth = direction;
            }
            
            rightWall = Physics2D.Raycast (new Vector2 (transform.position.x + rayCastOffset.x, transform.position.y + rayCastOffset.y), Vector2.right, 1f, layerMask);
            Debug.DrawRay (new Vector2 (transform.position.x, transform.position.y + rayCastOffset.y), Vector2.right, Color.yellow);

            if (rightWall.collider != null) {
                if (!followPlayer) {
                    direction = -1;
                } else {
                    Jump ();
                }

            }

            leftWall = Physics2D.Raycast (new Vector2 (transform.position.x - rayCastOffset.x, transform.position.y + rayCastOffset.y), Vector2.left, 1f, layerMask);
            Debug.DrawRay (new Vector2 (transform.position.x, transform.position.y + rayCastOffset.y), Vector2.left, Color.blue);

            if (leftWall.collider != null) {
                if (!followPlayer) {
                    direction = 1;
                } else {
                    Jump ();
                }
            }
            
        } 
        else if (recovering)
        {
            recoveryCounter += Time.deltaTime;
            move.x = launch;
            launch += (0 - launch) * Time.deltaTime;
            if (recoveryCounter >= recoveryTime)
            {
                recoveryCounter = 0;
                recovering = false;
            }
        }
        

        targetVelocity = move * stats.maxSpeed;
    }

    public IEnumerator Attack()
    {
        hitBox.gameObject.SetActive(true);
        yield return  new WaitForSeconds(.5f);
        hitBox.gameObject.SetActive(false);
        
    }

    public void Damage(int amount, int launchDirection)
    {
        //do shake
        health -= amount;
        //set hurt anim
        animator.SetTrigger ("hurt");

        velocity.y = launchPower / 2f;
        launch = launchDirection * (launchPower / 5);
        recoveryCounter = 0;
        recovering = true;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Jump()
    {
        if (grounded)
        {
            velocity.y = stats.jumpTakeOffSpeed;
            //Play sound
        }
    }

    public void Die()
    {
        deathParticles.SetActive(true);
        deathParticles.transform.parent = transform.parent;
        isDead = true;
        OnEnemyDie ed = new OnEnemyDie
        {
            val = 1,
            en = this
        };
        
        
        EventManager.Instance.FireEvent(ed);
        
        Destroy(gameObject);
    }
    

    
}
