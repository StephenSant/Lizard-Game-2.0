using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score;
    public int highScore;
    public bool gameOver;

    public float boostAmount;

    public static GameManager instance = null;

    private UIManager uIManager;
    private EnvironmentGenerator environmentGenerator;
    private BugSpawner bugSpawner;
    private TouchInputs touchInputs;

    public float verticalInput;
    public float horizontalInput;

    public bool boostActive;

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
        boostActive = touchInputs.boostActive;
        horizontalInput = touchInputs.horizontalInput;
        verticalInput = touchInputs.verticalInput;
    }

    public void GameOver()
    {
        gameOver = true;
        uIManager.OpenGameOverPanel(true);
    }


    
}
