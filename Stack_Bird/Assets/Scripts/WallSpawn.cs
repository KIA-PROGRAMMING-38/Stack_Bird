using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawn : MonoBehaviour
{
    [SerializeField] private GameObject wallPrefab;

    private Vector2 spawnPos;

    private float startDelay = 3f;
    private float repeatRate = 5f;

    void Start()
    {
        InvokeRepeating("SpawnWall", startDelay, repeatRate);
    }

    private void SpawnWall()
    {
        if (PlayerController.gameOver == false)
        {
            spawnPos = new Vector2(0f, Random.Range(1f, 5f));
            Instantiate(wallPrefab, spawnPos, wallPrefab.transform.rotation);
        }
    }
}
