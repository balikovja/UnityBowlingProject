using System;

public class Profile
{
    private string _name;
    private int _regularHighScore;
    private int _challengeHighScore;
    private bool _musicEnabled;
    private float _musicVolume;
    private bool _soundEffectsEnabled;
    private float _soundEffectsVolume;

    public string Name
    {
        get { return _name; }
        private set
        {
            if (!string.IsNullOrEmpty(value) && value.Length <= 20)
            {
                _name = value;
            }
            else
            {
                throw new ArgumentException("Name must be between 1 and 20 characters.");
            }
        }
    }

    public int RegularHighScore
    {
        get { return _regularHighScore; }
        private set
        {
            if (value >= 0)
            {
                _regularHighScore = value;
            }
            else
            {
                throw new ArgumentException("Regular high score cannot be negative.");
            }
        }
    }

    public int ChallengeHighScore
    {
        get { return _challengeHighScore; }
        private set
        {
            if (value >= 0)
            {
                _challengeHighScore = value;
            }
            else
            {
                throw new ArgumentException("Challenge high score cannot be negative.");
            }
        }
    }

    public bool MusicEnabled
    {
        get { return _musicEnabled; }
        private set
        {
            _musicEnabled = value;
        }
    }

    public float MusicVolume
    {
        get { return _musicVolume; }
        private set
        {
            if (value >= 0.0001 && value <= 1.0000)
            {
                _musicVolume = value;
            }
            else
            {
                throw new ArgumentException("Music volume must be between 0.0001 and 1.0000 (inclusive).");
            }
        }
    }

    public bool SoundEffectsEnabled
    {
        get { return _soundEffectsEnabled; }
        private set
        {
            _soundEffectsEnabled = value;
        }
    }

    public float SoundEffectsVolume
    {
        get { return _soundEffectsVolume; }
        private set
        {
            if (value >= 0.0001 && value <= 1.0000)
            {
                _soundEffectsVolume = value;
            }
            else
            {
                throw new ArgumentException("Sound effects volume must be between 0.0001 and 1.0000 (inclusive).");
            }
        }
    }

    public Profile(string name, int regularHighScore, int challengeHighScore, bool musicEnabled, float musicVolume, bool soundEffectsEnabled, float soundEffectsVolume)
    {
        Name = name;  // Validation occurs in the property setter
        RegularHighScore = regularHighScore;  // Validation occurs in the property setter
        ChallengeHighScore = challengeHighScore;  // Validation occurs in the property setter
        MusicEnabled = musicEnabled;  // Validation occurs in the property setter
        MusicVolume = musicVolume;  // Validation occurs in the property setter
        SoundEffectsEnabled = soundEffectsEnabled;  // Validation occurs in the property setter
        SoundEffectsVolume = soundEffectsVolume;  // Validation occurs in the property setter
    }
}
