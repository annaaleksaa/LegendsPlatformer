using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoins : MonoBehaviour
{
    [Tooltip("The particles that appear after the player collects a coin.")]
    public GameObject coinParticles;

    // Counter to track the number of coins collected
    private static int coinsCollected = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerMovement playerMovementScript = other.GetComponent<PlayerMovement>();

            if (playerMovementScript != null)
            {
                // Increase the counter of coins collected
                coinsCollected++;

                // Increase score
                ScoreManager.score += 10;

                // Check if the number of coins collected is a multiple of 10
                if (coinsCollected % 10 == 0)
                {
                    // Increment player's health by 1, but ensure it doesn't exceed 100
                    GetHit playerHealthScript = other.GetComponent<GetHit>();
                    if (playerHealthScript != null)
                    {
                        playerHealthScript.IncreaseHealth();
                    }
                }

                // Play coin collection sound
                if (playerMovementScript.soundManager != null)
                {
                    playerMovementScript.soundManager.PlayCoinSound();
                }

                // Instantiate coin particles
                GameObject particles = Instantiate(coinParticles, transform.position, Quaternion.identity);

                // Destroy the collected coin object
                Destroy(gameObject);
            }
        }
    }
}