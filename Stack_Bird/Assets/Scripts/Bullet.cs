using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bullet : MonoBehaviour
{
    private AudioSource bulletAudio;

    [SerializeField] private AudioClip crashSound;

    [SerializeField] private float speed;

    Vector2 moveVecRight;

    private float rightBound = 2.4f;

    private void Start()
    {
        bulletAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        BulletMoveRight();
    }

    private void OnEnable()
    {
        Invoke(nameof(BulletHide), 5);
    }

    private void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);
        CancelInvoke();
    }

    private void BulletHide()
    {
        gameObject.SetActive(false);
    }

    private void BulletMoveRight()
    {
        moveVecRight = Vector2.right * (speed * Time.deltaTime);

        if (PlayerController.gameOver == false)
        {
            transform.Translate(moveVecRight);
        }

        if (transform.position.x > rightBound && gameObject.CompareTag("Bullet"))
        {
            BulletHide();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            PlayerController.actionPlayerController();

            bulletAudio.PlayOneShot(crashSound, 0.8f);

            collision.gameObject.SetActive(false);
            Invoke (nameof(BulletHide), 0.05f);
        }
    }
}
