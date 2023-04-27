using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 2.5f;

    private float leftBound = -8f;

    Vector2 moveVecLeft;

    void Update()
    {
        moveVecLeft = Vector2.left * (speed * Time.deltaTime);

        if (PlayerController.gameOver == false)
        {
            transform.Translate(moveVecLeft);
        }

        if (transform.position.x < leftBound && gameObject.CompareTag("WallManager"))
        {
            Debug.Log("Wall Destroy");
            Destroy(gameObject);
        }
    }
}
