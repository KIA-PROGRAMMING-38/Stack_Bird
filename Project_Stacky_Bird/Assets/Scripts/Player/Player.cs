using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        PlayerMoveRangeLimit();
    }

    private void PlayerMoveRangeLimit()
    {
        if (transform.position.y >= 4.75f)
            transform.position = new Vector3(-1.5f, 4.75f, 0f);
        if (transform.position.y <= -2.65)
            transform.position = new Vector3(-1.5f, -2.65f, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerClone"))
        {
            transform.position = gameObject.transform.position;
        }
    }

    //private void OnCollisionExit2D(Collision2D collision)
    //{

    //}
}
