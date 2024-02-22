using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeEnemy : MonoBehaviour
{
    /// <summary>
    /// Contains tunable parameters to tweak the enemy's movement.
    /// </summary>
    [System.Serializable]
    public struct Stats
    {
        [Tooltip("Should the Enemy keep jumping")]
        public bool shouldJump;

        [Tooltip("How high the enemy jumps")]
        public float jumpVelocity;

        [Tooltip("How close the enemy needs to be to explode")]
        public float explodeDist;
    }

    [Tooltip("The basic stats of our enemy")]
    public Stats enemyStats;

    [Tooltip("Blue explosion particles")]
    public GameObject enemyExplosionParticles;

    [Tooltip("Reference to the player GameObject")]
    public Transform playerTransform;

    private Rigidbody rb;

    private bool exploded = false;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        //Start jump in place coroutine
        StartCoroutine("Jump");
    }

    private void Update()
    {
        {
            
              //Check to see how close the player is to the enemy
                if (Vector3.Distance(transform.position, playerTransform.position) < enemyStats.explodeDist)
                {
                    //Explode if player is within range
                    StartCoroutine("Explode");
                }
        }
    }

    //Explode Coroutine
    private IEnumerator Explode()
    {
        Instantiate(enemyExplosionParticles, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(0.2f);

        Destroy(transform.parent.gameObject);
    }

    //Coroutine that allows the enemy to jump in place
    private IEnumerator Jump()
    {
        while (enemyStats.shouldJump)
        {
            rb.AddForce(new Vector3(0, enemyStats.jumpVelocity, 0));
            yield return new WaitForSeconds(1f);
        }
    }
}

























//-----------SOLUTION SCRIPT------------------
public class ExplodeEnemy_Solution : MonoBehaviour
{
    /// <summary>
    /// Contains tunable parameters to tweak the enemy's movement.
    /// </summary>
    [System.Serializable]
    public struct Stats
    {
        [Tooltip("Should the Enemy keep jumping")]
        public bool shouldJump;

        [Tooltip("How high the enemy jumps")]
        public float jumpVelocity;

        [Tooltip("How close the enemy needs to be to explode")]
        public float explodeDist;
    }

    [Tooltip("The basic stats of our enemy")]
    public Stats enemyStats;

    [Tooltip("Blue explosion particles")]
    public GameObject enemyExplosionParticles;

    [Tooltip("Reference to the player GameObject")]
    public Transform playerTransform;

    private Rigidbody rb;

    private bool exploded = false;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        //Start jump in place coroutine
        StartCoroutine("Jump");
    }

    void Update()
    {
        if (!exploded)
        {
            //Check to see how close the player is to the enemy
            if (Vector3.Distance(transform.position, playerTransform.position) <= enemyStats.explodeDist)
            {
                //Explode if player is within range
                StartCoroutine("Explode");
            }
        }
    }

    //Explode Coroutine
    private IEnumerator Explode()
    {
        exploded = true;

        Instantiate(enemyExplosionParticles, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(0.2f);

        Destroy(transform.parent.gameObject);
    }

    //Coroutine that allows the enemy to jump in place
    private IEnumerator Jump()
    {
        while (enemyStats.shouldJump)
        {
            rb.AddForce(new Vector3(0, enemyStats.jumpVelocity, 0));
            yield return new WaitForSeconds(1f);
        }
    }
}