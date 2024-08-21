using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AudioToggleMusic : MonoBehaviour
{
    private const string ProfilesKey = "Profiles";

    [SerializeField]
    private int currentProfile;
    [SerializeField]
    private AudioSource audioSource;

    public void OnToggle()
    {
        audioSource.mute = !audioSource.mute;
    }

    private void Awake()
    {
        audioSource = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        currentProfile = PlayerPrefs.GetInt(ProfilesKey + "_Selected");
        audioSource.mute = PlayerPrefs.GetInt($"{ProfilesKey}_{currentProfile}_MusicEnabled", 1) == 1 ? false : true;
    }
}
