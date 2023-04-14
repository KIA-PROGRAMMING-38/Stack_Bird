using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private Slider BgmSlider, SfxSlider;

    [SerializeField] private GameObject onButton, offButton;

    public void SetBgmVolume()
    {
        audioMixer.SetFloat("Bgm", Mathf.Log10(BgmSlider.value) * 20);
    }

    public void SetSfxVolume()
    {
        audioMixer.SetFloat("Sfx", Mathf.Log10(SfxSlider.value) * 20);
    }
    public void OnclickOnButton()
    {
        AudioListener.volume = 0;
        onButton.SetActive(false);
        offButton.SetActive(true);
    }

    public void OnclickOffButton()
    {
        AudioListener.volume = 1;
        offButton.SetActive(false);
        onButton.SetActive(true);
    }
}