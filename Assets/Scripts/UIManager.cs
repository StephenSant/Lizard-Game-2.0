using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    GameManager gm;

    public bool startScene;

    public Text scoreText;
    public Text finalScoreText;
    public Slider boostBar;
    public GameObject gamePanel;
    public GameObject pausePanel;
    public GameObject gameOverPanel;

    public Toggle musicButton;
    public Image musicImageOn;
    public Image musicImageOff;

    public Toggle soundButton;
    public Image soundImageOn;
    public Image soundImageOff;

    public bool fadeOutCompleted;
    public bool fadeInCompleted;

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

            if (gm.score > gm.highScore)
            {
                scoreText.text = "Score:\n" + gm.score + "\nHighScore:\n" + gm.score;
            }
            else
            {
                scoreText.text = "Score:\n" + gm.score + "\nHighScore:\n" + gm.highScore;
            }
            boostBar.value = gm.boostAmount;
        }

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

        if (fadeOutCompleted)
        {
            SceneManager.LoadScene(sceneToGoTo);
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
        gm.fadeAnimator.SetTrigger("FadeOut");
        
    }
}
