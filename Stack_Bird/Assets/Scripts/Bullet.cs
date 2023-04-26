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
            // ���̶� �ε����� �� �ı�, ������� �� ���� ���� ȹ��
            _playerController.UpdateScore(15);
        }
    }
}
