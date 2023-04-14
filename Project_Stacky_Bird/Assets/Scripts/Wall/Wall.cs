using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private GameObject wall;

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
            walls[index] = Instantiate(wall, new Vector3(4f, Random.Range(-1f, 3.2f), 0f), Quaternion.identity);
            ++index;
            if (index == 3) index = 0;
        }
        WallCreate(0);
        WallCreate(1);
        WallCreate(2);
    }

    private void WallCreate(int index)
    {
        if (walls[index])
        {
            walls[index].transform.Translate(-0.01f, 0, 0);
            if (walls[index].transform.position.x < -4f)
                Destroy(walls[index]);
        }
    }
}
