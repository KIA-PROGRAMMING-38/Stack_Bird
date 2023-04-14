using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public bool stop = false;

    [SerializeField] private GameObject Floor, White, SettingScreenBoard, First, Player, PlayerPreview;

    public static GameManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(GameManager)) as GameManager;

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
        // DontDestroyOnLoad(gameObject); ���� �߰��Ǹ� ���
    }

    public void GameOver()
    {
        Debug.Log("Gameover");

        stop = true;

        Floor.GetComponent<Animator>().enabled = false; // �׾����� Floor �ִϸ��̼� ��Ȱ��ȭ

        White.SetActive(true);
    }

    public void OnClickRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickSetting()
    {
        Time.timeScale = 0f;
        SettingScreenBoard.SetActive(true);
    }

    public void OnClickExit()
    {
        Time.timeScale = 1f;
        SettingScreenBoard.SetActive(false);
    }

    public void OnClickTapToPlay()
    {
        First.SetActive(false);
        Player.SetActive(true);
        PlayerPreview.SetActive(false);
    }
}
