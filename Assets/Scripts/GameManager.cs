using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public GameObject touchInputObject;
    public GameObject pauseButton;
    public GameObject uIPanel;

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

    public bool playerHidden = true;

    string json;

    void Save()
    {
        SaveData saveData = new SaveData { highScore = instance.highScore };
        json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.dataPath + "/save.txt", json);
    }

    void Load()
    {
        if (File.Exists(Application.dataPath + "/save.txt"))
        {
            string saveString = File.ReadAllText(Application.dataPath + "/save.txt");
            SaveData loadedData = JsonUtility.FromJson<SaveData>(saveString);
            highScore = loadedData.highScore;
        }
    }

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

#if (UNITY_EDITOR || UNITY_STANDALONE)
        pauseButton.transform.SetParent(uIPanel.transform);
        Destroy(touchInputObject);
#endif

        
    }


    private void Start()
    {
        environmentGenerator.PlaceObsticals();
        bugSpawner.IsSpawning(true);
        Time.timeScale = 1;
        Load();
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
        if (score > highScore)
        {
            highScore = score;
            Save();
        }
        uIManager.finalScoreText.text = "Your score: " + score + "\nHigh score: " + highScore;
    }


    
}

class SaveData
{
    public int highScore;
}