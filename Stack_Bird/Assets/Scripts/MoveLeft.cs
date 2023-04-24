using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private PlayerController _playerController;

    private float speed = 2.5f;

    private float leftBound = -8f;

    Vector2 moveVecLeft;

    void Start()
    {
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        moveVecLeft = Vector2.left * (speed * Time.deltaTime);

        if (_playerController.gameOver == false)
        {
            transform.Translate(moveVecLeft);
        }

        if (transform.position.x < leftBound && gameObject.CompareTag("WallManager"))
        {
            Debug.Log("Wall Destroy");
            Destroy(gameObject);
            //gameObject.SetActive(false);
        }
    }
}
