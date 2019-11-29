using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdManager : MonoBehaviour
{
    GameManager gm;

    void Start()
    {
        gm = GameManager.instance;
    }

    public void PlayAd()
    {
        OnAdComplete();
    }

    void OnAdComplete()
    {
        gm.secondChance = true;
        StartCoroutine(gm.Respawn());
        gm.uIManager.adPanel.SetActive(false);
        gm.uIManager.gamePanel.SetActive(true);
    }
}
