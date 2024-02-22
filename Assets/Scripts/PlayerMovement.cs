using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [System.Serializable]
    public struct Stats
    {
        [Tooltip("How fast the player runs.")]
        public float speed;

        [Tooltip("How high the player jumps.")]
        public float jumpForce;

        [Tooltip("Whether the player is allowed to move or not.")]
        public bool canMove;

        [Tooltip("When the player is allowed to jump or not.")]
        public bool canJump;
    }

    public Stats playerStats;

    [Tooltip("The script that will play the player's sound effects.")]
    public SoundManager soundManager;

    [Tooltip("Which layer allows the player to jump.")]
    public LayerMask groundLayer;

    [Tooltip("The transform that detects what layer the player is on.")]
    public Transform groundCheckL, groundCheckR;

    [Tooltip("The transform that the player's directional movement will be based upon.")]
    public Transform mainCamera;

    private float moveX, moveY;
    private float facing;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (playerStats.canMove)
        {
            moveX = Input.GetAxis("Horizontal");
            moveY = Input.GetAxis("Vertical");

            bool hitL = Physics.Linecast(new Vector3(groundCheckL.position.x, transform.position.y + 1, transform.position.z), groundCheckL.position, groundLayer);
            bool hitR = Physics.Linecast(new Vector3(groundCheckR.position.x, transform.position.y + 1, transform.position.z), groundCheckR.position, groundLayer);

            if (hitL || hitR)
            {
                playerStats.canJump = true;
            }
            else
            {
                playerStats.canJump = false;
            }

            if (playerStats.canJump && Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }
    }

    private void FixedUpdate()
    {
        if (playerStats.canMove)
        {
            Vector3 movement = ((mainCamera.right * moveX) * playerStats.speed) + ((mainCamera.forward * moveY) * playerStats.speed);
            rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

            if (movement.x != 0 && movement.z != 0)
            {
                facing = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
            }
            rb.rotation = Quaternion.Euler(0, facing, 0);
        }
    }

    private void Jump()
    {
        playerStats.canJump = false;
        soundManager.PlayJumpSound();
        rb.AddForce(Vector3.up * playerStats.jumpForce);
    }
}