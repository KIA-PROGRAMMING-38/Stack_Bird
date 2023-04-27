using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private PlayerController _playerController;

    [SerializeField] private float speed;

    Vector2 moveVecRight;

    void Start()
    {
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
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

        if (_playerController.gameOver == false)
        {
            transform.Translate(moveVecRight);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // º®ÀÌ¶û ºÎµúÈ÷¸é º® ÆÄ±«, Åë°úº¸´Ù ´õ ¸¹Àº Á¡¼ö È¹µæ
            _playerController.UpdateScore(15);
        }
    }
}
