using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    GetScore getScore;

    public bool stop = false;

    private int bestScore;

    [SerializeField] private GameObject Floor, White, Quit;

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
        getScore = GetComponent<GetScore>();

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        // DontDestroyOnLoad(gameObject); 씬이 추가되면 사용
    }

    public void GameOver()
    {
        Debug.Log("Gameover");

        stop = true;

        Floor.GetComponent<Animator>().enabled = false; // 죽었으니 Floor 애니메이션 비활성화

        White.SetActive(true);

        getScore.score.gameObject.SetActive(false);

        if (PlayerPrefs.GetInt("bestScore", 0) < int.Parse(getScore.score.text))
            PlayerPrefs.SetInt("bestScore", int.Parse(getScore.score.text));

        Quit.SetActive(true);
        Quit.transform.Find("CurrentScoreScreen").GetComponent<Text>().text = getScore.score.text;
        Quit.transform.Find("BestScoreScreen").GetComponent<Text>().text = PlayerPrefs.GetInt("bestScore").ToString();
    }
}
