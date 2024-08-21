using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSliderMusic : MonoBehaviour
{
    private const string ProfilesKey = "Profiles";

    [SerializeField]
    private int currentProfile;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private TextMeshProUGUI valueText;

    public void OnChangeSlider(float Value)
    {
        valueText.SetText(Mathf.RoundToInt(Value * 100).ToString());
        audioSource.volume = Value;
    }

    // Start is called before the first frame update
    private void Start()
    {
        currentProfile = PlayerPrefs.GetInt(ProfilesKey + "_Selected");
        audioSource.volume = PlayerPrefs.GetFloat($"{ProfilesKey}_{currentProfile}_MusicVolume", 1.000f);
    }

    private void Awake()
    {
        audioSource = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
    }
}
