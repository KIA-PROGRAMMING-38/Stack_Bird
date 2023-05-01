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
