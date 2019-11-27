using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    GameManager gm;
    /// <summary>
    /// Is this the first scene before the game starts.
    /// </summary>
    public bool startScene;

    [Header("Boost Bar")]
    public Slider boostBar;
    public Color boostReadyColor;
    public Color boostChargingColor;

    [Header("Score Board")]
    public Text scoreText;
    public Text finalScoreText;

    [Header("Panels")]
    public GameObject gamePanel;
    public GameObject pausePanel;
    public GameObject gameOverPanel;

    [Header("Music Button")]
    public Toggle musicButton;
    public Image musicImageOn;
    public Image musicImageOff;

    [Header("Sound Button")]
    public Toggle soundButton;
    public Image soundImageOn;
    public Image soundImageOff;

    [Header("Scene Transition")]
    public bool fadeOutCompleted;
    public bool fadeInCompleted;
    public Animator fadeAnimator;
    int sceneToGoTo;

    void Start()
    {
        if (!startScene)
        {
            gm = GameManager.instance;
        }
    }
    private void Update()
    {
        if (!startScene)
        {
            OperateScoreBoard();
            OperateBoostBar();
            OperateAudioButtons();
        }

        if (fadeOutCompleted)
        {
            SceneManager.LoadScene(sceneToGoTo);
        }
    }
    void OperateScoreBoard()
    {
        if (gm.score > gm.highScore)
        {
            scoreText.text = "Score:\n" + gm.score + "\nHighScore:\n" + gm.score;
        }
        else
        {
            scoreText.text = "Score:\n" + gm.score + "\nHighScore:\n" + gm.highScore;
        }

    }
    void OperateBoostBar()
    {
        boostBar.value = gm.boostAmount;
        if (boostBar.value != 1)
        {
            boostBar.fillRect.GetComponent<Image>().color = boostChargingColor;
        }
        else
        {
            boostBar.fillRect.GetComponent<Image>().color = boostReadyColor;
        }
    }
    void OperateAudioButtons()
    {
        if (musicButton.isOn)
        {
            musicImageOn.enabled = true;
            musicImageOff.enabled = false;
        }
        else
        {
            musicImageOn.enabled = false;
            musicImageOff.enabled = true;
        }

        if (soundButton.isOn)
        {
            soundImageOn.enabled = true;
            soundImageOff.enabled = false;
        }
        else
        {
            soundImageOn.enabled = false;
            soundImageOff.enabled = true;
        }
    }
    bool paused;
    public void TogglePause()
    {
        if (!startScene)
        {
            if (paused)
            {
                gamePanel.SetActive(true);
                pausePanel.SetActive(false);
                Time.timeScale = 1;
                paused = false;
            }
            else
            {
                gamePanel.SetActive(false);
                pausePanel.SetActive(true);
                Time.timeScale = 0;
                paused = true;
            }
        }
    }
    public void OpenGameOverPanel(bool openPanel)
    {
        if (!startScene)
        {
            //Time.timeScale = openPanel == true ? 0 : 1;
            gameOverPanel.SetActive(openPanel);
            gamePanel.SetActive(!openPanel);
            pausePanel.SetActive(false);
        }
    }
    public void OnFadeOutComplete()
    {
        fadeOutCompleted = true;
    }
    public void OnFadeInComplete()
    {
        fadeInCompleted = true;
    }
    public void GoToScene(int scene)
    {
        sceneToGoTo = scene;
        fadeAnimator.SetTrigger("FadeOut");
    }
}
