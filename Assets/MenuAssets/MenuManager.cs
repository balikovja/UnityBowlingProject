using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private const string ProfilesKey = "Profiles";

    public Button playButton;
    public Button profilesButton;
    public Button creditsButton;
    public Button mainMenuButton;
    public Canvas mainMenuCanvas;
    public Canvas creditsCanvas;
    public GameObject menuMusicHandler;
    public TextMeshProUGUI selectedProfileText;
    public TextMeshProUGUI pleaseSelectProfileText;
    [SerializeField]
    private int selectedProfile;


    private void Start()
    {
        menuMusicHandler = GameObject.FindGameObjectWithTag("Music");
        pleaseSelectProfileText.gameObject.SetActive(false);
        creditsCanvas.gameObject.SetActive(false);
        playButton.onClick.AddListener(ClickPlay);
        profilesButton.onClick.AddListener(ClickProfiles);
        creditsButton.onClick.AddListener(ShowCredits);
        mainMenuButton.onClick.AddListener(ReturnToMenu);
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
            Destroy(menuMusicHandler);
            SceneManager.LoadScene("RegularBowling");
        }
        else
        {
            StartCoroutine(PleaseSelectProfile());
        }
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
}
