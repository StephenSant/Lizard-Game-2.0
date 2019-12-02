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
    public bool secondChance;

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
    public AdManager adManager;

    [Header("Input")]
    public float verticalInput;
    public float horizontalInput;
    public TouchInputs touchInputs;

    [Header("Saving")]
    public string saveLocation = "/SaveData.txt";
    string json;

    [Header("Player")]
    public GameObject playerInst;
    public GameObject playerPrefab;
    public float invulnTime;
    public LayerMask invulnLayer;
    public LayerMask playerLayer;
    public bool boostActive;
    public float boostAmount;
    public bool playerHidden = true;

    [Header("Bird")]
    public GameObject bird;
    public GameObject birdPoint;


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

    public IEnumerator Respawn()
    {
        bird.GetComponent<Bird>().player = birdPoint.transform;
        playerInst = Instantiate(playerPrefab, Vector3.forward * 0.5f, transform.rotation, null);
        playerInst.layer = LayermaskToLayer(invulnLayer);
        yield return new WaitForSeconds(invulnTime);
        playerInst.layer = LayermaskToLayer(playerLayer);
        yield return new WaitUntil(() => (bird.transform.position - birdPoint.transform.position).magnitude <= 0.5f);
        bird.GetComponent<Bird>().player = playerInst.transform;
    }

    public static int LayermaskToLayer(LayerMask layerMask)
    {
        int layerNumber = 0;
        int layer = layerMask.value;
        while (layer > 0)
        {
            layer = layer >> 1;
            layerNumber++;
        }
        return layerNumber - 1;
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

    public void PlayerEaten()
    {
        uIManager.gamePanel.SetActive(false);
        audioManager.PlaySound("Game Over");
        if (secondChance)
        {
            GameOver();
        }
        else
        {
            uIManager.adPanel.SetActive(true);
        }
    }

    public void GameOver()
    {
        uIManager.adPanel.SetActive(false);
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