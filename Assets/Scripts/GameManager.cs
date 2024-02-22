using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Tooltip("Which key quits the game and closes the application when playing the build.")]
    public KeyCode quitGame;

    [Tooltip("The camera that pans around the goal at the start of the game.")]
    public GameObject introCamera;

    [Tooltip("The main camera that follows the player around.")]
    public GameObject playerCamera;

    [Tooltip("The image that appears on screen after the player reaches the goal.")]
    public GameObject winUI;

    [Tooltip("The score text.")]
    public GameObject scoreUI;

    [Tooltip("The text that indicates you can skip the intro.")]
    public GameObject skipIntroUI;

    [Tooltip("The text that reminds the player which key quits the game.")]
    public GameObject quitGameUI;

    [Tooltip("The text that shows the player's score.")]
    public Text scoreText;

    [Tooltip("Which game scene will load up after the player has reached the goal.")]
    public string gameScene;

    [Tooltip("The player's movement script.")]
    public PlayerMovement playerMovementScript;

    private bool introducing;

    void Start()
    {
        StartCoroutine("Cinematic");
        introducing = true;
    }
    void Update()
    {
        // displays the player's current score onto text
        scoreText.text = ("SCORE: " + ScoreManager.score);

        // allows the player to skip the panning shot at the start of the game
        if (introducing == true)
        {
            if (Input.anyKey)
            {
                introducing = false;
                StopCoroutine("Cinematic");
                playerCamera.SetActive(true);
                introCamera.SetActive(false);
                skipIntroUI.SetActive(false);
                scoreUI.SetActive(true);
                quitGameUI.SetActive(true);
                playerMovementScript.playerStats.canMove = true;
            }
        }
        // if playing on the build, pressing this key will close the application
        if (Input.GetKey(quitGame))
        {
            Application.Quit();
        }
    }
    public void Win()
    {
        winUI.SetActive(true);
        StartCoroutine("Reset");
    }
    IEnumerator Cinematic()
    {
        playerCamera.SetActive(false);
        introCamera.SetActive(true);
        yield return new WaitForSeconds(7);
        playerCamera.SetActive(true);
        introCamera.SetActive(false);
        introducing = false;
        skipIntroUI.SetActive(false);
        scoreUI.SetActive(true);
        quitGameUI.SetActive(true);
        playerMovementScript.playerStats.canMove = true;
    }
    IEnumerator Reset()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(gameScene);
    }
}