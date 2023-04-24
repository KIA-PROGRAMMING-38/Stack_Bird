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

    private void PlayerCloneHide()
    {
        gameObject.SetActive(false);
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
}
