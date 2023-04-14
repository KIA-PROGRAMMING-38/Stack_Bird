using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // �ִϸ������� Velocity �Ķ���Ϳ� �÷��̾��� y �ӵ� �����͸� �ش�.
        GetComponent<Animator>().SetFloat("Velocity", rigidbody.velocity.y);

        if (GameManager.Instance.stop) return;

        if (Input.GetKeyDown(KeyCode.Space))
            PlayerJump();
    }

    public void PlayerJump()
    {
        rigidbody.AddForce(Vector3.up * 225);
        SoundManager.Instance.BirdSound_1.Play();
    }
}
