using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSliderSoundEffects : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI valueText;

    public void OnChangeSlider(float Value)
    {
        valueText.SetText($"{Value.ToString("N4")}");
    }
}
