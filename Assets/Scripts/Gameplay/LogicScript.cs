using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public static LogicScript Instance { get; private set; }
    public int playerScore;
    public int highScore;

    public TextMeshProUGUI playerText;
    public GameObject GameOverScreen;
    public GameObject gamePauseScreen;

    public TextMeshProUGUI scoreLabel;
    public TextMeshProUGUI highScoreLabel;

    public AudioClip GameOverClip;

    public float pipeMoveSpeed = 5;
    public bool gameIsPaused = false;

    public GameObject settingsPanel;
    public SettingsScript settingsPanelScript;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
    }
    void Start()
    {
        highScore = PlayerPrefs.GetInt("Score", 0);
        Time.timeScale = 1f;
    }
    public void AddPlayerScore(int scoreToAdd)
    {
        playerScore += scoreToAdd;
        if (playerScore % 10 == 0)
        {
           pipeMoveSpeed++;
        }
        playerText.text = playerScore.ToString();
    }

    public void RestartGame()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {

        if (GameOverScreen.activeSelf) return;
        GameOverScreen.SetActive(true);
       
        
        
        if(highScore < playerScore)
        {
            highScore = playerScore;
        }
        
        PlayerPrefs.SetInt("Score", highScore);

        scoreLabel.text += playerScore.ToString();
        highScoreLabel.text += highScore.ToString();
       
        AudioScript.Instance.StopBGM();
        AudioScript.Instance.PlaySFX(GameOverClip);
    }

    public void PauseGame()
    {
        if(gamePauseScreen.activeSelf) return;
        if (IsGamePlaying())
        {
            gamePauseScreen.SetActive(true);
            Time.timeScale = 0;
            gameIsPaused = true;
        }
    }

    public void ResumeGame()
    {
        gamePauseScreen.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }


    public void GoToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void OpenSettingsPanel()
    {
        settingsPanelScript.OpenPanel();
    }

    public void CloseSettingsPanel()
    {

        settingsPanelScript.ClosePanel();
    }

    public bool IsGamePlaying()
    {
        return !GameOverScreen.activeSelf && !gamePauseScreen.activeSelf && !settingsPanel.activeSelf;
    }

    public bool IsGameOver()
    {
        return !gamePauseScreen.activeSelf;
    }

    public bool IsGamePaused()
    {
        return !GameOverScreen.activeSelf && !settingsPanel.activeSelf;
    }

    public bool IsSettingsOn()
    {
        return !GameOverScreen.activeSelf && !gamePauseScreen.activeSelf;
    }
}
