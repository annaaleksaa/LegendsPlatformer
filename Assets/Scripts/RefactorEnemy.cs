using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefactorEnemy : MonoBehaviour
{
    public Stats enemyStats;

    [Tooltip("The transform that will lock onto the player once the enemy has spotted them.")]
    public Transform sight;

    [Tooltip("The transform to which the enemy will pace back and forth to.")]
    public Transform[] patrolPoints;

    [Tooltip("Blue explosion particles")]
    public GameObject enemyExplosionParticles;

    public int currentPatrolPoint = 0;

    public bool slipping = false;
   
    public float facing;
    
    public Rigidbody rb;

    private GameObject player;

    /// <summary>
    /// Contains tunable parameters to tweak the enemy's movement and behavior.
    /// </summary>
    [System.Serializable]
    public struct Stats
    {
        [Header("Enemy Settings")]
        [Tooltip("How fast the enemy walks (only when idle is true).")]
        public float walkSpeed;

        [Tooltip("How fast the enemy turns in circles as they're walking (only when idle is true).")]
        public float rotateSpeed;

        [Tooltip("How fast the enemy runs after the player (only when idle is false).")]
        public float chaseSpeed;

        [Tooltip("Whether the enemy is idle or not. Once the player is within distance, idle will turn false and the enemy will chase the player.")]
        public bool idle;

        [Tooltip("How close the enemy needs to be to explode")]
        public float explodeDist;

    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        // changes the enemy's behavior: pacing in circles or chasing the player
        if (enemyStats.idle == true)
        {
            //Patrol Logic
                Vector3 moveToPoint = patrolPoints[currentPatrolPoint].position;
                transform.position = Vector3.MoveTowards(transform.position, moveToPoint, enemyStats.walkSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, moveToPoint) < 0.01f)
                {
                    currentPatrolPoint++;
                    if (currentPatrolPoint > patrolPoints.Length - 1)
                    {
                        currentPatrolPoint = 0;
                    }
                }
        }
        else if (enemyStats.idle == false)
        {
            //Chase the player
             sight.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
             transform.LookAt(sight);
             transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * enemyStats.chaseSpeed);
           
            //Explode if we get within the enemyStats.explodeDist
            if (Vector3.Distance(transform.position, player.transform.position) < enemyStats.explodeDist)
            {
                StartCoroutine("Explode");
                enemyStats.idle = true;
            }
        }

        // stops enemy from following player up the inaccessible slopes
        if (slipping == true)
        {
            transform.Translate(Vector3.back * 20 * Time.deltaTime, Space.World);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 9)
        {
            slipping = true;
        }
        else
        {
            slipping = false;
        }
    }


   private void OnTriggerEnter(Collider other)
    {
        //start chasing if the player gets close enough
        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject;
            enemyStats.idle = false;
        }
    }

   private void OnTriggerExit(Collider other)
    {
        //stop chasing if the player gets far enough away
        if (other.gameObject.tag == "Player")
        {
            enemyStats.idle = true;      
        }
    }

    private IEnumerator Explode()
    {
        GameObject particles = Instantiate(enemyExplosionParticles, transform.position, new Quaternion());
        yield return new WaitForSeconds(0.2f);
        Destroy(transform.parent.gameObject);
    }


}