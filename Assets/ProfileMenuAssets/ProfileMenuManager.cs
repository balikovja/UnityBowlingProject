using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using TMPro;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProfileMenuManager : MonoBehaviour
{
    public Canvas settingsCanvas;
    public Canvas profileMenuCanvas;
    public GameObject buttonPrefab;  
    private List<GameObject> buttonList = new List<GameObject>();
    private GameObject lastPressedButton;
    public Transform content;        
    public ProfileManager profileManager;
    public Button addProfileButton;
    public Button selectProfileButton;
    public Button deleteProfileButton;
    public Button settingsButton;
    private Profile selectedProfile;
    private readonly ColorBlock pressedColorBlock = new ColorBlock { 
        normalColor = Color.gray, 
        highlightedColor = Color.gray, 
        pressedColor = Color.gray, 
        selectedColor = Color.gray, 
        disabledColor = Color.gray, 
        colorMultiplier = 1.0f, 
        fadeDuration = 0.1f 
    };
    private int numOfProfiles;

    private void Start()
    {
        profileMenuCanvas.gameObject.SetActive(true);
        settingsCanvas.gameObject.SetActive(true);
        settingsCanvas.gameObject.SetActive(false);
        PopulateProfileList();
        addProfileButton.onClick.AddListener(AddProfile);
        selectProfileButton.onClick.AddListener(SelectProfile);
        deleteProfileButton.onClick.AddListener(DeleteProfile);
        settingsButton.onClick.AddListener(Settings);
        lastPressedButton = null;
    }

    private void PopulateProfileList()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);  // Clear previous buttons
        }

        List<Profile> profiles = profileManager.GetProfiles();
        foreach (Profile profile in profiles)
        {
            GameObject newButton = Instantiate(buttonPrefab, content);
            buttonList.Add(newButton);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = $"{profile.Name}\nHighscore (Regular): {profile.RegularHighScore}\nHighscore (Challenge): {profile.ChallengeHighScore}";

            // Add a listener to handle profile selection
            newButton.GetComponent<Button>().onClick.AddListener(() => OnProfileSelected(profile, newButton));
        }
        if (profiles.Count == 0)
        {
            GameObject newButton = Instantiate(buttonPrefab, content);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = $"No Profiles Found - Please Add One";
            newButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 40;
            newButton.GetComponentInChildren<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
            newButton.GetComponentInChildren<TextMeshProUGUI>().verticalAlignment = VerticalAlignmentOptions.Middle;
        }
        numOfProfiles = profiles.Count;
    }

    private void OnProfileSelected(Profile selectedProfile, GameObject pressedButton)
    {
        if (lastPressedButton != null)
        {
            lastPressedButton.GetComponent<Button>().colors = ColorBlock.defaultColorBlock;
        }
        Button pressedButtonComponent = pressedButton.GetComponent<Button>();
        pressedButtonComponent.colors = pressedColorBlock;
        lastPressedButton = pressedButton;
        this.selectedProfile = selectedProfile;
    }

    private void AddProfile()
    {
        StartCoroutine(AddProfileScreenCoroutine());
    }

    private IEnumerator AddProfileScreenCoroutine()
    {
        // ensures button sound plays
        yield return new WaitForSeconds(0.06f);
        SceneManager.LoadScene("AddProfileScreen");
    }

    void SelectProfile()
    {
        if (selectedProfile != null)
        {
            StartCoroutine(SelectProfileCoroutine());
        }
        
    }

    private IEnumerator SelectProfileCoroutine()
    {
        // ensures button sound plays
        yield return new WaitForSeconds(0.06f);
        profileManager.SelectProfile(selectedProfile);
        SceneManager.LoadScene("MainMenu");
    }

    private void DeleteProfile()
    {
        if (lastPressedButton != null && selectedProfile != null)
        {
            Destroy(lastPressedButton);
            profileManager.DeleteProfile(selectedProfile);
            selectedProfile = null;
            lastPressedButton = null;
        }
        numOfProfiles--;
        if (numOfProfiles == 0)
        {
            PopulateProfileList();
        }
    }

    private void Settings()
    {
        settingsCanvas.gameObject.SetActive(true);
        profileMenuCanvas.gameObject.SetActive(false);
    }
}
