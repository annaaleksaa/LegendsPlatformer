using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GetHit : MonoBehaviour
{
    public bool hurt = false;

    private bool slipping = false;
    private PlayerMovement playerMovementScript;
    private Rigidbody rb;
    private Transform enemy;
    public int startingHealth = 100;
    private int currentHealth;
    public Text healthText;

    public void IncreaseHealth()
    {
        currentHealth = Mathf.Min(currentHealth + 1, 100);
        UpdateHealthUI();
    }

    private void Start()
    {
        playerMovementScript = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
        currentHealth = startingHealth;
        UpdateHealthUI();
    }

    private void FixedUpdate()
    {
        if (slipping)
        {
            transform.Translate(Vector3.back * 20 * Time.deltaTime, Space.World);
            playerMovementScript.playerStats.canMove = false;
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (!hurt)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                enemy = other.gameObject.transform;
                rb.AddForce(enemy.forward * 1000);
                rb.AddForce(transform.up * 500);
                TakeDamage(10);
            }
            if (other.gameObject.CompareTag("Trap"))
            {
                rb.AddForce(transform.forward * -1000);
                rb.AddForce(transform.up * 500);
                TakeDamage(10);
            }
        }
        if (other.gameObject.layer == 9)
        {
            slipping = true;
        }
        if (other.gameObject.layer != 9)
        {
            if (slipping)
            {
                slipping = false;
                playerMovementScript.playerStats.canMove = true;
            }
        }
    }

    private void TakeDamage(int damage)
    {
        hurt = true;
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        UpdateHealthUI();
        playerMovementScript.playerStats.canMove = false;
        playerMovementScript.soundManager.PlayHitSound();
        StartCoroutine(Recover());
    }

    private IEnumerator Recover()
    {
        yield return new WaitForSeconds(0.75f);
        hurt = false;
        playerMovementScript.playerStats.canMove = true;
    }

    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth.ToString();
        }
    }
}