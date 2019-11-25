using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameManager : MonoBehaviour
{
    [Header("Game Management")]
    public int score;
    public int highScore;
    public bool gameOver;

    [Header("UI/Display")]
    public GameObject touchInputObject;
    public GameObject pauseButton;
    public GameObject uIPanel;
    public GameObject instructions;
    public Toggle musicButton;
    public Toggle soundButton;
    public Image fadeImage;

    [Header("References")]
    public static GameManager instance = null;
    public UIManager uIManager;
    public EnvironmentGenerator environmentGenerator;
    public BugSpawner bugSpawner;
    public AudioManager audioManager;

    [Header("Input")]
    public float verticalInput;
    public float horizontalInput;
    public TouchInputs touchInputs;

    [Header("Saving")]
    public string saveLocation = "/SaveData.txt";
    string json;

    [Header("Player")]
    public bool boostActive;
    public float boostAmount;
    public bool playerHidden = true;


    public void Save()
    {
        SaveData saveData = new SaveData
        {
            highScore = instance.highScore,
            music = musicButton.isOn,
            sound = soundButton.isOn
        };
        json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.dataPath + saveLocation, json);
    }

    void Load()
    {
        if (File.Exists(Application.dataPath + saveLocation))
        {
            string saveString = File.ReadAllText(Application.dataPath + saveLocation);
            SaveData loadedData = JsonUtility.FromJson<SaveData>(saveString);
            instance.highScore = loadedData.highScore;
            musicButton.isOn = loadedData.music;
            soundButton.isOn = loadedData.sound;
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

        #if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
        pauseButton.transform.SetParent(uIPanel.transform);
        Destroy(touchInputObject);
        #endif
        #if UNITY_IOS || UNITY_ANDROID
        Destroy(instructions);
        #endif 
    }


    private void Start()
    {
        StartCoroutine(Starting());
    }

    IEnumerator Starting()
    {
        environmentGenerator.PlaceObsticals();
        Load();
        yield return new WaitUntil(() => uIManager.fadeInCompleted);
        Time.timeScale = 1;
    }

    private void Update()
    {
        boostActive = touchInputs.boostActive;
        horizontalInput = touchInputs.horizontalInput;
        verticalInput = touchInputs.verticalInput;

#if !UNITY_IOS || !UNITY_ANDROID
        if (score > 5)
        { Destroy(instructions); }
        if (Input.GetKeyDown(KeyCode.P))
        {
            uIManager.TogglePause();
        }
#endif
        
    }

    public void GameOver()
    {
        audioManager.PlaySound("Game Over");
        gameOver = true;
        uIManager.OpenGameOverPanel(true);
        if (score > highScore)
        {
            highScore = score;
        }
        Save();
        uIManager.finalScoreText.text = "Your score: " + score + "\nHigh score: " + highScore;

    }

    public void SetSound()
    {
        audioManager.UpdateSound(soundButton.isOn);
        Save();
    }

    public void SetMusic()
    {
        Save();
    }
}

class SaveData
{
    public int highScore;
    public bool music;
    public bool sound;
}