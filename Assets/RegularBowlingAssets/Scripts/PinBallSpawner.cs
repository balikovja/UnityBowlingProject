using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinBallSpawner : MonoBehaviour
{
    public Vector3[] pinLocations;
    public Vector3 pinRotation;
    public GameObject bowlingPin;
    public List<GameObject> currentPins;
    public GameObject bowlingBall;
    public GameObject guideLine;
    public Vector3 ballPosition;
    public Vector3 guideLinePosition;
    public GameManager gameManager;
    public int throwNum = 1;
    private int[] multFrameTen = new int[3];
    private int frameTenThrowNum = 0;
    private bool frameTenThirdThrow = false;

    // Start is called before the first frame update
    void Start()
    {
        ResetLane();
    }

    public void SpawnBall()
    {
        if (gameManager.GetBowlingBall() != null && gameManager.GetBowlingBall().GetComponent<BowlingBall>().GetGuideLine() != null)
        {
            Destroy(gameManager.GetBowlingBall().GetComponent<BowlingBall>().GetGuideLine());
            Destroy(gameManager.GetBowlingBall());
        }
        gameManager.SetBowlingBall(Instantiate(bowlingBall, ballPosition, Quaternion.identity));
        gameManager.GetBowlingBall().GetComponent<BowlingBall>().SetGuideLine(Instantiate(guideLine, guideLinePosition, Quaternion.identity));
    }

    public void SpawnPins()
    {
        foreach(GameObject pin in currentPins)
        {
            Destroy(pin);
        }
        currentPins.Clear();
        foreach(Vector3 pinLocation in pinLocations)
        {
            currentPins.Add(Instantiate(bowlingPin, pinLocation, Quaternion.Euler(pinRotation)));
        }
    }

    
    public void ResetLane()
    {
        SpawnBall();
        SpawnPins();
        throwNum = 1;
    }


    public void ManualResetLane()
    {
        SpawnBall();
    }


    public int CheckPinsAndUpdateScore()
    {
        int scoreThisBowl = 0;
        int i = 0;
        if (ScoreManager.Instance.GetFrame() != 10)
        {
            while (i < currentPins.Count)
            {
                if (currentPins[i].transform.position.y < 0 || currentPins[i].transform.rotation.eulerAngles.x < 260 || currentPins[i].transform.rotation.eulerAngles.x > 280)
                {
                    ScoreManager.Instance.IncreaseScore(1 + ScoreManager.Instance.GetMultiplierCount());
                    scoreThisBowl++;
                    Destroy(currentPins[i]);
                    currentPins.RemoveAt(i);
                    i--;
                }
                i++;
            }
            ScoreManager.Instance.ModifyMultiplierCount("Bowl");
            if (scoreThisBowl < 10 && throwNum == 1)
            {
                SpawnBall();
                throwNum++;
            }
            else
            {
                if (currentPins.Count == 0)
                {
                    if (throwNum == 1)
                    {
                        ScoreManager.Instance.ModifyMultiplierCount("Strike");
                    }
                    else
                    {
                        ScoreManager.Instance.ModifyMultiplierCount("Spare");
                    }
                }
                else
                {
                    ScoreManager.Instance.ModifyMultiplierCount("Open");
                }
                ResetLane();
                ScoreManager.Instance.IncreaseFrame();
            }
            ScoreManager.Instance.UpdateCurrentThrowNumText(throwNum.ToString());
        }
        else
        {
            if(frameTenThrowNum == 0)
            {
                multFrameTen[0] = ScoreManager.Instance.GetMultiplierCount();
                ScoreManager.Instance.ModifyMultiplierCount("Bowl");
                multFrameTen[1] = ScoreManager.Instance.GetMultiplierCount();
                multFrameTen[2] = 0;
            }
            if (frameTenThrowNum < 2 || (frameTenThrowNum == 2 && frameTenThirdThrow))
            {
                while (i < currentPins.Count)
                {
                    if (currentPins[i].transform.position.y < 0 || currentPins[i].transform.rotation.eulerAngles.x < 260 || currentPins[i].transform.rotation.eulerAngles.x > 280)
                    {
                        ScoreManager.Instance.IncreaseScore(1 + multFrameTen[frameTenThrowNum]);
                        scoreThisBowl++;
                        Destroy(currentPins[i]);
                        currentPins.RemoveAt(i);
                        i--;
                    }
                    i++;
                }
                if (currentPins.Count == 0 || frameTenThrowNum == 2)
                {
                    if (!frameTenThirdThrow)
                    {
                        frameTenThirdThrow = true;
                    }
                    ResetLane();
                }
                else
                {
                    SpawnBall();
                }
                frameTenThrowNum++;
            }
            ScoreManager.Instance.UpdateCurrentThrowNumText((frameTenThrowNum + 1).ToString());
            if ((currentPins.Count != 0 && frameTenThrowNum == 2 && !frameTenThirdThrow) || frameTenThrowNum == 3)
            {
                ResetLane();
                ScoreManager.Instance.UpdateCurrentThrowNumText("1");
                ScoreManager.Instance.IncreaseFrame();
            }
        }
        return scoreThisBowl;
    }


    public void ResetGame()
    {
        ResetLane();
        ScoreManager.Instance.ResetGame();
        throwNum = 1;
        frameTenThrowNum = 0;
        frameTenThirdThrow = false;
    }
}
