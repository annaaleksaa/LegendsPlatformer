using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    public Stats platformStats;

    [Tooltip("The transform of which the platform will move back and forth to.")]
    public Transform pointA, pointB;

    GameObject player;

    /// <summary>
    /// Contains tunable parameters to tweak the platforms's movement and timing.
    /// </summary>
    [System.Serializable]
    public struct Stats
    {
        [Header("Moving Platform Settings")]
        [Tooltip("How fast the platform moves.")]
        public float speed;

        [Tooltip("How long the platform waits before moving again.")]
        public float waitTime;

        [Tooltip("Whether the platform is headed towards point A or point B.")]
        public bool forth;
    }
    void Update()
    {
        // platform glides back and for between two designated targets, taking time to stop upon arriving
        if (platformStats.forth == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointB.position, platformStats.speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, pointB.position) < 0.01f)
            {
                StartCoroutine("Go");
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pointA.position, platformStats.speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, pointA.position) < 0.01f)
            {
                StartCoroutine("Back");
            }
        }
    }

    IEnumerator Go()
    {
        yield return new WaitForSeconds(platformStats.waitTime);
        platformStats.forth = false;
    }
    IEnumerator Back()
    {
        yield return new WaitForSeconds(platformStats.waitTime);
        platformStats.forth = true;
    }
}