using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private GameObject WallPrefab;

    private GameObject[] walls = new GameObject[3];
    
    private float nextTime = 0;

    private int index = 0;

    // º® »ý¼º
    private void Update()
    {
        if (GameManager.Instance.stop) return;

        if (Time.time > nextTime)
        {
            nextTime = (Time.time + 2.5f);
            walls[index] = Instantiate(WallPrefab, new Vector3(4f, Random.Range(-1f, 3.2f), 0f), Quaternion.identity);
            ++index;
            if (index == 3) index = 0;
        }

        for (int i = 0; i < walls.Length; ++i)
        {
            WallCreate(i);
        }
    }

    private void WallCreate(int i)
    {
        if (walls[i])
        {
            walls[i].transform.Translate(-0.01f, 0, 0);
            if (walls[i].transform.position.x < -4f)
                Destroy(walls[i]);
        }
    }
}
