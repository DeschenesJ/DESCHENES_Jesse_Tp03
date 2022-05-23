using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class AudioManager : MonoBehaviour
{
    // Les sliders
    public Slider sliderMaster;
    public Slider sliderMusic;
    public Slider sliderSFX;

    //Le Mixer Audio
    public AudioMixer audioMixer;


    // Start is called before the first frame update
    void Start()
    {
        sliderMaster.onValueChanged.AddListener(sliderMaster_OnValueChange);
        sliderMusic.onValueChanged.AddListener(sliderMusic_OnValueChange);
        sliderSFX.onValueChanged.AddListener(sliderSFX_OnValueChange);
    }

    // Réglage du volume Master
    void sliderMaster_OnValueChange(float value)
    {
        audioMixer.SetFloat("GainMaster",Mathf.Log(value) * 20f);
    }

    // Réglage du volume Music
    void sliderMusic_OnValueChange(float value)
    {
        audioMixer.SetFloat("GainMusic", Mathf.Log(value) * 20f);
    }

    // Réglage du volume des bruits
    void sliderSFX_OnValueChange(float value)
    {
        audioMixer.SetFloat("GainSFX", Mathf.Log(value) * 20f);
    }
}
