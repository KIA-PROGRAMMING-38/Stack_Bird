using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{ 
    public AudioSource BirdSound_1, BirdSound_2, BirdSound_3, BirdSound_4;

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
}
