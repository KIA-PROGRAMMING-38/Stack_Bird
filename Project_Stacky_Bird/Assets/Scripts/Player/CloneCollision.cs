using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �Ʒ� �ִ� Ŭ�а� �ε����ٸ� y������ ����
        if (collision.gameObject.CompareTag("PlayerClone"))
        {

        }
    }
}
