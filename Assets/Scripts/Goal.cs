using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [Tooltip("The game manager script.")]
    public GameManager gameManager;

    [Tooltip("The particles that will appear once the player reaches the goal.")]
    public GameObject goalParticles;

    private bool triggered = false;
    SoundManager soundManager;

    void Start()
    {
        soundManager = gameManager.GetComponent<SoundManager>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (triggered == false)
        {
            if (other.gameObject.tag == "Player")
            {
                triggered = true;
                GameObject particles = Instantiate(goalParticles, transform.position, new Quaternion());
                soundManager.PlayGoalSound();
                gameManager.Win();
            }
        }
    }
}