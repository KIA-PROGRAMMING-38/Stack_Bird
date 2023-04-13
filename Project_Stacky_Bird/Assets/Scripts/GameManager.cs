using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public bool stop = false;

    [SerializeField] private GameObject Floor, White;

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
}
