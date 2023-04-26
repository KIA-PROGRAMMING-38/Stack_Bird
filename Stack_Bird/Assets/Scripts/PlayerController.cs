using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    private Vector2 playerCloneSpawnPos;

    public bool gameOver = false;
    private int score, bestScore;
    private float playerLimitCeiling = 5f;
    private float elapsedTime;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

        Physics.gravity = Physics.gravity * gravityForce;

        score = 0;
        UpdateScore(0);

        elapsedTime = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !gameOver && transform.position.y < playerLimitCeiling)
        {
            SpawnPlayerClone();

            playerRb.AddForce(Vector3.up * jumpForce);

            sparkParticle_1.Stop();
            sparkParticle_2.Stop();

            playerAudio.PlayOneShot(jumpSound, 0.8f);
        }
    }

    private void UpdateScore(int scoreAdd)
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

    private void SpawnPlayerClone()
    {
        playerCloneSpawnPos = new Vector2(transform.position.x, transform.position.y);
        //var clone = Instantiate(PlayerClonePrefab, playerCloneSpawnPos, transform.rotation);
        GameObject playerCloneCreate = ObjectPooler.SharedInstance.GetPooledObject();
        if (playerCloneCreate != null)
        {
            playerCloneCreate.SetActive(true);
            playerCloneCreate.transform.position = playerCloneSpawnPos;

            //elapsedTime += Time.deltaTime;
            //if (elapsedTime > 3f)
            //{
            //    Debug.Log("Test");
            //    playerCloneCreate.SetActive(false);
            //    elapsedTime = 0f;
            //}
        }

        //Destroy(clone, 3f);
        //Invoke("PlayerCloneHide", 3f);

        // collider 비활성화
        //PlayerClonePrefab.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    //private void PlayerCloneHide()
    //{
    //    PlayerClonePrefab.gameObject.SetActive(false);
    //}

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
    }
}
