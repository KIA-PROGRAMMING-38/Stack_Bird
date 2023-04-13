using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetScore : MonoBehaviour
{
    public Text score;

    private int scoreIndex = 0;

    Rigidbody2D rigidbody;

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
            GameManager.Instance.GameOver();
        }
    }
}
