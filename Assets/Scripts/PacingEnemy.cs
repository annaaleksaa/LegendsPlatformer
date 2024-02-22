using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacingEnemy : MonoBehaviour
{
    /// <summary>
    /// Contains tunable parameters to tweak the enemy's movement.
    /// </summary>
    [System.Serializable]
    public struct Stats
    {
        [Header("Enemy Settings")]
        [Tooltip("How fast the enemy moves.")]
        public float speed;

        [Tooltip("Whether the enemy is currently headed towards point A or point B.")]
        public bool forth;
    }

    public Stats enemyStats;

    [Tooltip("The transform to which the enemy will pace back and forth to.")]
    public Transform pointA, pointB;

    [Tooltip("The audio clip that will play when the enemy attacks the player.")]
    public AudioClip attackSound;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {

        // enemy marches back and forth between its designated targets
        if (enemyStats.forth == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointB.position, enemyStats.speed * Time.deltaTime);
            transform.LookAt(pointB);
            if (Vector3.Distance(transform.position, pointB.position) < 0.01f)
            {
                enemyStats.forth = false;
            }
        }
        if (enemyStats.forth == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointA.position, enemyStats.speed * Time.deltaTime);
            transform.LookAt(pointA);
            if (Vector3.Distance(transform.position, pointA.position) < 0.01f)
            {
                enemyStats.forth = true;
            }
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            audioSource.PlayOneShot(attackSound);
        }
    }
}