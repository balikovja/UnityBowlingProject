using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioToggleSoundEffects : MonoBehaviour
{
    private const string ProfilesKey = "Profiles";

    [SerializeField]
    private int currentProfile;
    [SerializeField]
    private List<AudioSource> sources = new List<AudioSource>();

    public void OnToggle()
    {
        foreach (AudioSource source in sources)
        {
            source.mute = !source.mute;
        }
    }

    // Start is called before the first frame update
    private void Awake()
    {
        currentProfile = PlayerPrefs.GetInt(ProfilesKey + "_Selected");
        bool playerPrefMute = PlayerPrefs.GetInt($"{ProfilesKey}_{currentProfile}_SoundEffectsEnabled", 1) == 1 ? false : true;
        foreach (AudioSource source in sources)
        {
            source.mute = playerPrefMute;
        }
    }
}
