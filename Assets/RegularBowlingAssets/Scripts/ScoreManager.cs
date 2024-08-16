using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField]
    private int score = 0;
    [SerializeField]
    private int frame = 1;
    [SerializeField]
    private List<int> multiplierCount = new List<int>();
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI frameText;
    public TextMeshProUGUI lastFrameText;
    public TextMeshProUGUI currentThrowNumText;
    public GameManager gameManager;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateScoreText();
        UpdateFrameText();
        UpdateLastFrameText("N/A");
        UpdateCurrentThrowNumText("1");
        multiplierCount.Add(0);
        multiplierCount.Add(0);
    }
    
    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    public void IncreaseFrame()
    {
        if (frame < 10)
        {
            frame++;
        }else if (frame == 10)
        {
            gameManager.GameOver();
        }
        UpdateFrameText();
    }

    public void ModifyMultiplierCount(string type)
    {
        if (type == "Strike")
        {
            multiplierCount[0] += 1;
            multiplierCount[1] += 1;
            UpdateLastFrameText("Strike");
        }
        else if (type == "Spare")
        {
            multiplierCount[0] += 1;
            UpdateLastFrameText("Spare");
        }else if (type == "Bowl")
        {
            multiplierCount.RemoveAt(0);
            multiplierCount.Add(0);
        }
        else if (type == "Open")
        {
            UpdateLastFrameText("Open");
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    void UpdateFrameText()
    {
        frameText.text = "Frame " + frame + "/10";
    }

    public void UpdateLastFrameText(string text)
    {
        lastFrameText.text = "Last Frame: " + text;
    }
    public void UpdateCurrentThrowNumText(string text)
    {
        currentThrowNumText.text = "Throw #" + text;
    }

    public int GetScore()
    {
        return score;
    }

    public int GetFrame()
    {
        return frame;
    }

    public int GetMultiplierCount()
    {
        return multiplierCount[0];
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }

    public void ResetFrame()
    {
        frame = 1;
        UpdateFrameText();
    }

    public void ResetMultiplierCount()
    {
        multiplierCount.Clear();
        multiplierCount.Add(0);
        multiplierCount.Add(0);
    }
    
    public void ResetGame()
    {
        ResetScore();
        ResetFrame();
        ResetMultiplierCount();
        UpdateLastFrameText("N/A");
        UpdateCurrentThrowNumText("1");
    }
}
