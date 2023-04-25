using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    [SerializeField] private GameObject settingMenu, soundOnImg, soundOffImg;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider MusicSlider, SoundSlider;

    private bool isMuted = false;
    private int trueInt = 1;
    private int falseInt = 0;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("isMuted"))
        {
            PlayerPrefs.SetInt("muted", falseInt);
            LoadValue();
        }
        else
        {
            LoadValue();
        }

        UpdateButtonImage();
        AudioListener.pause = isMuted;
    }

    public void OnClickSettingButton()
    {
        Time.timeScale = 0f;
        settingMenu.SetActive(true);
    }

    public void OnClickExitSettingButton()
    {
        Time.timeScale = 1f;
        settingMenu.SetActive(false);
    }

    public void SetMusicVolume()
    {
        audioMixer.SetFloat("BGM_Music", Mathf.Log10(MusicSlider.value) * 20);
    }

    public void SetSoundVolume()
    {
        audioMixer.SetFloat("SFX_Sound", Mathf.Log10(SoundSlider.value) * 20);
    }

    public void OnClickMuteButton()
    {
        if (isMuted == false)
        {
            isMuted = true;
            AudioListener.pause = true;
        }
        else
        {
            isMuted = false;
            AudioListener.pause = false;
        }

        SavedValue();
        UpdateButtonImage();
    }

    private void UpdateButtonImage()
    {
        if (isMuted == false)
        {
            soundOnImg.SetActive(true);
            soundOffImg.SetActive(false);
        }
        else
        {
            soundOnImg.SetActive(false);
            soundOffImg.SetActive(true);
        }
    }

    private void LoadValue()
    {
        // 저장된 isMuted가 1이면 true, 0이면 false
        isMuted = PlayerPrefs.GetInt("isMuted") == trueInt;
    }

    private void SavedValue()
    {
        // isMuted가 true면 1이 저장, false면 0을 저장
        PlayerPrefs.SetInt("isMuted", isMuted ? trueInt : falseInt);
    }
}
