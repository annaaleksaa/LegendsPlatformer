using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
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

        [Tooltip("How fast the enemy runs after you (only when idle is false).")]
        public float chaseSpeed;

        [Tooltip("Whether the enemy is idle or not. Once the player is within distance, idle will turn false and the enemy will chase the player.")]
        public bool idle;
    }

    public Stats enemyStats;

    [Tooltip("The transform that will lock onto the player once the enemy has spotted them.")]
    public Transform target;

    [Tooltip("The audio clip that will play when the enemy spots the player.")]
    public AudioClip alertedSound;

    [Tooltip("The audio clip that will play when the enemy attacks the player.")]
    public AudioClip attackSound;

    private AudioSource audioSource;
    private bool alerted = false;
    private bool slipping = false;
    private float facing;
    private Rigidbody rb;
    private GameObject player;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {

        // changes the enemy's behavior: pacing in circles or chasing the player
        if (enemyStats.idle == true)
        {
            transform.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * enemyStats.rotateSpeed, Space.Self);
            transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * enemyStats.walkSpeed, Space.Self);
        }
        else if (enemyStats.idle == false)
        {
            target.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            transform.LookAt(target);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * enemyStats.chaseSpeed);
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

        if (other.gameObject.tag == "Player")
        {
            audioSource.PlayOneShot(attackSound);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject;
            enemyStats.idle = false;

            if (alerted == false)
            {
                alerted = true;
                audioSource.PlayOneShot(alertedSound);
            }
        }
    }
}