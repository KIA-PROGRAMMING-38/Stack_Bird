using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{ 
    public AudioSource BirdSound_1, BirdSound_2, BirdSound_3, BirdSound_4;

    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private AudioSource Bgm;

    [SerializeField] Slider BgmSlider, SfxSlider;

    private static SoundManager instance;

    public static SoundManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(SoundManager)) as SoundManager;

                if (instance == null)
                    Debug.Log("no Singleton object");
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        // DontDestroyOnLoad(gameObject); 씬이 추가되면 사용
    }

    public void SetBgmVolume()
    {
        audioMixer.SetFloat("Bgm", Mathf.Log10(BgmSlider.value) * 20);
    }

    public void SetSfxVolume()
    {
        audioMixer.SetFloat("Sfx", Mathf.Log10(SfxSlider.value) * 20);
    }
}
