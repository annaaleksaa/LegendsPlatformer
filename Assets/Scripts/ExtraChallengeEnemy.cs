using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraChallengeEnemy : MonoBehaviour
{
    public Stats enemyStats;

    [Tooltip("The transform to which the enemy will pace back and forth to.")]
    public Transform[] patrolPoints;

    private int currentPatrolPoint = 1;
    public int speed = 10;

    /// <summary>
    /// Contains tunable parameters to tweak the enemy's movement.
    /// </summary>
    [System.Serializable]
    public struct Stats
    {
        [Header("Enemy Settings")]

        [Tooltip("How fast the enemy moves.")]
        public int speed ;

        [Tooltip("Whether the enemy should move or not")]
        public bool move;

    }

    void Update()
    {
        
        //if the enemy is allowed to move
        if (enemyStats.move = true)
        {
            Vector3 moveToPoint = patrolPoints[currentPatrolPoint].position;
            transform.position = Vector3.MoveTowards(transform.position, moveToPoint, enemyStats.speed * Time.deltaTime);
           
            if (Vector3.Distance(transform.position, moveToPoint) < 0.01f)
            {
                currentPatrolPoint++;
                
                if (currentPatrolPoint >= patrolPoints.Length)
                {
                    currentPatrolPoint = 0;
                }
            
            }
        }
        
    }
}
