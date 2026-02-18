using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject infoPanel;
    public SettingsScript settingsPanelScript;

    private void Start()
    {
        AudioScript.Instance.Load();
    }
    public void GoToGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("You have quit the game!");
    }

    public void OpenInfoPanel()
    {
        if (infoPanel != null)
        {
            infoPanel.SetActive(true);
        }
    }

    public void CloseInfoPanel()
    {
        if (infoPanel.activeSelf)
        {
            infoPanel.SetActive(false);
        }
    }
                                
    public void OpenSettingsPanel()
    {
        settingsPanelScript.OpenPanel();
    }

    public void CloseSettingsPanel()
    {
        settingsPanelScript.ClosePanel();

    }
}
