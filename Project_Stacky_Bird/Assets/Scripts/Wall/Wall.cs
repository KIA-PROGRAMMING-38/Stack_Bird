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
        if (Time.time > nextTime)
        {
            nextTime = (Time.time + 2.5f);
            walls[index] = Instantiate(wall, new Vector3(4f, Random.Range(-1f, 3.2f), 0f), Quaternion.identity);
            ++index;
            if (index == 3) index = 0;
        }
        if (walls[0])
        {
            walls[0].transform.Translate(-0.005f, 0, 0);
            if (walls[0].transform.position.x < -4f)
                Destroy(walls[0]);
        }
        if (walls[1])
        {
            walls[1].transform.Translate(-0.005f, 0, 0);
            if (walls[1].transform.position.x < -4f)
                Destroy(walls[1]);
        }
        if (walls[2])
        {
            walls[2].transform.Translate(-0.005f, 0, 0);
            if (walls[2].transform.position.x < -4f)
                Destroy(walls[2]);
        }
    }
}
