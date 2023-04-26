using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCloneSpawn : MonoBehaviour
{
    private Animator playerCloneAnim;

    private void Start()
    {
        playerCloneAnim = GetComponent<Animator>();

        //StartCoroutine(PlayerCloneHideCoroutine());
    }

    //IEnumerator PlayerCloneHideCoroutine()
    //{
    //    PlayerCloneHide();
    //    yield return new WaitForSeconds(3f);
    //}

    private void OnDisable() 
    {
        // 플레이어클론이 사라진 이후 알파값을 0으로 바꾼것을 다시 1로 되돌림
        Color color = GetComponent<SpriteRenderer>().color;
        color.a = 1f;
        GetComponent<SpriteRenderer>().color = color;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("PlayerClone SetActive(false)");
            playerCloneAnim.SetBool("isCollision", true);
            // Destroy(gameObject, 2f);
            Invoke("PlayerCloneHide", 2f);
        }
    }

    private void PlayerCloneHide()
    {
        if (gameObject != null)
        {
            gameObject.SetActive(false);
        }
    }
}
