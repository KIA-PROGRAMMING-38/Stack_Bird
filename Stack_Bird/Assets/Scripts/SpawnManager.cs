using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private PlayerController _playerControllerScript;

    [SerializeField] private GameObject wallPrefab;

    private Vector2 spawnPos;

    private float startDelay = 3f;
    private float repeatRate = 5f;

    void Start()
    {
        InvokeRepeating("SpawnWall", startDelay, repeatRate);
        _playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void SpawnWall()
    {
        if (_playerControllerScript.gameOver == false)
        {
            spawnPos = new Vector2(0f, Random.Range(0f, 5f));
            Instantiate(wallPrefab, spawnPos, wallPrefab.transform.rotation);
        }
    }
}
