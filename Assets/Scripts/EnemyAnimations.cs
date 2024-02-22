using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    private Animator anim;
    private BasicEnemy basicEnemyScript;

    private void Start()
    {
        anim = GetComponent<Animator>();
        basicEnemyScript = GetComponent<BasicEnemy>();
    }

    private void Update()
    {
        if (basicEnemyScript.enemyStats.idle == false)
        {
            anim.speed = 2;
        }
    }
}