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
        // 애니메이터의 Velocity 파라미터에 플레이어의 y 속도 데이터를 준다.
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
