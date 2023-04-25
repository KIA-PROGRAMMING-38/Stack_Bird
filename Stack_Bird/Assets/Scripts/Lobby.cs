using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lobby : MonoBehaviour
{
    private Button TapToPlayBtn;

    void Start()
    {
        TapToPlayBtn = GetComponent<Button>();
        gameObject.SetActive(true);
    }

    public void ClickTapToPlayBtn()
    {
        gameObject.SetActive(false);
    }
}
