using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClone : MonoBehaviour
{
    [SerializeField] private GameObject PlayerClonePrefab;

    private GameObject[] clones = new GameObject[20];

    private float nextTime = 0;

    private int index = 0;

    private void Update()
    {
        if (GameManager.Instance.stop) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreatePlayerClone();
        }

        for (int i = 0; i < clones.Length; ++i)
        {
            DestroyPlayerClone(i);
        }
    }

    private void CreatePlayerClone()
    {
        clones[index] = Instantiate(PlayerClonePrefab, new Vector3(transform.position.x, transform.position.y - 0.5f, 0f), Quaternion.identity);
        ++index;
        if (index == 20) index = 0;
    }

    private void DestroyPlayerClone(int i)
    {
        if (clones[i])
        {
            if (Time.time > nextTime)
            {
                nextTime = (Time.time + 2f);
                Destroy(clones[i]);
            }
        }
    }
}
