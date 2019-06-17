using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score;
    public int highScore;
    public bool gameOver;

    public static GameManager instance = null;

    public UIManager uIManager;
    public EnvironmentGenerator environmentGenerator;
    public BugSpawner bugSpawner;

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

        uIManager = GetComponent<UIManager>();
        environmentGenerator = GetComponent<EnvironmentGenerator>();
        bugSpawner = GetComponent<BugSpawner>();
    }


    private void Start()
    {
        environmentGenerator.PlaceObsticals();
        bugSpawner.isSpawning(true);
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            uIManager.TogglePause();
        }
    }

    public void GameOver()
    {
        gameOver = true;
        uIManager.OpenGameOverPanel(true);
    }


    
}
