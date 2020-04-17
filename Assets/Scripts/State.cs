using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class State: MonoBehaviour
{
    //config
    int numBlocksInLevel = 0;
    object valueTypeLock = new object();

    [SerializeField] int pointsPerBlock = 10;
    [Range(0f, 3f)] [SerializeField] float gameSpeed = 1f;
    int playerScore = 0;
    TextMeshProUGUI score = null;

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

    void Start()
    {
        GameObject[] objects = FindObjectsOfType<GameObject>();
        foreach(GameObject o in objects)
        {
            if(o.name == "Score Text")
            {
                score = o.GetComponent<TextMeshProUGUI>();
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
        if(score != null)
            score.text = playerScore.ToString();
    }

    public void AddBlock()
    {
        if(this.enabled)
        {
            lock(valueTypeLock)
            {
                numBlocksInLevel++;
                //Debug.Log(numBlocksInLevel);
            }

        }
    }

    public void RemoveBlock()
    {
        lock (valueTypeLock)
        {
            numBlocksInLevel--;
            //Debug.Log(numBlocksInLevel);
            playerScore += pointsPerBlock;
        }

        if(numBlocksInLevel < 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void OnDestroy()
    {
        int score = PlayerPrefs.GetInt("Highscore", 0);
        if (playerScore > score)
        {
            PlayerPrefs.SetInt("Highscore", playerScore);
            PlayerPrefs.Save();
        }

        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void ResetGame()
    {
        Destroy(this.gameObject);
    }

}
