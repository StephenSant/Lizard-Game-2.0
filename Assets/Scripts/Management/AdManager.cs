﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{
    [SerializeField] string gameId = "";
    [SerializeField] string adPlacementId;

    void Start()
    {
        Advertisement.Initialize(gameId, false);
    }
    public void ShowAd(Action<ShowResult> callback)
    {
        if (Advertisement.IsReady(adPlacementId))
        {
            ShowOptions so = new ShowOptions();
            so.resultCallback = callback;
            Advertisement.Show(adPlacementId, so);
        }
        else
        {
            Debug.Log("Ad loading...");
        }
    }
    public void PlayAd()
    {
        GameManager.instance.adManager.ShowAd(OnAdClosed);
    }

    void OnAdClosed(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                GetComponent<GameManager>().StartCoroutine("Respawn");
                Debug.Log("Ad completed!");
                break;
            case ShowResult.Skipped:
                Debug.Log("Ad skipped!");
                break;
            case ShowResult.Failed:
                Debug.LogError("Ad failed!");
                break;
        }
    }
}
