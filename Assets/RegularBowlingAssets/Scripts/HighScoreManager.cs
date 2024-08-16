using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class HighScoreManager : MonoBehaviour
{
    private const string ProfilesKey = "Profiles";
    private int highscore;
    public TextMeshProUGUI highScoreText;

    void Start()
    {
        InitializeHighscore();
        UpdateHighScoreText();
    }

    void InitializeHighscore()
    {
        int currentProfile = PlayerPrefs.GetInt(ProfilesKey + "_Selected", 0);
        highscore = PlayerPrefs.GetInt($"{ProfilesKey}_{currentProfile}_RegularHighScore");
    }

    public bool CheckForHighscore(int score)
    {
        int currentProfile = PlayerPrefs.GetInt(ProfilesKey + "_Selected", 0);
        if (score > PlayerPrefs.GetInt($"{ProfilesKey}_{currentProfile}_RegularHighScore"))
        {
            PlayerPrefs.SetInt($"{ProfilesKey}_{currentProfile}_RegularHighScore", score);
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
