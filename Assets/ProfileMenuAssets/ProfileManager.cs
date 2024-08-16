using UnityEngine;
using System.Collections.Generic;

public class ProfileManager : MonoBehaviour
{
    private const string ProfilesKey = "Profiles";

    [SerializeField]
    private List<Profile> profiles = new List<Profile>();

    private void Start()
    {
        LoadProfiles();
    }

    public void SelectProfile(Profile profile)
    {
        for (int i = 0; i < profiles.Count; i++)
        {
            if (profiles[i].Name == profile.Name)
            {
                PlayerPrefs.SetInt(ProfilesKey + "_Selected", i);
            }
        }
    }

    public void AddProfile(Profile profile)
    {
        profiles.Add(profile);
        SaveProfiles();
    }

    public void DeleteProfile(Profile profile)
    {
        // Delete the profile with given name, if it was the selected profile
        // mark no profile as selected, if it was earlier in the list than the
        // selected profile, decrease selected by 1 to account for list shrinking
        for(int i = 0; i < profiles.Count; i++)
        {
            if (profiles[i].Name == profile.Name)
            {
                profiles.RemoveAt(i);
                int selectedProfile = PlayerPrefs.GetInt(ProfilesKey + "_Selected", -1);
                if (selectedProfile == i)
                {
                    PlayerPrefs.SetInt(ProfilesKey + "_Selected", -1);
                }else if (selectedProfile > i)
                {
                    PlayerPrefs.SetInt(ProfilesKey + "_Selected", selectedProfile-1);
                }
            }
        }
        SaveProfiles();
    }

    public List<Profile> GetProfiles()
    {
        return profiles;
    }

    private void SaveProfiles()
    {
        int count = PlayerPrefs.GetInt(ProfilesKey + "_Count", 0);

        // Delete all associated profile keys in PlayerPrefs
        for (int i = 0; i < count; i++)
        {
            PlayerPrefs.DeleteKey($"{ProfilesKey}_{i}_Name");
            PlayerPrefs.DeleteKey($"{ProfilesKey}_{i}_RegularHighScore");
            PlayerPrefs.DeleteKey($"{ProfilesKey}_{i}_ChallengeHighScore");
            PlayerPrefs.DeleteKey($"{ProfilesKey}_{i}_MusicEnabled");
            PlayerPrefs.DeleteKey($"{ProfilesKey}_{i}_MusicVolume");
            PlayerPrefs.DeleteKey($"{ProfilesKey}_{i}_SoundEffectsEnabled");
            PlayerPrefs.DeleteKey($"{ProfilesKey}_{i}_SoundEffectsVolume");
        }

        // Save the updated list of profiles
        for (int i = 0; i < profiles.Count; i++)
        {
            var profile = profiles[i];
            PlayerPrefs.SetString($"{ProfilesKey}_{i}_Name", profile.Name);
            PlayerPrefs.SetInt($"{ProfilesKey}_{i}_RegularHighScore", profile.RegularHighScore);
            PlayerPrefs.SetInt($"{ProfilesKey}_{i}_ChallengeHighScore", profile.ChallengeHighScore);
            PlayerPrefs.SetInt($"{ProfilesKey}_{i}_MusicEnabled", profile.MusicEnabled ? 1 : 0);
            PlayerPrefs.SetFloat($"{ProfilesKey}_{i}_MusicVolume", profile.MusicVolume);
            PlayerPrefs.SetInt($"{ProfilesKey}_{i}_SoundEffectsEnabled", profile.SoundEffectsEnabled ? 1 : 0);
            PlayerPrefs.SetFloat($"{ProfilesKey}_{i}_SoundEffectsVolume", profile.SoundEffectsVolume);
        }

        // Update Count and save PlayerPrefs
        PlayerPrefs.SetInt(ProfilesKey + "_Count", profiles.Count);
        PlayerPrefs.Save();
    }

    private void LoadProfiles()
    {
        profiles.Clear();

        int count = PlayerPrefs.GetInt(ProfilesKey + "_Count", 0);

        for (int i = 0; i < count; i++)
        {
            string name = PlayerPrefs.GetString($"{ProfilesKey}_{i}_Name");
            int regularHighScore = PlayerPrefs.GetInt($"{ProfilesKey}_{i}_RegularHighScore");
            int challengeHighScore = PlayerPrefs.GetInt($"{ProfilesKey}_{i}_ChallengeHighScore");
            bool musicEnabled = PlayerPrefs.GetInt($"{ProfilesKey}_{i}_MusicEnabled") == 1 ? true : false;
            float musicVolume = PlayerPrefs.GetFloat($"{ProfilesKey}_{i}_MusicVolume");
            bool soundEffectsEnabled = PlayerPrefs.GetInt($"{ProfilesKey}_{i}_SoundEffectsEnabled") == 1 ? true : false;
            float soundEffectsVolume = PlayerPrefs.GetFloat($"{ProfilesKey}_{i}_SoundEffectsVolume");

            var profile = new Profile(name, regularHighScore, challengeHighScore, musicEnabled, musicVolume, soundEffectsEnabled, soundEffectsVolume);
            profiles.Add(profile);
        }
    }
}
