using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlowControl : MonoBehaviour
{
    State state;
    // Start is called before the first frame update
    void Start()
    {
        state = FindObjectOfType<State>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Level01");
    }

    public void LoadHighScores()
    {
        SceneManager.LoadScene("High Scores");
    }

    public void LoanMainMenu()
    {
        SceneManager.LoadScene("Start Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
