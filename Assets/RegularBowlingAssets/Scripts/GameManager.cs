using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Canvas regularBowlingCanvas;
    public Canvas settingsCanvas;
    public Button bowlButton;
    public Button resetButton;
    public Button cameraButton;
    public Button moveLeftButton;
    public Button moveRightButton;
    public Button rotateLeftButton;
    public Button rotateRightButton;
    public Button settingsButton;
    [SerializeField]
    private GameObject bowlingBall;
    [SerializeField]
    private GameObject guideLine;
    public PinBallSpawner pinBallSpawner;
    /*
    private bool hasBowled = false;
    private Vector3 lastBowlPosition;
    private Quaternion lastBowlRotation;
    */
    [SerializeField]
    private CameraManager cameraManager;
    public TextMeshProUGUI gameOverText;
    public Button restartGameButton;
    public Button mainMenuButton;
    private bool gameOver = false;
    public GameObject gameOverTextBackground;
    public HighScoreManager highScoreManager;
    public PowerBarManager powerBarManager;
    [SerializeField]
    private int bowlButtonClicked = 0;
    public AudioSource bowlingBallSoundManager;
    private GameObject menuMusicHandler;
    
    
    // Start is called before the first frame update
    void Start()
    {
        menuMusicHandler = GameObject.FindGameObjectWithTag("Music");
        settingsCanvas.gameObject.SetActive(true);
        settingsCanvas.gameObject.SetActive(false);
        bowlButton.onClick.AddListener(HandleBowlButtonClick);
        resetButton.onClick.AddListener(ResetLane);
        settingsButton.onClick.AddListener(Settings);
        restartGameButton.onClick.AddListener(ResetGame);
        restartGameButton.gameObject.SetActive(false);
        mainMenuButton.onClick.AddListener(MainMenu);
        mainMenuButton.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        gameOverTextBackground.SetActive(false);
        cameraManager = FindObjectOfType<CameraManager>();
        StartCoroutine(powerBarManager.UpdatePowerBar());
    }

    IEnumerator BowlSequence()
    {
        if (bowlingBall != null)
        {
            /*
            hasBowled = true;
            lastBowlPosition = bowlingBall.GetComponent<BowlingBall>().transform.position;
            lastBowlRotation = bowlingBall.GetComponent<BowlingBall>().transform.rotation;
            */
            SetButtonsActive(false);
            bowlingBallSoundManager.Play();
            yield return StartCoroutine(bowlingBall.GetComponent<BowlingBall>().BowlCoroutine((powerBarManager.GetPowerRatio() * 30f) + 35f));
            if (!gameOver)
            {
                SetButtonsActive(true);
            }
            powerBarManager.SetCurrentPower(0);
        }
    }

    void HandleBowlButtonClick()
    {
        if(bowlButtonClicked == 0)
        {
            powerBarManager.SetPowerBarOn(true);
            StartCoroutine(powerBarManager.UpdatePowerBar());
            bowlButtonClicked++;
        }
        else
        {
            powerBarManager.SetPowerBarOn(false);
            bowlButtonClicked = 0;
            StartCoroutine(BowlSequence());
        }
    }

    public void MoveLeft()
    {
        if (bowlingBall != null)
        {
            bowlingBall.GetComponent<BowlingBall>().MoveLeft();
        }
    }

    public void MoveRight()
    {
        if (bowlingBall != null)
        {
            bowlingBall.GetComponent<BowlingBall>().MoveRight();
        }
    }

    public void RotateLeft()
    {
        if (bowlingBall != null)
        {
            bowlingBall.GetComponent<BowlingBall>().RotateLeft();
        }
    }

    public void RotateRight()
    {
        if (bowlingBall != null)
        {
            bowlingBall.GetComponent<BowlingBall>().RotateRight();
        }
    }

    void ResetLane()
    {
        pinBallSpawner.ManualResetLane();
    }

    public void SetBowlingBall(GameObject bowlingBall)
    {
        this.bowlingBall = bowlingBall;
    }

    public GameObject GetBowlingBall()
    {
        return bowlingBall;
    }

    /*
    private IEnumerator InstantReplay()
    {
        if (hasBowled)
        {
            SetButtonsActive(false);
            for (int i = 0; i < cameraManager.cameras.Length; i++)
            {
                if (i == 0)
                {
                    cameraManager.SetActiveCamera(0);
                }
                else
                {
                    cameraManager.SwitchCamera();
                }
                pinBallSpawner.ResetLane();
                bowlingBall.GetComponent<BowlingBall>().Bowl(lastBowlPosition, lastBowlRotation);

                // Wait before switching cameras
                yield return new WaitForSeconds(6f);
            }
            pinBallSpawner.ResetLane();
            // Ensure the final camera switch
            cameraManager.SwitchCamera();
            SetButtonsActive(true);
        }
    }
    */

    private void SetButtonsActive(bool isActive)
    {
        bowlButton.gameObject.SetActive(isActive);
        resetButton.gameObject.SetActive(isActive);
        cameraButton.gameObject.SetActive(isActive);
        moveLeftButton.gameObject.SetActive(isActive);
        moveRightButton.gameObject.SetActive(isActive);
        rotateLeftButton.gameObject.SetActive(isActive);
        rotateRightButton.gameObject.SetActive(isActive);
        settingsButton.gameObject.SetActive(isActive);
    }


    public void GameOver()
    {
        gameOver = true;
        gameOverText.text = "Game Over\nFinal Score: " + ScoreManager.Instance.GetScore();
        if (highScoreManager.CheckForHighscore(ScoreManager.Instance.GetScore()))
        {
            gameOverText.text = "Game Over\nNew High Score Of " + highScoreManager.GetHighscore() + "!";
        }
        gameOverText.gameObject.SetActive(true);
        gameOverTextBackground.SetActive(true);
        restartGameButton.gameObject.SetActive(true);
        mainMenuButton.gameObject.SetActive(true);
        SetButtonsActive(false);
    }

    void ResetGame()
    {
        gameOverText.gameObject.SetActive(false);
        gameOverTextBackground.SetActive(false);
        restartGameButton.gameObject.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);
        SetButtonsActive(true);
        pinBallSpawner.ResetGame();
        gameOver = false;
    }

    void Settings()
    {
        regularBowlingCanvas.gameObject.SetActive(false);
        settingsCanvas.gameObject.SetActive(true);
    }

    private void MainMenu()
    {
        StartCoroutine(ReturnToMainMenuCoroutine());
    }

    private IEnumerator ReturnToMainMenuCoroutine()
    {
        // ensures button sound effect plays
        yield return new WaitForSeconds(0.06f);
        Destroy(menuMusicHandler);
        SceneManager.LoadScene("MainMenu");
    }
}
