using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;

    [SerializeField] private float jumpForce, gravityForce;
    [SerializeField] private ParticleSystem deadParticle, sparkParticle_1, sparkParticle_2;
    [SerializeField] private AudioClip jumpSound, deadSound, getScoreSound;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject Quit, PlayerClonePrefab;

    private Vector2 playerCloneSpawnPos, bulletSpawnPos;

    public bool gameOver { get; private set; } = false;
    private int score, bestScore;
    public float playerLimitCeiling { get; private set; } = 5f;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

        Physics.gravity = Physics.gravity * gravityForce;

        score = 0;
        UpdateScore(0);
    }

    void Update()
    {
        playerCloneSpawnPos = new Vector2(transform.position.x, transform.position.y);
        bulletSpawnPos = new Vector2(transform.position.x + 0.5f, transform.position.y - 0.5f);

        if (Input.GetKeyDown(KeyCode.Space) && !gameOver && transform.position.y < playerLimitCeiling)
        {
            GameObject playerClone = ObjectPooler.SpawnFromPool("PlayerClone", playerCloneSpawnPos);
            playerClone.GetComponent<PlayerCloneSpawn>();
            ObjectPooler.SetOrderInLayer();

            playerRb.AddForce(Vector3.up * jumpForce);

            sparkParticle_1.Stop();
            sparkParticle_2.Stop();

            playerAudio.PlayOneShot(jumpSound, 0.8f);
        }
    }

    public void UpdateScore(int scoreAdd)
    {
        score += scoreAdd;
        scoreText.text = $"{score}";
    }

    private void GameOver()
    {
        gameOver = true;

        scoreText.gameObject.SetActive(false);
        if (PlayerPrefs.GetInt("bestScore", 0) < int.Parse(scoreText.text))
        {
            PlayerPrefs.SetInt("bestScore", int.Parse(scoreText.text));
        }
        Quit.SetActive(true);
        Quit.transform.Find("ScoreScreenBoard").GetComponent<Text>().text = scoreText.text;
        Quit.transform.Find("BestScoreScreenBoard").GetComponent<Text>().text = PlayerPrefs.GetInt("bestScore").ToString();
    }

    public void ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("SafeZone"))
        {
            sparkParticle_1.Play();
            sparkParticle_2.Play();
        }

        else if (collision.gameObject.CompareTag("Wall")) 
        {
            GameOver();

            playerAnim.SetBool("isDeath", true);

            deadParticle.Play();
            sparkParticle_1.Stop();
            sparkParticle_2.Stop();

            playerAudio.PlayOneShot(deadSound, 0.8f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int onTriggerCount = 0;
        ++onTriggerCount;

        if (collision.gameObject.CompareTag("GetScoreZone"))
        {
            playerAudio.PlayOneShot(getScoreSound, 0.8f);
            UpdateScore(3);
        }

        if (onTriggerCount > 3)
        {
            if (collision.gameObject.CompareTag("PerfectZone"))
            {
                playerAnim.SetBool("isAttackMode", true);

                GameObject bullet = ObjectPooler.SpawnFromPool("Bullet", bulletSpawnPos);
                bullet.GetComponent<Bullet>();
            }

            onTriggerCount = 0;
        }
    }
}
