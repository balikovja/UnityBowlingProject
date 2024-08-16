using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AudioToggleMusic : MonoBehaviour
{
    private const string ProfilesKey = "Profiles";

    private int currentProfile;
    [SerializeField]
    private AudioSource audioSource;

    public void OnToggle()
    {
        audioSource.mute = !audioSource.mute;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentProfile = PlayerPrefs.GetInt(ProfilesKey + "_Selected");
        audioSource.mute = PlayerPrefs.GetInt($"{ProfilesKey}_{currentProfile}_MusicEnabled") == 1 ? false : true;
    }
}
