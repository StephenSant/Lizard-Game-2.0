using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    GameManager gm;

    public Text scoreText;
    public Text finalScoreText;
    public Slider boostBar;
    public GameObject gamePanel;
    public GameObject pausePanel;
    public GameObject gameOverPanel;

    void Start()
    {
        gm = GameManager.instance;
    }
    private void Update()
    {
        scoreText.text = "Score: \n" + gm.score;
        boostBar.value = gm.boostAmount;
    }

    bool paused;
    public void TogglePause()
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

    public void OpenGameOverPanel(bool openPanel)
    {
        Time.timeScale = openPanel == true ? 0 : 1;
        gameOverPanel.SetActive(openPanel);
        gamePanel.SetActive(!openPanel);
        pausePanel.SetActive(false);
    }

    public void GoToScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
