using TMPro; //need for ui
using UnityEngine;
using UnityEngine.SceneManagement; //need to switch scenes
using System.Collections;

public class PasswordMiniGame : MonoBehaviour
{
    public TMP_Text displayText; //shows the blank to input word "- - - - -"
    public TMP_InputField input; //input field for guessing
    public TMP_Text timerText; // countdown
    public float timeLeft = 60f; //time to play game
    public GameObject gameOverScreen;

    public GameObject congratsScreen; 
    public float waitBeforeNextScene = 3f; //waitime after winning before loading next scene
    public string nextSceneName = "Room2";

    private string answer = "paint";
    private char[] currentGuess;
    private bool gameOver = false;

    void Start() 
    {
        //initialized current guess with the underscores and starts countdown
        currentGuess = new string('_', answer.Length).ToCharArray();
        displayText.text = new string(currentGuess);
        StartCoroutine(Countdown());
    }

    public void SubmitGuess()
    {
        if (gameOver) return; //doesn't allow guesses if game is over

        string guess = input.text.ToLower();
        input.text = ""; //clear input field
        ProcessGuess(guess); //checks guess
    }

    public void CheckGuess(string guess)
    {
        if (gameOver) return;

        ProcessGuess(guess.ToLower());
    }

    private void ProcessGuess(string guess)
    {
        //bool correct = false;
        for (int i = 0; i < answer.Length; i++) //loop through answer and reveal correct letters
        {
            if (answer[i].ToString() == guess)
            {
                currentGuess[i] = answer[i];
                //correct = true;
            }
        }

        displayText.text = new string(currentGuess); //updates display with guessed letters

        if (new string(currentGuess) == answer) //if all letters have been guessed, trigger win
        {
            GameWon();
        }
    }

    IEnumerator Countdown()
    {
        //countdown timer that updates every frame
        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerText.text = Mathf.Ceil(timeLeft).ToString();
            yield return null;
        }

        if (new string(currentGuess) != answer)
        {
            GameOver();
        }
    }

    void GameWon()
    {
        gameOver = true;
        Debug.Log("You unlocked the password!");

        if (congratsScreen != null) //if won, show win screen and load next scene
        {
            congratsScreen.SetActive(true);
        }

        StartCoroutine(LoadNextSceneAfterDelay());
    }

    IEnumerator LoadNextSceneAfterDelay()
    {
        yield return new WaitForSeconds(waitBeforeNextScene);
        SceneManager.LoadScene(nextSceneName); //next scene
    }

    void GameOver()
    {
        gameOver = true;
        gameOverScreen.SetActive(true); //show game over screen
        Debug.Log("Game Over!");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //try again
    }
}
