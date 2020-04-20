using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class HighScores : MonoBehaviour
{
    [SerializeField] Text highScoreText = null;
    //int highScore = 0;

    // Start is called before the first frame update
    void Start()
    {

        string scoreString = PlayerPrefs.GetString("Highscores");
        string[] scores = scoreString.Split(',');
        StringBuilder highScoreString = new StringBuilder();

        for(int i = 0; i < scores.Length; i++)
        {
            if(int.TryParse(scores[i], out var num))
            {
                if(i < 9)
                    highScoreString.Append(string.Format("{0}. {1}\n", (i + 1).ToString(), num.ToString().PadLeft(30, ' ')));
                else
                    highScoreString.Append(string.Format("{0} {1}\n", (i + 1).ToString(), num.ToString().PadLeft(29, ' ')));

            }
        }
        //highScore = PlayerPrefs.GetInt("Highscore", 0);
        highScoreText.text = highScoreString.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
