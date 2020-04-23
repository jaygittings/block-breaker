using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class State: MonoBehaviour
{
    //config
    [SerializeField] int numBlocksInLevel = 0;
    object valueTypeLock = new object();

    [SerializeField] int pointsPerBlock = 10;
    [Range(0f, 3f)] [SerializeField] float gameSpeed = 1f;
    int playerScore = 0;
    TextMeshProUGUI score = null;

    [SerializeField] bool isAutoPlayEnabled = false;
    [SerializeField] GameObject soundtrackController = null;
    [SerializeField] GameObject levelStinger = null;

    private void Awake()
    {
        //Singleton pattern for game object
        int controllerCount = FindObjectsOfType<State>().Length;
        if (controllerCount > 1)
        {
            this.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("loaded: " + scene.name);
        GameObject[] objs = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach(GameObject o in objs)
        {
            if (o.name == "Soundtrack" && o.activeInHierarchy)
            {
                //Debug.Log("soundtrack found");
                soundtrackController = o;
            }
            if (o.name == "Score Text" && o.activeInHierarchy)
            {
                //Debug.Log("score found " + o.GetInstanceID());
                score = o.GetComponent<TextMeshProUGUI>();
                score.text = playerScore.ToString();
            }

            if (o.name == "Level End Text")
            {
                //Debug.Log("level stinger found");
                levelStinger = o;
            }
        }
    }

    internal bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }

    void Start()
    {
        //GameObject[] objects = FindObjectsOfType<GameObject>();
        //foreach(GameObject o in objects)
        //{
        //    if(o.name == "Score Text")
        //    {
        //        score = o.GetComponent<TextMeshProUGUI>();
        //        break;
        //    }
        //}
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;


        //if(score != null)
        //    score.text = playerScore.ToString();
    }

    public void AddBlock()
    {
            numBlocksInLevel++;
        //if(this.enabled)
        //{
        //    //lock(valueTypeLock)
        //    //{
        //    //    //Debug.Log(numBlocksInLevel);
        //    //}

        //}
    }

    //public void RemoveBlock()
    //{
    //    lock (valueTypeLock)
    //    {
    //        numBlocksInLevel--;
    //        //Debug.Log(numBlocksInLevel);
    //        playerScore += pointsPerBlock;
    //    }

    //    if(numBlocksInLevel <= 0)
    //    {
    //        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //    }
    //}

    public void RemoveBlock(int multiplier)
    {
        //Debug.Log("score ID = " + score.GetInstanceID());
        numBlocksInLevel--;

        playerScore += (pointsPerBlock * multiplier);
        score.text = playerScore.ToString();
        //lock (valueTypeLock)
        //{
        //    //Debug.Log(numBlocksInLevel);
        //}

        if (numBlocksInLevel <= 0)
        {
            StartCoroutine(DoLevelEnd("Success"));
        }
    }

    private IEnumerator DoLevelEnd(String result)
    {
        Ball ball = FindObjectOfType<Ball>();
        ball.gameObject.SetActive(false);

        if (result == "Success")
            soundtrackController.GetComponent<Soundtrack>().PlaySuccessStinger();
        else
            soundtrackController.GetComponent<Soundtrack>().PlayFailureStinger();

        var levelStingerText = levelStinger.GetComponent<TextMeshProUGUI>();
        levelStingerText.text = result;
        levelStinger.SetActive(true);
        yield return new WaitForSeconds(3);
        if(result == "Success")
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene("Game Over");
        }
    }

    private void OnDestroy()
    {
        UpdateHighScore();

        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void UpdateHighScore()
    {
        string scoreString = PlayerPrefs.GetString("Highscores", "0,0,0,0,0,0,0,0,0,0");

        string[] scores = scoreString.Split(',');


        for(int i = 0; i < scores.Length; i++)
        {
            if(int.TryParse(scores[i], out var num))
            {
                if(playerScore >= num)
                {
                    scores[i] = playerScore.ToString();
                    playerScore = num;
                }
            }
        }

        scoreString = string.Join(",", scores);
        PlayerPrefs.SetString("Highscores", scoreString);


        //int score = PlayerPrefs.GetInt("Highscore", 0);
        //if (playerScore > score)
        //{
        //    PlayerPrefs.SetInt("Highscore", playerScore);
        //    PlayerPrefs.Save();
        //}

        PlayerPrefs.Save();

    }

    public void ResetGame()
    {
        StartCoroutine(DoLevelEnd("Failure"));
    }

}
