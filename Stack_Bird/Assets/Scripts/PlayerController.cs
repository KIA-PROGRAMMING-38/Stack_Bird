using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;

    [SerializeField] private float jumpForce, gravityForce;
    [SerializeField] private ParticleSystem deadParticle, sparkParticle_1, sparkParticle_2;
    [SerializeField] private AudioClip jumpSound, deadSound;

    public bool gameOver = false;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

        Physics.gravity = Physics.gravity * gravityForce;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce);

            sparkParticle_1.Stop();
            sparkParticle_2.Stop();

            playerAudio.PlayOneShot(jumpSound, 0.8f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerClone") || collision.gameObject.CompareTag("Floor"))
        {
            sparkParticle_1.Play();
            sparkParticle_2.Play();
        }

        else if (collision.gameObject.CompareTag("Wall")) 
        {
            Debug.Log("Game Over!");
            gameOver = true;

            playerAnim.SetBool("isDeath", true);

            deadParticle.Play();
            sparkParticle_1.Stop();
            sparkParticle_2.Stop();

            playerAudio.PlayOneShot(deadSound, 0.8f);
        }
    }
}
