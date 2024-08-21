using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProfileMenuSettingsMenuManager : MonoBehaviour
{
    private const string ProfilesKey = "Profiles";

    public Button exitButton;
    public Button mainMenuButton;
    public Button exitSettingsButton;
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
    public TextMeshProUGUI confirmationText;
    public Button confirmExitSettingsButton;
    public Button confirmReturnToMainMenuButton;
    public Button confirmExitGameButton;
    public Button cancelButton;
    public TextMeshProUGUI saveSettingsFadingText;

    // Start is called before the first frame update
    void Start()
    {
        menuMusicHandler = GameObject.FindGameObjectWithTag("Music");
        exitButton.onClick.AddListener(ConfirmExitGame);
        mainMenuButton.onClick.AddListener(ConfirmReturnToMainMenu);
        exitSettingsButton.onClick.AddListener(ConfirmExitSettings);
        saveSettingsButton.onClick.AddListener(SaveSettings);
        confirmExitSettingsButton.onClick.AddListener(ExitSettings);
        confirmExitGameButton.onClick.AddListener(ExitGame);
        confirmReturnToMainMenuButton.onClick.AddListener(ReturnToMainMenu);
        cancelButton.onClick.AddListener(Cancel);
        confirmationText.gameObject.SetActive(false);
        confirmExitSettingsButton.gameObject.SetActive(false);
        confirmExitGameButton.gameObject.SetActive(false);
        confirmReturnToMainMenuButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
        saveSettingsFadingText.gameObject.SetActive(false);
        InitializeSettings();
    }

    private void ConfirmExitGame()
    {
        if (musicVolumeSlider.value != musicVolume || musicVolumeToggle.isOn != musicEnabled ||
            soundEffectsVolumeSlider.value != soundEffectsVolume || soundEffectsVolumeToggle.isOn != soundEffectsEnabled)
        {
            SetSettingsObjectsActive(false);
            confirmationText.gameObject.SetActive(true);
            confirmExitGameButton.gameObject.SetActive(true);
            cancelButton.gameObject.SetActive(true);
        }
        else
        {
            ExitGame();
        }
    }

    private void ExitGame()
    {
        musicVolumeSlider.value = musicVolume;
        musicVolumeToggle.isOn = musicEnabled;
        soundEffectsVolumeSlider.value = soundEffectsVolume;
        soundEffectsVolumeToggle.isOn = soundEffectsEnabled;
        SetSettingsObjectsActive(true);
        confirmationText.gameObject.SetActive(false);
        confirmExitSettingsButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
        PlayerPrefs.Save();
        Application.Quit();
    }

    private void ConfirmReturnToMainMenu()
    {
        if (musicVolumeSlider.value != musicVolume || musicVolumeToggle.isOn != musicEnabled ||
            soundEffectsVolumeSlider.value != soundEffectsVolume || soundEffectsVolumeToggle.isOn != soundEffectsEnabled)
        {
            SetSettingsObjectsActive(false);
            confirmationText.gameObject.SetActive(true);
            confirmReturnToMainMenuButton.gameObject.SetActive(true);
            cancelButton.gameObject.SetActive(true);
        }
        else
        {
            ReturnToMainMenu();
        }
    }

    private void ReturnToMainMenu()
    {
        StartCoroutine(ReturnToMainMenuCoroutine());
    }

    private IEnumerator ReturnToMainMenuCoroutine()
    {
        // ensures button sound effect plays
        yield return new WaitForSeconds(0.06f);
        confirmationText.gameObject.SetActive(false);
        confirmReturnToMainMenuButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }

    private void ConfirmExitSettings()
    {
        if (musicVolumeSlider.value != musicVolume || musicVolumeToggle.isOn != musicEnabled ||
            soundEffectsVolumeSlider.value != soundEffectsVolume || soundEffectsVolumeToggle.isOn != soundEffectsEnabled)
        {
            SetSettingsObjectsActive(false);
            confirmationText.gameObject.SetActive(true);
            confirmExitSettingsButton.gameObject.SetActive(true);
            cancelButton.gameObject.SetActive(true);
        }
        else
        {
            ExitSettings();
        }
    }

    private void ExitSettings()
    {
        musicVolumeSlider.value = musicVolume;
        musicVolumeToggle.isOn = musicEnabled;
        soundEffectsVolumeSlider.value = soundEffectsVolume;
        soundEffectsVolumeToggle.isOn = soundEffectsEnabled;
        SetSettingsObjectsActive(true);
        confirmationText.gameObject.SetActive(false);
        confirmExitSettingsButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
        previousCanvas.gameObject.SetActive(true);
        settingsCanvas.gameObject.SetActive(false);
    }

    private void Cancel()
    {
        SetSettingsObjectsActive(true);
        confirmationText.gameObject.SetActive(false);
        confirmExitSettingsButton.gameObject.SetActive(false);
        confirmReturnToMainMenuButton.gameObject.SetActive(false);
        confirmExitGameButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
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
        PlayerPrefs.Save();
        StartCoroutine(FadeTextInAndOut());
    }

    private void InitializeSettings()
    {
        currentProfile = PlayerPrefs.GetInt(ProfilesKey + "_Selected");
        musicEnabled = PlayerPrefs.GetInt($"{ProfilesKey}_{currentProfile}_MusicEnabled") == 1 ? true : false;
        musicVolume = PlayerPrefs.GetFloat($"{ProfilesKey}_{currentProfile}_MusicVolume");
        soundEffectsEnabled = PlayerPrefs.GetInt($"{ProfilesKey}_{currentProfile}_SoundEffectsEnabled") == 1 ? true : false;
        soundEffectsVolume = PlayerPrefs.GetFloat($"{ProfilesKey}_{currentProfile}_SoundEffectsVolume");
        musicVolumeToggle.SetIsOnWithoutNotify(musicEnabled);
        if (musicVolumeSlider.value == musicVolume)
        {
            musicVolumeSlider.GetComponent<AudioSliderMusic>().OnChangeSlider(musicVolume);
        }
        musicVolumeSlider.value = musicVolume;
        soundEffectsVolumeToggle.SetIsOnWithoutNotify(soundEffectsEnabled);
        if (soundEffectsVolumeSlider.value == soundEffectsVolume)
        {
            soundEffectsVolumeSlider.GetComponent<AudioSliderSoundEffects>().OnChangeSlider(soundEffectsVolume);
        }
        soundEffectsVolumeSlider.value = soundEffectsVolume;
    }

    IEnumerator FadeTextInAndOut()
    {
        saveSettingsFadingText.gameObject.SetActive(true);
        // Show the text by setting alpha to 1
        Color color = saveSettingsFadingText.color;
        color.a = 1;
        saveSettingsFadingText.color = color;

        // Wait for 0.5 seconds
        yield return new WaitForSeconds(0.5f);

        // Fade out the text over time
        float startAlpha = saveSettingsFadingText.color.a;
        for (float t = 0.0f; t < 0.5f; t += Time.deltaTime)
        {
            float blend = t / 0.5f;
            color.a = Mathf.Lerp(startAlpha, 0, blend);
            saveSettingsFadingText.color = color;
            yield return null;
        }

        // Ensure the text is completely hidden after fading out
        color.a = 0;
        saveSettingsFadingText.color = color;
        saveSettingsFadingText.gameObject.SetActive(false);
    }

    private void SetSettingsObjectsActive(bool isActive)
    {
        exitButton.gameObject.SetActive(isActive);
        saveSettingsButton.gameObject.SetActive(isActive);
        exitSettingsButton.gameObject.SetActive(isActive);
        mainMenuButton.gameObject.SetActive(isActive);
        musicVolumeToggle.gameObject.SetActive(isActive);
        musicVolumeSlider.gameObject.SetActive(isActive);
        soundEffectsVolumeToggle.gameObject.SetActive(isActive);
        soundEffectsVolumeSlider.gameObject.SetActive(isActive);
    }
}
