using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float gameStartTime;
    public float timeSinceGameStart;
    public ScoreManager scoreManager;
    public PlayerController playerController;
    public CameraController camController;
    public bool isALive;
    public GameObject explosion;

    
    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceGameStart = Time.time - gameStartTime;
    }

    public void StartGame()
    {
        isALive = true;
        playerController.gameObject.SetActive(true);
        explosion.gameObject.SetActive(false);

        // Store the time when the game starts
        gameStartTime = Time.time;

    }

    public void ResetGame()
    {
        // Set new gameStartTime
        gameStartTime = Time.time;

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    
    }

    public void PlayerDeath()
    {
        isALive = false;

        // Save Highscore
        scoreManager.SaveHighScore();
        

        // Let player explode
        playerController.gameObject.SetActive(false);
        explosion.gameObject.SetActive(true);
        explosion.gameObject.transform.position = playerController.gameObject.transform.position;
        

        // Wait
        StartCoroutine(WaitAfterExplosion(1));

        
    }

    // Wait for a given number of seconds and then do something
    private IEnumerator WaitAfterExplosion(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        // Restart game
        ResetGame();

        StartGame();
    }
}
