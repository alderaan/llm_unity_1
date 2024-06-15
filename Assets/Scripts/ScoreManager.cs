using UnityEngine;
using TMPro; // Include the TextMeshPro namespace

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI myText; // Reference to your TextMeshPro object
    public TextMeshProUGUI highScoreText; // Reference to TextMeshPro object to display the highscore
    public GameObject player; // Reference to your player GameObject
    int playerYPosition = 0;
    int playerScore = 0;
    int highScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Load the high score from PlayerPrefs
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        // Update the high score text
        highScoreText.text = "High: " + highScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the player's y position, round it to the nearest integer
        playerYPosition = Mathf.RoundToInt(player.transform.position.y);

        if (playerYPosition * 100 > playerScore)
        {
            playerScore = playerYPosition * 100;
        }

        // Update the text of your TextMeshPro object
        myText.text = "Score: " + playerScore.ToString();
    }

    // Call this function when the game ends
    public void SaveHighScore()
    {
        // If the player's score is higher than the current high score
        if (playerScore > highScore)
        {
            // Save the player's score as the new high score
            PlayerPrefs.SetInt("HighScore", playerScore);
            PlayerPrefs.Save(); // Don't forget to save the changes

            // Update the high score text
            highScoreText.text = "High Score: " + playerScore.ToString();
        }
    }
}
