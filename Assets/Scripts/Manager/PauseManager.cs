using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;

    [Header("UI Panels")]
    public GameObject pausePanel;
    public GameObject optionsPanel;

    private bool _isPaused = false;

    private void Awake()
    {
        Instance = this;
    }

    public void TogglePause()
    {
        if (_isPaused) ResumeGame();
        else PauseGame();
    }

    public void PauseGame()
    {
        _isPaused = true;
        Time.timeScale = 0f; // Gèle la physique et les animations du jeu
        pausePanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    public void ResumeGame()
    {
        _isPaused = false;
        Time.timeScale = 1f; // Relance le temps
        pausePanel.SetActive(false);
        optionsPanel.SetActive(false);
    }

    public void OpenOptions()
    {
        pausePanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void CloseOptions() // Pour le bouton Back
    {
        optionsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void LoadMainMenu(string menuSceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(menuSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}