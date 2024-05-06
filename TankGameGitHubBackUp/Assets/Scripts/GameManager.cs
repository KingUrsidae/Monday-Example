using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Things
    public HighScores m_HighScores;

    public TextMeshProUGUI m_MessageText;
    public TextMeshProUGUI m_TimerText;
    public TextMeshProUGUI m_HighScoresText;
    public TextMeshProUGUI J_AmmoCounterText;
    // Panels
    public GameObject m_HighScorePanel;
    public GameObject m_LevelSelecetPanel;
    // Buttons
    public Button m_LevelSelectButton;
    public Button m_LevelSelecet1;
    public Button m_LevelSelecet2;
    public Button m_LevelSelecet3;
    public Button m_QuitButton;
    public Button m_HighScoresButton;
    // More things
    public GameObject[] m_Tanks;
    private float m_gameTime = 0;
    // Game state stuff
    public enum GameState
    {
        Start,
        Playing,
        GameOver
    };
    private GameState m_GameState;
    public GameState State { get { return m_GameState; } }
    private void Awake()
    {
        m_GameState = GameState.Start;
    }
    private void Start()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            m_Tanks[i].SetActive(false);
        }
        m_TimerText.gameObject.SetActive(false);
        m_MessageText.text = "Get Ready";

        m_HighScoresButton.gameObject.SetActive(false);
        m_LevelSelectButton.gameObject.SetActive(false);
        m_HighScorePanel.gameObject.SetActive(false);
        J_AmmoCounterText.gameObject.SetActive(false); 
    }
    private bool OneTankLeft()
    {
        int numTanksLeft = 0;

        for (int i = 0; i < m_Tanks.Length; i++)
        {
            if (m_Tanks[i].activeSelf == true)
            {
                numTanksLeft++;
            }
        }
        return numTanksLeft <= 1;
    }
    private bool IsPlayerDead()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            if (m_Tanks[i].activeSelf == false)
            {
                if (m_Tanks[i].tag == "Player")
                    return true;
            }
        }
        return false;
    }
    public float GameTime { get { return m_gameTime; } }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
        switch (m_GameState)
        {
            case GameState.Start:
                GameStateStart();
                break;
            case GameState.Playing:
                GameStatePlaying();
                break;
            case GameState.GameOver:
                GameStateGameOver();
                break;
        }
    }
    private void GameStateStart()
    {
        if (Input.GetKeyUp(KeyCode.Return) == true)
        {
            OnNewGame();
        }
    }
    private void GameStatePlaying()
    {
        bool isGameOver = false;

        m_gameTime += Time.deltaTime;
        int seconds = Mathf.RoundToInt(m_gameTime);
        
        m_TimerText.text = string.Format("{0:D2}:{1:D2}", (seconds / 60), (seconds % 60));

        if (IsPlayerDead() == true)
        {
            m_MessageText.text = "YOU DIED";
            isGameOver = true;
        }
        else if (OneTankLeft() == true)
        {
            m_MessageText.text = "YOU WON";
            isGameOver = true;
            m_HighScores.AddScore(Mathf.RoundToInt(m_gameTime));
            m_HighScores.SaveScoresToFile();
        }

        if (isGameOver == true)
        {
            m_GameState = GameState.GameOver;
            m_LevelSelectButton.gameObject.SetActive(true);
            m_HighScoresButton.gameObject.SetActive(true);
        }
    }
    private void GameStateGameOver()
    {
        if (Input.GetKeyDown(KeyCode.Return) == true)
        {
            OnLevelSelect();
        }
    }
    public void OnNewGame()
    {
        m_HighScoresButton.gameObject.SetActive(false);
        m_HighScorePanel.gameObject.SetActive(false);
        m_LevelSelecetPanel.gameObject.SetActive(false);
        m_LevelSelectButton.gameObject.SetActive(false);

        J_AmmoCounterText.gameObject.SetActive(true);
        m_TimerText.gameObject.SetActive(true);
        m_MessageText.text = "";

        m_gameTime = 0;
        m_GameState = GameState.Playing;

        for (int i = 0; i < m_Tanks.Length; i++)
        {
            m_Tanks[i].SetActive(true);
        }
    }
    // Highscores stuff
    public void OnHighScores()
    {
        m_MessageText.text = "";

        m_HighScoresButton.gameObject.SetActive(false);
        m_HighScorePanel.gameObject.SetActive(true);

        string text = "";
        for (int i = 0; i < m_HighScores.scores.Length; i++)
        {
            int seconds = m_HighScores.scores[i];
            text += string.Format("{0:D2}:{1:D2}\n", 
                            (seconds / 60), (seconds % 60));
        }
        m_HighScoresText.text = text;
    }
    // Level select stuff
    public void OnLevelSelect()
    {
        m_LevelSelecetPanel.gameObject.SetActive(true);
        m_HighScoresButton.gameObject.SetActive(false);
        m_HighScorePanel.gameObject.SetActive(false);
        m_HighScoresButton.gameObject.SetActive(false);
        m_LevelSelectButton.gameObject.SetActive(false);
        m_TimerText.gameObject.SetActive(false);
    }
    public void OnLevelSelected1()
    {
        SceneManager.LoadScene(0);
    }
    public void OnLevelSelected2()
    {
        SceneManager.LoadScene(1);
    }
    public void OnLevelSelected3()
    {
        SceneManager.LoadScene(2);
    }
    public void OnQuit()
    {
        Application.Quit();
    }
}
