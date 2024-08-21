using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AddProfileMenuHandler : MonoBehaviour
{
    private const string ProfilesKey = "Profiles";
    public TMP_InputField profileNameInput;
    public Button addProfileButton;
    public Button cancelButton;
    public ProfileManager profileManager;
    public TextMeshProUGUI enterNameText;
    public TextMeshProUGUI errorText;
    public AudioSource audioSource;

    private void Start()
    {
        addProfileButton.onClick.AddListener(SaveProfile);
        cancelButton.onClick.AddListener(Cancel);
        int currentProfile = PlayerPrefs.GetInt(ProfilesKey + "_Selected");
        bool playerPrefMute = PlayerPrefs.GetInt($"{ProfilesKey}_{currentProfile}_SoundEffectsEnabled", 1) == 1 ? false : true;
        float playerPrefVolume = PlayerPrefs.GetFloat($"{ProfilesKey}_{currentProfile}_SoundEffectsVolume", 1.000f);
        audioSource.mute = playerPrefMute;
        audioSource.volume = playerPrefVolume;
    }

    private void SaveProfile()
    {
        string profileName = profileNameInput.text;

        List<Profile> profileList = profileManager.GetProfiles();

        if (string.IsNullOrEmpty(profileName))
        {
            errorText.text = "Profile Name Cannot Be Empty";
            StartCoroutine(NameEntryError());
        }
        else if (profileName.Length > 20)
        {
            errorText.text = "Profile Name Must Be 20 Characters Or Less";
            StartCoroutine(NameEntryError());
        }
        else
        {
            bool usernameTaken = false;
            foreach (Profile profile in profileList)
            {
                if (profile.Name == profileName)
                {
                    usernameTaken = true;
                    errorText.text = "Profile Name Already Exists";
                    StartCoroutine(NameEntryError());
                    break;
                }
            }
            if (!usernameTaken)
            {
                profileManager.AddProfile(new Profile(profileName, 0, 0, true, 1.0000f, true, 1.0000f));
                StartCoroutine(ReturnToProfileCoroutine());
            }
        }
    }

    private IEnumerator ReturnToProfileCoroutine()
    {
        // ensures button sound effect plays
        yield return new WaitForSeconds(0.06f);
        SceneManager.LoadScene("ProfileMenu");
    }

    private IEnumerator NameEntryError()
    {
        SetMenuActive(false);
        errorText.gameObject.SetActive(true);
        cancelButton.gameObject.SetActive(false);
        yield return new WaitForSeconds(3.0f);
        errorText.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(true);
        SetMenuActive(true);
    }

    private void SetMenuActive(bool isActive)
    {
        profileNameInput.gameObject.SetActive(isActive);
        addProfileButton.gameObject.SetActive(isActive);
        enterNameText.gameObject.SetActive(isActive);
    }

    private void Cancel()
    {
        StartCoroutine(ReturnToProfileCoroutine());
    }
}
