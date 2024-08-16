using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenuManager : MonoBehaviour
{
    private const string ProfilesKey = "Profiles";

    public Button exitButton;
    public Button mainMenuButton;
    public Button backToGameButton;
    public Button saveSettingsButton;
    public Toggle musicVolumeToggle;
    public Slider musicVolumeSlider;
    public Toggle soundEffectsVolumeToggle;
    public Slider soundEffectsVolumeSlider;
    public Canvas previousCanvas;
    public Canvas settingsCanvas;
    private GameObject menuMusicHandler;
    [SerializeField]
    private int currentProfile;
    [SerializeField]
    private bool musicEnabled;
    [SerializeField]
    private float musicVolume;
    [SerializeField]
    private bool soundEffectsEnabled;
    [SerializeField]
    private float soundEffectsVolume;

    // Start is called before the first frame update
    void Start()
    {
        menuMusicHandler = GameObject.FindGameObjectWithTag("Music");
        exitButton.onClick.AddListener(ExitGame);
        mainMenuButton.onClick.AddListener(ReturnToMainMenu);
        backToGameButton.onClick.AddListener(BackToGame);
        saveSettingsButton.onClick.AddListener(SaveSettings);
        InitializeSettings();
    }

    private void ExitGame()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }

    private void ReturnToMainMenu()
    {
        Destroy(menuMusicHandler);
        SceneManager.LoadScene("MainMenu");
    }

    private void BackToGame()
    {
        if (musicVolumeSlider.value != musicVolume)
        {
            musicVolumeSlider.value = musicVolume;
        }
        if (musicVolumeToggle.isOn != musicEnabled)
        {
            musicVolumeToggle.isOn = musicEnabled;
        }
        if (soundEffectsVolumeSlider.value != soundEffectsVolume)
        {
            soundEffectsVolumeSlider.value = soundEffectsVolume;
        }
        if (soundEffectsVolumeToggle.isOn != soundEffectsEnabled)
        {
            soundEffectsVolumeToggle.isOn = soundEffectsEnabled;
        }
        previousCanvas.gameObject.SetActive(true);
        settingsCanvas.gameObject.SetActive(false);
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetFloat($"{ProfilesKey}_{currentProfile}_MusicVolume", musicVolumeSlider.value);
        musicVolume = musicVolumeSlider.value;
        PlayerPrefs.SetInt($"{ProfilesKey}_{currentProfile}_MusicEnabled", musicVolumeToggle.isOn ? 1 : 0);
        musicEnabled = musicVolumeToggle.isOn;
        PlayerPrefs.SetFloat($"{ProfilesKey}_{currentProfile}_SoundEffectsVolume", soundEffectsVolumeSlider.value);
        soundEffectsVolume = soundEffectsVolumeSlider.value;
        PlayerPrefs.SetInt($"{ProfilesKey}_{currentProfile}_SoundEffectsEnabled", soundEffectsVolumeToggle.isOn ? 1 : 0);
        soundEffectsEnabled = soundEffectsVolumeToggle.isOn;
    }

    private void InitializeSettings()
    {
        currentProfile = PlayerPrefs.GetInt(ProfilesKey + "_Selected");
        musicEnabled = PlayerPrefs.GetInt($"{ProfilesKey}_{currentProfile}_MusicEnabled") == 1 ? true : false;
        musicVolume = PlayerPrefs.GetFloat($"{ProfilesKey}_{currentProfile}_MusicVolume");
        soundEffectsEnabled = PlayerPrefs.GetInt($"{ProfilesKey}_{currentProfile}_SoundEffectsEnabled") == 1 ? true : false;
        soundEffectsVolume = PlayerPrefs.GetFloat($"{ProfilesKey}_{currentProfile}_SoundEffectsVolume");
        musicVolumeToggle.isOn = musicEnabled;
        musicVolumeSlider.value = musicVolume;
        soundEffectsVolumeToggle.isOn = soundEffectsEnabled;
        soundEffectsVolumeSlider.value = soundEffectsVolume;
    }
}
