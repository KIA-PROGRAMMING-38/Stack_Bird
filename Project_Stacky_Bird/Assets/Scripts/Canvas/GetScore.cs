using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetScore : MonoBehaviour
{
    [SerializeField] private Text score;

    private int scoreIndex = 0;

    private int bestScore;

    Rigidbody2D rigidbody;

    [SerializeField] private GameObject Quit;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ScoreZone"))
        {
            score.text = (++scoreIndex).ToString();
            SoundManager.Instance.BirdSound_2.Play();
        }
        else if (GameManager.Instance.stop == false)
        {
            rigidbody.velocity = Vector3.zero;
            SoundManager.Instance.BirdSound_4.Play();

            score.gameObject.SetActive(false);

            if (PlayerPrefs.GetInt("bestScore", 0) < int.Parse(score.text))
                PlayerPrefs.SetInt("bestScore", int.Parse(score.text));

            Quit.SetActive(true);
            Quit.transform.Find("CurrentScoreScreen").GetComponent<Text>().text = score.text;
            Quit.transform.Find("BestScoreScreen").GetComponent<Text>().text = PlayerPrefs.GetInt("bestScore").ToString();

            GameManager.Instance.GameOver();
        }
    }
}
