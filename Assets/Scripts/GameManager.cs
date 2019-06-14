using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score;
    public bool gameOver;

    public static GameManager instance = null;

    public EnvironmentGenerator environmentGenerator;
    public BugSpawner bugSpawner;

    public Text scoreText;

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

        environmentGenerator = GetComponent<EnvironmentGenerator>();
        bugSpawner = GetComponent<BugSpawner>();
        environmentGenerator.PlaceObsticals();
    }


    private void Start()
    {
        bugSpawner.isSpawning(true);
    }

    private void Update()
    {
        scoreText.text = "Score: " + score;
    }
}
