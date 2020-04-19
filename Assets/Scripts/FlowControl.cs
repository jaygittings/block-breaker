using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlowControl : MonoBehaviour
{
    State state;
    [SerializeField] public Animator transition;
    [SerializeField] public float transitionTime;

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
        //SceneManager.LoadScene("Level01");
        StartCoroutine(LoadLevel("Level01"));
    }

    public void LoadHighScores()
    {
        StartCoroutine(LoadLevel("High Scores"));
    }

    public void LoanMainMenu()
    {
        Debug.Log("In LoadMainMenu");
        StartCoroutine(LoadLevel("Start Scene"));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        //play animation
        transition.SetTrigger("Start");
        
        //wait
        yield return new WaitForSeconds(transitionTime);

        //load scene
        SceneManager.LoadScene(levelIndex);
    }

    IEnumerator LoadLevel(String name)
    {
        Debug.Log("In LoadLevel");
        //play animation
        transition.SetTrigger("Start");

        //wait
        yield return new WaitForSeconds(transitionTime);

        //load scene
        SceneManager.LoadScene(name);
    }

}
