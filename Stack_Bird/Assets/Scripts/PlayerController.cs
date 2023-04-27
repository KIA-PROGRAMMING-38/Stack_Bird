using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;

    public static System.Action actionPlayerController;

    [SerializeField] private float jumpForce, gravityForce;
    [SerializeField] private ParticleSystem deadParticle, sparkParticle_1, sparkParticle_2;
    [SerializeField] private AudioClip jumpSound, deadSound, getScoreSound, perfectZoneSound, shootyModeSound;
    [SerializeField] private Text scoreText, perfectZoneText, shootyModeText;
    [SerializeField] private GameObject Quit, PlayerClonePrefab;

    private Vector2 playerCloneSpawnPos, bulletSpawnPos;

    public static bool gameOver { get; private set; } = false;
    private int score, bestScore, perfectZoneCount;
    public float playerLimitCeiling { get; private set; } = 5f;

    private void Awake()
    {
        actionPlayerController = () => { UpdateScore(15); };
    }

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
        if (collision.gameObject.CompareTag("GetScoreZone"))
        {
            playerAudio.PlayOneShot(getScoreSound, 0.8f);
            UpdateScore(3);
        }

        if (collision.gameObject.CompareTag("PerfectZone"))
        {
            ++perfectZoneCount;

            perfectZoneText.gameObject.SetActive(true);
            playerAudio.PlayOneShot(perfectZoneSound, 0.8f);
            StartCoroutine(TextHide(perfectZoneText));

            if (perfectZoneCount >= 3)
            {
                perfectZoneText.gameObject.SetActive(false);
                shootyModeText.gameObject.SetActive(true);
                playerAudio.PlayOneShot(shootyModeSound, 0.8f);
                StartCoroutine(TextHide(shootyModeText));

                StartCoroutine(TimeUpdate(0));

                perfectZoneCount = 0;
            }
        }
    }

    IEnumerator TextHide(Text textObj)
    {
        yield return new WaitForSeconds(2f);
        textObj.gameObject.SetActive(false);
    }

    private IEnumerator TimeUpdate(int timeCount)
    {
        while (timeCount <= 12)
        {
            ++timeCount;
            playerAnim.SetBool("isAttackMode", true);
            GameObject bullet = ObjectPooler.SpawnFromPool("Bullet", bulletSpawnPos);
            bullet.GetComponent<Bullet>();
            yield return new WaitForSeconds(0.5f);
            playerAnim.SetBool("isAttackMode", false);
        }
    }
}
