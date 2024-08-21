using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private const string ProfilesKey = "Profiles";

    public Button playButton;
    public Button profilesButton;
    public Button creditsButton;
    public Button mainMenuButton;
    public Button settingsButton;
    public Canvas mainMenuCanvas;
    public Canvas creditsCanvas;
    public Canvas settingsCanvas;
    public GameObject menuMusicHandler;
    public TextMeshProUGUI selectedProfileText;
    public TextMeshProUGUI pleaseSelectProfileText;
    [SerializeField]
    private int selectedProfile;


    private void Awake()
    {
        if (PlayerPrefs.GetInt($"{ProfilesKey}_-1_MusicEnabled", -1) == -1)
        {
            PlayerPrefs.SetString($"{ProfilesKey}_-1_Name", "No Profile Selected");
            PlayerPrefs.SetInt($"{ProfilesKey}_-1_RegularHighScore", -1);
            PlayerPrefs.SetInt($"{ProfilesKey}_-1_ChallengeHighScore", -1);
            PlayerPrefs.SetInt($"{ProfilesKey}_-1_MusicEnabled", 1);
            PlayerPrefs.SetFloat($"{ProfilesKey}_-1_MusicVolume", 1.000f);
            PlayerPrefs.SetInt($"{ProfilesKey}_-1_SoundEffectsEnabled", 1);
            PlayerPrefs.SetFloat($"{ProfilesKey}_-1_SoundEffectsVolume", 1.000f);
        }
        if (PlayerPrefs.GetInt(ProfilesKey + "_Selected", -1) == -1)
        {
            PlayerPrefs.SetInt(ProfilesKey + "_Selected", -1);
        }
        PlayerPrefs.Save();
    }

    private void Start()
    {
        mainMenuCanvas.gameObject.SetActive(true);
        settingsCanvas.gameObject.SetActive(true);
        settingsCanvas.gameObject.SetActive(false);
        menuMusicHandler = GameObject.FindGameObjectWithTag("Music");
        pleaseSelectProfileText.gameObject.SetActive(false);
        creditsCanvas.gameObject.SetActive(false);
        playButton.onClick.AddListener(ClickPlay);
        profilesButton.onClick.AddListener(ClickProfiles);
        creditsButton.onClick.AddListener(ShowCredits);
        mainMenuButton.onClick.AddListener(ReturnToMenu);
        settingsButton.onClick.AddListener(Settings);
        selectedProfile = PlayerPrefs.GetInt(ProfilesKey + "_Selected", -1);
        if (selectedProfile == -1)
        {
            selectedProfileText.text = "No Profile Selected";
        }
        else
        {
            selectedProfileText.text = "Profile: " + PlayerPrefs.GetString($"{ProfilesKey}_{selectedProfile}_Name");
        }
    }

    private void ClickPlay()
    {
        if (selectedProfile != -1)
        {
            StartCoroutine(ClickPlayCoroutine());
        }
        else
        {
            StartCoroutine(PleaseSelectProfile());
        }
    }

    private IEnumerator ClickPlayCoroutine()
    {
        // ensures button sound effect plays
        yield return new WaitForSeconds(0.06f);
        Destroy(menuMusicHandler);
        SceneManager.LoadScene("RegularBowling");
    }

    IEnumerator PleaseSelectProfile()
    {
        SetMenuActive(false);
        pleaseSelectProfileText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        pleaseSelectProfileText.gameObject.SetActive(false);
        SetMenuActive(true);
    }

    private void ClickProfiles()
    {
        StartCoroutine(ClickProfilesCoroutine());
    }
    
    private IEnumerator ClickProfilesCoroutine()
    {
        // ensures button sound effect plays
        yield return new WaitForSeconds(0.06f);
        SceneManager.LoadScene("ProfileMenu");
    }

    private void ShowCredits()
    {
        mainMenuCanvas.gameObject.SetActive(false);
        creditsCanvas.gameObject.SetActive(true);
    }

    private void ReturnToMenu()
    {
        mainMenuCanvas.gameObject.SetActive(true);
        creditsCanvas.gameObject.SetActive(false);
    }

    private void SetMenuActive(bool isActive)
    {
        selectedProfileText.gameObject.SetActive(isActive);
        playButton.gameObject.SetActive(isActive);
        profilesButton.gameObject.SetActive(isActive);
        creditsButton.gameObject.SetActive(isActive);
    }

    private void Settings()
    {
        mainMenuCanvas.gameObject.SetActive(false);
        settingsCanvas.gameObject.SetActive(true);
    }
}
