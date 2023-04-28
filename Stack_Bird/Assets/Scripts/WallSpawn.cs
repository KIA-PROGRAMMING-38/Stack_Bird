using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawn : MonoBehaviour
{
    [SerializeField] private GameObject wallPrefab;

    private Vector2 spawnPos;

    private float startDelay = 3f;
    private float repeatRate = 5f;

    private int randomSpawnPos_Min = 0;
    private int randomSpawnPos_Max = 6;
    private float spawnPos_x = 1f;
    private float spawnPos_y = 1.4f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnWall), startDelay, repeatRate);
    }

    private void SpawnWall()
    {
        if (PlayerController.gameOver == false)
        {
            spawnPos = new Vector2(0f, Random.Range(randomSpawnPos_Min, randomSpawnPos_Max)) + new Vector2(spawnPos_x, spawnPos_y);
            Instantiate(wallPrefab, spawnPos, wallPrefab.transform.rotation);
        }
    }
}
