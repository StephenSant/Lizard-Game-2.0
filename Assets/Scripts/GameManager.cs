using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score;
    public bool gameOver;
    public Transform bugSpawnerParent;

    public Transform[] bugSpawners;

    public static GameManager instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        for (int i = 0; i < bugSpawners.Length; i++)
        {
            bugSpawners[i] = bugSpawnerParent.GetChild(i);
        }
        StartGame();
    }
    void StartGame()
    {

    }

}
