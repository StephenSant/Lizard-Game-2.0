using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score;
    public int highScore;
    public bool gameOver;

    public static GameManager instance = null;

    private UIManager uIManager;
    private EnvironmentGenerator environmentGenerator;
    private BugSpawner bugSpawner;
    private TouchInputs touchInputs;

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
        touchInputs = GetComponent<TouchInputs>();
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
