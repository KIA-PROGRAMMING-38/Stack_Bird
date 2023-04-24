using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private PlayerController _playerControllerScript;

    private float speed = 2.5f;

    private float leftBound = -8f;

    void Start()
    {
        _playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (_playerControllerScript.gameOver == false)
        {
            transform.Translate(Vector2.left * (speed * Time.deltaTime));
        }

        if (transform.position.x < leftBound && gameObject.CompareTag("WallManager"))
        {
            Debug.Log("Wall Destroy");
            Destroy(gameObject);
        }
    }
}
