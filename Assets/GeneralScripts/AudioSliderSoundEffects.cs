using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSliderSoundEffects : MonoBehaviour
{
    private const string ProfilesKey = "Profiles";

    [SerializeField]
    private int currentProfile;
    [SerializeField]
    private List<AudioSource> sources = new List<AudioSource>();
    [SerializeField]
    private TextMeshProUGUI valueText;

    public void OnChangeSlider(float Value)
    {
        valueText.SetText(Mathf.RoundToInt(Value * 100).ToString());
        foreach (AudioSource source in sources)
        {
            source.volume = Value;
        }
    }

    void Start()
    {
        currentProfile = PlayerPrefs.GetInt(ProfilesKey + "_Selected");
        float playerPrefVolume = PlayerPrefs.GetFloat($"{ProfilesKey}_{currentProfile}_SoundEffectsVolume", 1.000f);
        foreach (AudioSource source in sources)
        {
            source.volume = playerPrefVolume;
        }
    }
}
