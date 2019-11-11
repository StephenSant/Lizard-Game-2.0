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

    [Header("Audio")]
    public bool music;
    public bool sound;
    public AudioSource gameOverSound;
    public AudioSource clickSound;


    public void Save()
    {
        SaveData saveData = new SaveData
        {
            highScore = instance.highScore,
            music = instance.music,
            sound = instance.sound
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
            music = loadedData.music;
            sound = loadedData.sound;
            musicButton.isOn = music;
            soundButton.isOn = sound;
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

        if (Input.GetKeyDown(KeyCode.P))
        {
            uIManager.TogglePause();
        }
        boostActive = touchInputs.boostActive;
        horizontalInput = touchInputs.horizontalInput;
        verticalInput = touchInputs.verticalInput;

#if !UNITY_IOS || !UNITY_ANDROID
        if (score > 5)
        { Destroy(instructions); }
#endif
    }

    public void GameOver()
    {
        gameOverSound.Play();
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
        sound = soundButton.isOn;
        Save();
    }

    public void SetMusic()
    {
        music = musicButton.isOn;
        Save();
    }

    public void Click()
    {
        clickSound.Play();
    }
}

class SaveData
{
    public int highScore;
    public bool music;
    public bool sound;
}