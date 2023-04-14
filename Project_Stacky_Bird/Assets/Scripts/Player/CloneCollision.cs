using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 아래 있는 클론과 부딪혔다면 y포지션 고정
        if (collision.gameObject.CompareTag("PlayerClone"))
        {

        }
    }
}
