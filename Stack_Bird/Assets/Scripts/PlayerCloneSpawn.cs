using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCloneSpawn : MonoBehaviour
{
    private Animator playerCloneAnim;

    private void Start()
    {
        playerCloneAnim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Invoke(nameof(PlayerCloneHide), 5);
    }

    private void OnDisable() 
    {
        ObjectPooler.ReturnToPool(gameObject);
        CancelInvoke();

        // 플레이어클론이 사라진 이후 알파값을 0으로 바꾼것을 다시 1로 되돌림
        Color color = GetComponent<SpriteRenderer>().color;
        color.a = 1f;
        GetComponent<SpriteRenderer>().color = color;
    }

    private void PlayerCloneHide()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            playerCloneAnim.SetBool("isCollision", true);
            Invoke(nameof(PlayerCloneHide), 2f);
        }
    }
}
