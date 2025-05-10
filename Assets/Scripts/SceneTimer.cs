using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTimer : MonoBehaviour
{

    public string levelToLoad;
    private float timer = 10f; 
    private Text timerSeconds;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerSeconds = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        timerSeconds.text = timer.ToString("f0");
        if (timer <= 0) {
            SceneManager.LoadScene(levelToLoad);
        }
    }
}
