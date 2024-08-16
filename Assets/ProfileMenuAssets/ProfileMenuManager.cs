using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using TMPro;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProfileMenuManager : MonoBehaviour
{
    public GameObject buttonPrefab;  
    private List<GameObject> buttonList = new List<GameObject>();
    private GameObject lastPressedButton;
    public Transform content;        
    public ProfileManager profileManager;
    public Button addProfileButton;
    public Button selectProfileButton;
    public Button deleteProfileButton;
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

    private void Start()
    {
        PopulateProfileList();
        addProfileButton.onClick.AddListener(AddProfile);
        selectProfileButton.onClick.AddListener(SelectProfile);
        deleteProfileButton.onClick.AddListener(DeleteProfile);
        lastPressedButton = null;
    }

    void PopulateProfileList()
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
    }

    void OnProfileSelected(Profile selectedProfile, GameObject pressedButton)
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

    void AddProfile()
    {
        SceneManager.LoadScene("AddProfileScreen");
    }

    void SelectProfile()
    {
        if (selectedProfile != null)
        {
            profileManager.SelectProfile(selectedProfile);
            SceneManager.LoadScene("MainMenu");
        }
        
    }

    void DeleteProfile()
    {
        if (lastPressedButton != null && selectedProfile != null)
        {
            Destroy(lastPressedButton);
            profileManager.DeleteProfile(selectedProfile);
            selectedProfile = null;
            lastPressedButton = null;
        }
    }
}
