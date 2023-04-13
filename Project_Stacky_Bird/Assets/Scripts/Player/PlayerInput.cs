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

        if (transform.position.y > 4.75f)
            transform.position = new Vector3(-1.5f, 4.75f, 0f);
        if (transform.position.y < -2.65)
            transform.position = new Vector3(-1.5f, -2.65f, 0f);

        if (GameManager.Instance.stop) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.AddForce(Vector3.up * 250);
            SoundManager.Instance.BirdSound_1.Play();
            // GameManager.Instance.GameOver();
        }
    }
}
