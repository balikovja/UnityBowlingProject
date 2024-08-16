using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    public TMP_InputField profileNameInput;
    public Button addProfileButton;
    public ProfileManager profileManager;
    public TextMeshProUGUI enterNameText;
    public TextMeshProUGUI errorText;

    void Start()
    {
        addProfileButton.onClick.AddListener(SaveProfile);
    }

    void SaveProfile()
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
                SceneManager.LoadScene("ProfileMenu");
            }
        }
    }

    IEnumerator NameEntryError()
    {
        SetMenuActive(false);
        errorText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        errorText.gameObject.SetActive(false);
        SetMenuActive(true);
    }

    void SetMenuActive(bool isActive)
    {
        profileNameInput.gameObject.SetActive(isActive);
        addProfileButton.gameObject.SetActive(isActive);
        enterNameText.gameObject.SetActive(isActive);
    }
}
