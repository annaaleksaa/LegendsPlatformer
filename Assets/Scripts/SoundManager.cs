using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip jumpSound, stompSound, enemyDeath, enemyPoof, hitSound, coinSound, goalSound;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayJumpSound()
    {
        audioSource.PlayOneShot(jumpSound);
    }

    public void PlayStompSound()
    {
        audioSource.PlayOneShot(stompSound);
    }

    public void PlayEnemyDeathSound()
    {
        audioSource.PlayOneShot(enemyDeath);
    }

    public void PlayEnemyPoofSound()
    {
        audioSource.PlayOneShot(enemyPoof);
    }

    public void PlayHitSound()
    {
        audioSource.PlayOneShot(hitSound);
    }

    public void PlayCoinSound()
    {
        audioSource.PlayOneShot(coinSound);
    }

    public void PlayGoalSound()
    {
        audioSource.PlayOneShot(goalSound);
    }
}