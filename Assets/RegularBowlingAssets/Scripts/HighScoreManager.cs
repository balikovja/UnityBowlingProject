using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class HighScoreManager : MonoBehaviour
{
    private int highscore;
    public TextMeshProUGUI highScoreText;

    void Start()
    {
        InitializeHighscore();
        UpdateHighScoreText();
    }

    void InitializeHighscore()
    {
        if (!PlayerPrefs.HasKey("Highscore"))
        {
            PlayerPrefs.SetInt("Highscore", 0);
            PlayerPrefs.Save();
        }

        highscore = PlayerPrefs.GetInt("Highscore");
    }

    public bool CheckForHighscore(int score)
    {
        if (score > PlayerPrefs.GetInt("Highscore"))
        {
            PlayerPrefs.SetInt("Highscore", score);
            PlayerPrefs.Save();
            highscore = score;
            UpdateHighScoreText();
            return true;
        }
        return false;
    }

    public int GetHighscore()
    {
        return highscore;
    }

    void UpdateHighScoreText()
    {
        highScoreText.text = "High Score: " + highscore;
    }
}
