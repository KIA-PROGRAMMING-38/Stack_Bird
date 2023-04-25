using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lobby : MonoBehaviour
{
    [SerializeField] private GameObject Player, WallSpawner;

    public void ClickTapToPlayBtn()
    {
        gameObject.SetActive(false);

        Player.GetComponent<PlayerController>().enabled = true;
        WallSpawner.GetComponent<WallSpawn>().enabled = true;
    }
}
