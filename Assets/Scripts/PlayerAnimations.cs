using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator anim;
    private PlayerMovement playerMovementScript;
    private StompEnemies stompEnemiesScript;
    private GetHit getHitScript;

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerMovementScript = GetComponent<PlayerMovement>();
        stompEnemiesScript = GetComponent<StompEnemies>();
        getHitScript = GetComponent<GetHit>();
    }

    private void Update()
    {
        // Plays the running animation when holding down the move keys.
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            anim.SetBool("Running", true);
        }
        else
        {
            anim.SetBool("Running", false);
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (playerMovementScript.playerStats.canJump == true)
            {
                anim.SetBool("Grounded", false);
                anim.SetTrigger("Jumped");
            }
        }

        // Linecasts determine when to play the landing animation.
        bool hitL = Physics.Linecast(new Vector3(playerMovementScript.groundCheckL.position.x, transform.position.y + 1, transform.position.z), playerMovementScript.groundCheckL.position, playerMovementScript.groundLayer);
        bool hitR = Physics.Linecast(new Vector3(playerMovementScript.groundCheckR.position.x, transform.position.y + 1, transform.position.z), playerMovementScript.groundCheckR.position, playerMovementScript.groundLayer);
        bool stomp = Physics.Linecast(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), stompEnemiesScript.stompCheck.position, stompEnemiesScript.enemyHeads);
        Debug.DrawLine(new Vector3(playerMovementScript.groundCheckL.position.x, transform.position.y + 1, transform.position.z), playerMovementScript.groundCheckL.position, Color.red);
        Debug.DrawLine(new Vector3(playerMovementScript.groundCheckR.position.x, transform.position.y + 1, transform.position.z), playerMovementScript.groundCheckR.position, Color.red);
        Debug.DrawLine(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), stompEnemiesScript.stompCheck.position, Color.red);
        if (hitL || hitL)
        {
            anim.SetBool("Grounded", true);
            anim.SetTrigger("Landed");
        }
        else
        {
            anim.SetBool("Grounded", false);
        }

        if (stomp)
        {
            anim.SetTrigger("Jumped");
        }
    }

    // Plays the hit animation after running into an enemy and taking damage.
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Trap")
        {
            if (getHitScript.hurt == false)
            {
                anim.SetTrigger("Hit");
            }
        }
    }
}